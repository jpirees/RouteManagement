using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RouteManagement.Models;

namespace RouteManagement.Services
{
    public static class CitiesService
    {
        readonly static string baseUri = "https://localhost:44306/api/";

        public static async Task<List<CityViewModel>> Get()
        {
            List<CityViewModel> people = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Cities");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    people = JsonConvert.DeserializeObject<List<CityViewModel>>(responseBody);
                }
                else
                {
                    people = new List<CityViewModel>();
                }
            }

            return people;
        }

        public static async Task<CityViewModel> Get(string id)
        {
            CityViewModel people = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync($"Cities/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    people = JsonConvert.DeserializeObject<CityViewModel>(responseBody);
                }
                else
                {
                    people = new CityViewModel();
                }
            }

            return people;
        }


        public static async Task Create(CityViewModel city)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                await httpClient.PostAsJsonAsync("Cities", city);
            }
        }

        public static async Task<HttpResponseMessage> Update(string id, CityViewModel city)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                return await httpClient.PutAsJsonAsync($"Cities/{id}", city);
            }
        }
        public static async Task<HttpResponseMessage> Delete(string id)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                return await httpClient.DeleteAsync($"Cities/{id}");

            }
        }

    }
}
