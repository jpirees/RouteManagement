using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RouteManagement.Models;

namespace RouteManagement.Controllers
{
    public class TeamsController : Controller
    {
        private readonly string baseUri = "https://localhost:44373/api/";

        public IActionResult Index()
        {
            IEnumerable<TeamViewModel> teams = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var responseRequest = httpClient.GetAsync("Teams");
                responseRequest.Wait();

                var response = responseRequest.Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    teams = JsonConvert.DeserializeObject<IList<TeamViewModel>>(responseBody);
                }
                else
                {
                    teams = Enumerable.Empty<TeamViewModel>();
                    ModelState.AddModelError(String.Empty, "Falha ao enviar requisição");
                }
            }

            return View(teams);
        }


        public IActionResult Details(string id)
        {
            TeamViewModel team = null;

            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var responseRequest = httpClient.GetAsync($"Teams/{id}");
                responseRequest.Wait();

                var response = responseRequest.Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    team = JsonConvert.DeserializeObject<TeamViewModel>(responseBody);
                }
                else
                {
                    team = null;
                    ModelState.AddModelError(String.Empty, "Falha ao enviar requisição");
                }

            }

            return View(team);
        }
    }
}
