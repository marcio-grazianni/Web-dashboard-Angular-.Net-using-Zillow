using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using CDACommercial.PoC.Infrastructure.AirDNA.Contracts;
using CDACommercial.PoC.Infrastructure.Idsts.Contracts;
using CDACommercial.PoC.Infrastructure.Process.Contracts;
using CDACommercial.PoC.Infrastructure.Zillow.Contracts;
using CDACommercial.PoC.Infrastructure.Zillow.DTO;
using Newtonsoft.Json;

namespace CDACommercial.PoC.Infrastructure.Process.Jobs
{
    public class ListingDiscovery : IListingDiscovery
    {
        protected readonly IApify _apifyService;
        protected readonly IAirDNAService _airDNAService;
        protected readonly IPredictions _predictionService;

        protected readonly IApifyRunRepository _apifyRunRepository;
        
        protected readonly IStateRepository _stateRepository;
        protected readonly ICityRepository _cityRepository;
        protected readonly IListingRepository _listingRepository;
        public ListingDiscovery(
            IApify apifyService,
            IAirDNAService airDNAService,
            IPredictions predictionService,
            IApifyRunRepository apifyRunRepository,
            IStateRepository stateRepository,
            ICityRepository cityRepository,
            IListingRepository listingRepository)
        {
            _apifyService = apifyService;
            _airDNAService = airDNAService;
            _predictionService = predictionService;
            _apifyRunRepository = apifyRunRepository;
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _listingRepository = listingRepository;
        }

        public async Task<string> RunAsync()
        {
            var result = "";
            var runs = await _apifyService.GetActorRunsAsync();
            if (runs.Count > 0) await _listingRepository.FlushAsync();
            foreach (var run in runs)
            {
                var entity = await _apifyRunRepository.GetByRunIdAsync(run.Id);
                if (entity == null)
                {
                    entity = new ApifyRun(run.Id, JsonConvert.SerializeObject(run));
                    try
                    {
                        await GetListingsFromDatasetAsync(run.DefaultDatasetId);
                        _apifyRunRepository.Add(entity);
                        await _apifyRunRepository.UnitOfWork.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        entity.Fail(e.Message);
                        _apifyRunRepository.Add(entity);
                        await _apifyRunRepository.UnitOfWork.SaveChangesAsync();
                        throw;
                    }
                }
            }
            return result;
        }

        private async Task GetListingsFromDatasetAsync(string datasetId)
        {
            var path = await _apifyService.DownloadListingsByDatasetId(datasetId);

            using (StreamReader r = new StreamReader(path))
            {
                string json = await r.ReadToEndAsync();
                List<Property> properties = JsonConvert.DeserializeObject<List<Property>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                await ProcessListings(properties);
            }
            await NotifyPredictionServerAsync(datasetId);

        }
        private async Task<string> GetListingsFromDatasetAsyncOld(string datasetId)
        {
            //https://www.tugberkugurlu.com/archive/efficiently-streaming-large-http-responses-with-httpclient
            //https://newbedev.com/parsing-large-json-file-in-net
            var url = _apifyService.GetListingsURLByDatasetId(datasetId);
            var properties = new List<Property>();
            var failures = 0;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader streamReader = new StreamReader(stream))
                using (JsonTextReader reader = new JsonTextReader(streamReader))
                {
                    reader.SupportMultipleContent = true;

                    var serializer = new JsonSerializer();
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            try
                            {
                                Property p = serializer.Deserialize<Property>(reader);
                                properties.Add(p);

                                if (properties.Count == 100)
                                {
                                    await ProcessListings(properties);
                                    properties = new List<Property>();
                                }
                            }
                            catch (Exception e)
                            {
                                failures++;
                            }
                        }
                    }
                    if (properties.Count > 0)
                    {
                        await ProcessListings(properties);
                    }
                }
            }
            await NotifyPredictionServerAsync(datasetId);
            return failures > 0 ? $"Failed parsing {failures} listings" : "";
        }

        private async Task ProcessListings(List<Property> properties)
        {
            var states = await _stateRepository.ListAsync();
            foreach (var p in properties)
            {
                if (p.Address != null)
                {
                    var propertyJson = JsonConvert.SerializeObject(p);
                    var listing = await _listingRepository.FindByZillowIdAsync(p.Zpid);
                    if (listing == null)
                    {

                        var state = states
                            .Where(s => s.Code.Equals(p.Address.State))
                            .FirstOrDefault();
                        
                        if(state == null)
                        {
                            state = new State(p.Address.State, "");
                            _stateRepository.Add(state);
                            await _stateRepository.UnitOfWork.SaveChangesAsync();
                            states.Add(state);
                        }

                        var city = await _cityRepository.FindByNameAsync(p.Address.City);
                        if (city == null)
                        {
                            city = new Domain.Entities.City(state.Id, p.Address.City, p.Address.State);
                            _cityRepository.Add(city);
                            await _cityRepository.UnitOfWork.SaveChangesAsync();
                        }
                        
                        listing = new Listing(p.Zpid, p.DaysOnZillow, p.HomeStatus, p.HomeType);
                        listing.AddPricingInfo(p.Price, p.GetLastTaxPaid());
                        listing.AddInfo(p.LivingArea, p.Bedrooms, p.Bathrooms);
                        listing.AddAddressInfo(p.Address.StreetAddress, p.Address.Zipcode, p.Address.State);
                        listing.AddZillowData(propertyJson);
                        city.AddListing(listing);
                    }
                    else
                    {
                        listing.UpdateStatus(p.DaysOnZillow, p.HomeStatus);
                        listing.AddZillowData(propertyJson);
                        _listingRepository.Update(listing);
                        await _listingRepository.UnitOfWork.SaveChangesAsync();
                    }
                    
                }
            }
            await _cityRepository.UnitOfWork.SaveChangesAsync();
        }

        private async Task NotifyPredictionServerAsync(string datasetId)
        {
            try
            {
                var url = _apifyService.GetListingsURLByDatasetId(datasetId, "csv");
                await _predictionService.SendLinkAsync(url);
            }
            catch
            {

            }
        }
    }
}