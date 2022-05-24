using CDACommercial.PoC.Infrastructure.Zillow.Contracts;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using CDACommercial.PoC.Infrastructure.Zillow.DTO;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CDACommercial.PoC.Infrastructure.Zillow.Services 
{
    public class Apify : IApify
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;
        protected readonly string _clientToken;
        protected readonly string _storagePath;
        public Apify(IConfiguration configuration, HttpClient httpClient)
        {
            var section = configuration.GetSection("Apify");
            var host = section.GetValue<string>("Host");
            var version = section.GetValue<string>("Version");

            _httpClient = httpClient;
            //_baseUrl = "https://api.apify.com/v2/datasets/G9ndyLqflpkgRZfYh/items?clean=true&format=json";
            _baseUrl = $"{host}/{version}";
            _clientToken = section.GetValue<string>("ClientToken");
            _storagePath = configuration["StoragePath"];
        }
        private async Task<T> Get<T>(Uri uri)
        {
            var response = await _httpClient.GetAsync(uri);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
        public async Task<List<ActorRun>> GetActorRunsAsync()
        {
            var url = $"{_baseUrl}/actor-runs?token={_clientToken}&desc=1&status=SUCCEEDED";
            var response = await Get<ActorRunResponse>(new Uri(url));
            return response != null ? response.Data.Items : new List<ActorRun>();
        }
        public async Task<List<Property>> GetListingsByDatasetIdAsync(string datasetId)
        {
            var url = $"{_baseUrl}/datasets/{datasetId}/items?token={_clientToken}&clean=true&format=json";
            var properties = await Get<List<Property>>(new Uri(url));
            return properties;
        }

        public async Task<string> DownloadListingsByDatasetId(string datasetId)
        {
            var url = $"{_baseUrl}/datasets/{datasetId}/items?token={_clientToken}&clean=true&format=json";
            string path = Path.Combine(_storagePath, $"{datasetId}.json");

            using (WebClient wc = new WebClient())
            {
                // wc.Headers.Add("User-Agent: Test");
                await wc.DownloadFileTaskAsync(new Uri(url), path);
            }

            return path;
        }

        public string GetListingsURLByDatasetId(string datasetId, string format = "json")
        {
            return $"{_baseUrl}/datasets/{datasetId}/items?token={_clientToken}&clean=true&format={format}";
        }
    }
}