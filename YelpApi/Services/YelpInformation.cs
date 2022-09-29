using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using YelpApi.Models;

namespace YelpApi.Services
{
    internal class YelpInformation
    {
         
        private string ApiKey = string.Empty;

        public YelpInformation()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot root = builder.Build();
            ApiKey = root["APIKey"];

        }
        public async Task<ResponseData> GetYelpInformation(RequestData requestData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", ApiKey);

                var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.yelp.com/v3/businesses/search?term={requestData.term}&location={requestData.location}");

                HttpResponseMessage responseNasaData = client.SendAsync(request).Result;

                if (responseNasaData.IsSuccessStatusCode)
                {
                    var result = await responseNasaData.Content.ReadFromJsonAsync<ResponseData>();

                    return result;
                }
                else
                {
                    return new ResponseData { ErrorMessage = responseNasaData.ReasonPhrase };

                }
            }
        }
    }
}
