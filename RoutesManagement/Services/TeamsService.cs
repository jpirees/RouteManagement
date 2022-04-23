using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RoutesManagement.Models;

namespace RoutesManagement.Services
{
    public class TeamsService
    {
        readonly static string baseUri = "https://localhost:44373/api/";
        
        public static async Task<List<TeamViewModel>> Get()
        {
            List<TeamViewModel> teams = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Teams");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    teams = JsonConvert.DeserializeObject<List<TeamViewModel>>(responseBody);
                }
                else
                {
                    teams = new List<TeamViewModel>();
                }
            }

            return teams;
        }

        public static async Task<TeamViewModel> Get(string id)
        {
            TeamViewModel team = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync($"Teams/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    team = JsonConvert.DeserializeObject<TeamViewModel>(responseBody);
                }
                else
                {
                    team = new TeamViewModel();
                }
            }

            return team;
        }

        public static async Task<List<TeamViewModel>> GetTeamsByCity(string cityId)
        {
            List<TeamViewModel> teams = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync($"Teams/City/{cityId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    teams = JsonConvert.DeserializeObject<List<TeamViewModel>>(responseBody);
                }
                else
                {
                    teams = new List<TeamViewModel>();
                }
            }

            return teams;
        }

        public static async Task Create(TeamViewModel team)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                await httpClient.PostAsJsonAsync("Teams", team);
            }
        }

        public static async Task<HttpResponseMessage> Update(string id, TeamViewModel team)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                return await httpClient.PutAsJsonAsync($"Teams/{id}", team);
            }
        }

        public static async Task<HttpResponseMessage> UpdateInsert(string id, PersonViewModel person)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                return await httpClient.PutAsJsonAsync($"Teams/{id}/Insert", person);
            }
        }
        public static async Task<HttpResponseMessage> UpdateRemove(string id, PersonViewModel person)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                return await httpClient.PutAsJsonAsync($"Teams/{id}/Remove", person);
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

                return await httpClient.DeleteAsync($"Teams/{id}");
            }
        }

    }
}
