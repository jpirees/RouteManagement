using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RouteManagement.Models;

namespace RouteManagement.Controllers
{
    public class PeopleController : Controller
    {
        private readonly string baseUri = "https://localhost:44383/api/";

        public IActionResult Index()
        {
            IEnumerable<PersonViewModel> people = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var responseRequest = httpClient.GetAsync("People");
                responseRequest.Wait();

                var response = responseRequest.Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    people = JsonConvert.DeserializeObject<IList<PersonViewModel>>(responseBody);
                }
                else
                {
                    people = Enumerable.Empty<PersonViewModel>();
                    ModelState.AddModelError(String.Empty, "Falha ao enviar requisição");
                }
            }

            return View(people);
        }


        public IActionResult Details(string id)
        {
            PersonViewModel people = null;

            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var responseRequest = httpClient.GetAsync($"People/{id}");
                responseRequest.Wait();

                var response = responseRequest.Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    people = JsonConvert.DeserializeObject<PersonViewModel>(responseBody);
                }
                else
                {
                    people = null;
                    ModelState.AddModelError(String.Empty, "Falha ao enviar requisição");
                }

            }

            return View(people);
        }


        public IActionResult Create(PersonViewModel person)
        {
            if (string.IsNullOrEmpty(person.Name))
            {
                return View(person);
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseRequest = httpClient.PostAsJsonAsync("People", person);
                    responseRequest.Wait();

                    return RedirectToAction(nameof(Index));
                }
            }
        }


        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            PersonViewModel person = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var responseRequest = httpClient.GetAsync($"People/{id}");
                responseRequest.Wait();

                var response = responseRequest.Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    person = JsonConvert.DeserializeObject<PersonViewModel>(responseBody);
                }
                else
                {
                    person = null;
                    ModelState.AddModelError(String.Empty, "Falha ao enviar requisição");
                }

            }

            return View(person);
        }


        [HttpPost]
        public IActionResult Edit(string id, [Bind("Id,Name,IsAvailable")] PersonViewModel person)
        {
            if (!id.Equals(person.Id))
                return RedirectToAction(nameof(Index));
            else
            {
                using (var httpClient = new HttpClient())
                {
                    if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseRequest = httpClient.PutAsJsonAsync($"People/{id}", person);
                    responseRequest.Wait();

                    if (responseRequest.IsCompletedSuccessfully)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return View(person);
        }


        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseRequest = httpClient.DeleteAsync($"People/{id}");
                responseRequest.Wait();

                if (responseRequest.IsCompletedSuccessfully)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }
    }
}
