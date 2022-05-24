using CDACommercial.PoC.Infrastructure.AirDNA.Contracts;
using CDACommercial.PoC.Infrastructure.AirDNA.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CDACommercial.PoC.Infrastructure.AirDNA.Services
{
    public class Market : HttpService, IMarket
    {

        public Market(HttpClient httpClient, IConfiguration configuration)
        : base(configuration, "market", httpClient)
        {

        }

        public async Task<MarketSearchResponse> GetCodeAsync(string term)
        {
            var parameters = GetDefaultParameters();
            parameters["term"] = term;
            string json = await Get($"{_baseUrl}search", parameters);
            return JsonConvert.DeserializeObject<MarketSearchResponse>(json);
        }


        public async Task<MarketHistoryResponse> GetMonthlyADRAsync(long cityId, int bedrooms, int year, int startMonth, int numberOfMonths)
        {
            var parameters = GetDefaultParameters();
            var url = $"{_baseUrl}pricing/monthly"; // "http://localhost:5000/mock/adr.json"; 
            parameters["start_year"] = year.ToString();
            parameters["start_month"] = startMonth.ToString();
            parameters["number_of_months"] = numberOfMonths.ToString();
            parameters["city_id"] = cityId.ToString();
            parameters["bedrooms"] = bedrooms.ToString();
            parameters["room_types"] = "entire_place";
            string json = await Get(url, parameters);
            dynamic obj = JsonConvert.DeserializeObject(json);
            MarketHistoryResponse response;
            try
            {
                response = GetHistoricData(obj.data.adr, bedrooms);
                response.Json = json;
            }
            catch
            {
                response = new MarketHistoryResponse()
                {
                    Json = json
                };
            }
            return response;
        }

        public async Task<MarketHistoryResponse> GetMonthlyOccupancyAsync(long cityId, int bedrooms, int year, int startMonth, int numberOfMonths)
        {
            var parameters = GetDefaultParameters();
            var url = $"{_baseUrl}occupancy/monthly"; // "http://localhost:5000/mock/occupancy.json";
            parameters["start_year"] = year.ToString();
            parameters["start_month"] = startMonth.ToString();
            parameters["number_of_months"] = numberOfMonths.ToString();
            parameters["city_id"] = cityId.ToString();
            parameters["bedrooms"] = bedrooms.ToString();
            parameters["room_types"] = "entire_place";
            string json = await Get(url, parameters);
            dynamic obj = JsonConvert.DeserializeObject(json);
            MarketHistoryResponse response;
            try
            {
                response = GetHistoricData(obj.data.occupancy, bedrooms);
                response.Json = json;
            }
            catch
            {
                response = new MarketHistoryResponse()
                {
                    Json = json
                };
            }
            return response;
        }

        public async Task<MarketHistoryResponse> GetMonthlyRevenueAsync(long cityId, int bedrooms, int year, int startMonth, int numberOfMonths)
        {
            var parameters = GetDefaultParameters();
            var url = $"{_baseUrl}revenue/monthly"; // "http://localhost:5000/mock/revenue.json"
            parameters["start_year"] = year.ToString();
            parameters["start_month"] = startMonth.ToString();
            parameters["number_of_months"] = numberOfMonths.ToString();
            parameters["city_id"] = cityId.ToString();
            parameters["bedrooms"] = bedrooms.ToString();
            parameters["room_types"] = "entire_place";
            string json = await Get(url, parameters);
            dynamic obj = JsonConvert.DeserializeObject(json);
            MarketHistoryResponse response;
            try
            {
                response = GetHistoricData(obj.data.revenue, bedrooms);
                response.Json = json;
            }
            catch
            {
                response = new MarketHistoryResponse()
                {
                    Json = json
                };
            }
            return response;
        }


        public async Task<MarketSummaryResponse> GetSummaryAsync(long cityId)
        {
            var parameters = GetDefaultParameters();
            var url = $"{_baseUrl}summary"; 
            parameters["city_id"] = cityId.ToString();
            string json = await Get(url, parameters);
            dynamic obj = JsonConvert.DeserializeObject(json);

            MarketSummaryResponse response;
            try
            {
                dynamic data = obj.data;
                dynamic calendarMonths = data.calendar_months;
                response = new MarketSummaryResponse()
                {
                    Year = (int) calendarMonths.year.Value,
                    Month = (int) calendarMonths.month.Value,
                    Data = JsonConvert.SerializeObject(obj.data),
                    Json = json
                };
            }
            catch
            {
                response = new MarketSummaryResponse()
                {
                    Json = json
                };
            }
            return response;

        }
        private MarketHistoryResponse GetHistoricData(dynamic metric, int bedrooms)
        {
            var response = new MarketHistoryResponse();
            var percentiles = new string[] { "25", "50", "75", "90" };
            foreach (var calendarMonth in metric.calendar_months)
            {
                var historyMonth = new Month()
                {
                    Index = calendarMonth.month
                };
                foreach (var p in percentiles)
                {
                    var values = calendarMonth.percentiles;
                    historyMonth.Percentiles.Add(values[p].Value);
                }

                var historyYear = response.Years.Find(y => y.Index == calendarMonth.year.Value);
                if (historyYear == null)
                {
                    historyYear = new Year()
                    {
                        Index = calendarMonth.year,
                        Bedrooms = bedrooms
                    };
                    response.Years.Add(historyYear);
                }

                historyYear.Months.Add(historyMonth);
            }
            return response;
        }

    }
}