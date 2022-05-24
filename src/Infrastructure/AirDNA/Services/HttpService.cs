using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CDACommercial.PoC.Infrastructure.AirDNA.Services
{
    public class HttpService
    {
        protected readonly string _baseUrl;
        protected readonly HttpClient _httpClient;
        protected readonly string _clientToken;
        public HttpService(IConfiguration configuration, string endpoint, HttpClient httpClient)
        {
            var section = configuration.GetSection("AirDNA");
            var host = section.GetValue<string>("Host");
            var version = section.GetValue<string>("Version");
            _baseUrl = $"{host}/client/{version}/{endpoint}/";
            _clientToken = section.GetValue<string>("ClientToken");
            _httpClient = httpClient;
        }

        protected Dictionary<string, string> GetDefaultParameters()
        {
            var parameters = new Dictionary<string, string>();
            parameters["access_token"] = _clientToken;
            return parameters;
        }


        protected async Task<string> Get(string url, Dictionary<string, string> parameters)
        {
            var query = HttpUtility.ParseQueryString("");
            foreach(var p in parameters)
            {
                query[p.Key] = p.Value;
            }
            var response = await _httpClient.GetAsync(new Uri($"{url}?{query.ToString()}"));
            return await response.Content.ReadAsStringAsync();
        }
    }
}