using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RouteManagement.Models;

namespace RouteManagement.Services
{
    public static class PeopleService
    {
        readonly static string baseUri = "https://localhost:44383/api/";

        public static async Task<List<PersonViewModel>> Get()
        {
            List<PersonViewModel> people = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("People");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    people = JsonConvert.DeserializeObject<List<PersonViewModel>>(responseBody);
                }
                else
                {
                    people = new List<PersonViewModel>();
                }
            }

            return people;
        }
        
        public static async Task<PersonViewModel> Get(string id)
        {
            PersonViewModel people = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync($"People/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    people = JsonConvert.DeserializeObject<PersonViewModel>(responseBody);
                }
                else
                {
                    people = new PersonViewModel();
                }
            }

            return people;
        }
        
        public static async Task Create(PersonViewModel person)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                await httpClient.PostAsJsonAsync("People", person);
            }
        }

        public static async Task<HttpResponseMessage> Update(string id, PersonViewModel person)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                return await httpClient.PutAsJsonAsync($"People/{id}", person);
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

                return await httpClient.DeleteAsync($"People/{id}");

            }
        }
    }
}
