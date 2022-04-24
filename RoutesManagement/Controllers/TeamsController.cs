using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoutesManagement.Models;
using RoutesManagement.Services;

namespace RoutesManagement.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var teams = await TeamsService.Get();

            return View(teams);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var team = await TeamsService.Get(id);

            return View(team);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<PersonViewModel> peopleAvailable = await GetPeopleAvailable();

            IEnumerable<CityViewModel> cities = await CitiesService.Get();

            ViewBag.PeopleAvailable = peopleAvailable;
            ViewBag.Cities = cities;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, IsAvailable")] TeamViewModel team)
        {
            List<PersonViewModel> peopleSelected = new();

            if (ModelState.IsValid)
            {
                var operatingCityId = Request.Form["OperatingCity"].FirstOrDefault();

                var operatingCity = await CitiesService.Get(operatingCityId);

                if (operatingCity == null)
                    return RedirectToAction(nameof(Create));

                if (Request.Form["checkPeopleTeam"].ToList().Count == 0)
                    return RedirectToAction(nameof(Create));

                foreach (var person_id in Request.Form["checkPeopleTeam"].ToList())
                {
                    var person = await PeopleService.Get(person_id.ToString());
                    peopleSelected.Add(new PersonViewModel(person.Id, person.Name, person.IsAvailable));
                }

                team.People = peopleSelected;
                team.OperatingCity = operatingCity;

                await TeamsService.Create(team);

                return RedirectToAction(nameof(Index));
            }

            return View(team);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var team = await TeamsService.Get(id);

            var people = await PeopleService.Get();

            var peopleAvailable =
                (from person in people
                 where person.IsAvailable == true
                 select person);

            IEnumerable<CityViewModel> cities = await CitiesService.Get();

            ViewBag.PeopleAvailable = peopleAvailable;
            ViewBag.Cities = cities;

            List<PersonViewModel> peopleTeam = new();

            foreach (var person in team.People)
                peopleTeam.Add(new PersonViewModel(person.Id, person.Name, person.IsAvailable));

            ViewBag.PeopleAvailable = peopleAvailable;
            ViewBag.PeopleTeam = peopleTeam;

            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,OperatingCity,IsAvailable")] TeamViewModel team)
        {
            var teamStatus = team.IsAvailable;
            var operatingCity = await CitiesService.Get(team.OperatingCity.Id);

            if (operatingCity == null)
                return RedirectToAction(nameof(Create));

            var peopleToAdd = Request.Form["checkPeopleAvailableToAdd"].ToList();
            var peopleToRemove = Request.Form["checkPeopleTeamToRemove"].ToList();

            if (peopleToAdd.Count != 0)
                foreach (var person_id in Request.Form["checkPeopleAvailableToAdd"].ToList())
                {
                    var person = await PeopleService.Get(person_id.ToString());

                    await TeamsService.UpdateInsert(id, person);
                }

            team = await TeamsService.Get(team.Id);
            team.IsAvailable = teamStatus;
            team.OperatingCity = operatingCity;


            if (peopleToRemove.Count != 0)
                if (peopleToRemove.Count == team.People.Count)
                {
                    await TeamsService.Delete(team.Id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var person_id in Request.Form["checkPeopleTeamToRemove"].ToList())
                    {
                        var person = await PeopleService.Get(person_id.ToString());

                        await TeamsService.UpdateRemove(id, person);
                    }
                }

            var response = await TeamsService.Update(id, team);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(team);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var response = await TeamsService.Delete(id);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View();
        }

        private static async Task<IEnumerable<PersonViewModel>> GetPeopleAvailable()
        {
            var people = await PeopleService.Get();

            var peopleAvailable =
                (from person in people
                 where person.IsAvailable == true
                 select person);

            return peopleAvailable;
        }
    }
}
