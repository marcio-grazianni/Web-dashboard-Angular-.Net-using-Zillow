using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using CDACommercial.PoC.Infrastructure.Idsts.Contracts;

namespace CDACommercial.PoC.Infrastructure.Idsts.Services 
{
    public class Predictions : IPredictions
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;
        protected readonly string _key;
        public Predictions(IConfiguration configuration, HttpClient httpClient)
        {
            var section = configuration.GetSection("Idsts");
            var host = section.GetValue<string>("Host");
            var key = section.GetValue<string>("Key");

            _httpClient = httpClient;
            _baseUrl = host;
            _key = key;
        }

        // protected async Task EnsureAuthorizationHeader()
        // {
        //     // if (_httpClient.DefaultRequestHeaders.Authorization != null) return;
        //     // string uri = $"https://login.microsoftonline.com/{_tenantId}/oauth2/token";
        //     // var response = await _httpClient.PostAsync(new Uri(uri), new FormUrlEncodedContent(GetTokenRequest()));
        //     // string json = await response.Content.ReadAsStringAsync();
        //     // TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(json, _tokenSerializerSettings);
        //     // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
        // }
        
        protected Uri GetRequestUri(string uri)
        {
            return new Uri($"{uri}?api_key={_key}");
        }

        protected async Task<T> GetAsync<T>(string uri)
        {
            //await EnsureAuthorizationHeader();
            var response = await _httpClient.GetAsync(GetRequestUri(uri));
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected async Task<T> PostAsync<T, U>(string uri, U payload)
        {
            //await EnsureAuthorizationHeader();
            var data = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(GetRequestUri(uri), data);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
        public async Task<bool> SendLinkAsync(string link)
        {
            var url = $"{_baseUrl}/input/url";
            var payload = new Dictionary<string, dynamic>() { { "url", link } };
            var response = await PostAsync<dynamic, Dictionary<string, dynamic>>(url, payload);
            return response != null;
        }

    }
}