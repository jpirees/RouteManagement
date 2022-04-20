using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RouteManagement.Models;
using RouteManagement.Services;

namespace RouteManagement.Controllers
{
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

            ViewBag.PeopleAvailable = peopleAvailable;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamViewModel team)
        {
            List<PersonViewModel> peopleSelected = new();

            if (ModelState.IsValid)
            {
                if (Request.Form["checkPeopleTeam"].ToList().Count == 0)
                    return RedirectToAction(nameof(Create));

                foreach (var person_id in Request.Form["checkPeopleTeam"].ToList())
                {
                    var person = await PeopleService.Get(person_id.ToString());
                    peopleSelected.Add(new PersonViewModel(person.Id, person.Name, person.IsAvailable));
                }

                team.People = peopleSelected;

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

            List<PersonViewModel> peopleTeam = new();

            foreach (var person in team.People)
                peopleTeam.Add(new PersonViewModel(person.Id, person.Name, person.IsAvailable));

            ViewBag.PeopleAvailable = peopleAvailable;
            ViewBag.PeopleTeam = peopleTeam;

            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TeamViewModel team)
        {

            if (!id.Equals(team.Id))
                return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                var peopleToAdd = Request.Form["checkPeopleAvailableToAdd"].ToList();
                var peopleToRemove = Request.Form["checkPeopleTeamToRemove"].ToList();

                var teamToUpdate = await TeamsService.Get(id);

                if (peopleToAdd.Count != 0)
                    foreach (var person_id in Request.Form["checkPeopleAvailableToAdd"].ToList())
                    {
                        var person = await PeopleService.Get(person_id.ToString());

                        await TeamsService.UpdateInsert(id, person);
                    }

                if (peopleToRemove.Count != 0)
                    foreach (var person_id in Request.Form["checkPeopleTeamToRemove"].ToList())
                    {
                        var person = await PeopleService.Get(person_id.ToString());

                        await TeamsService.UpdateRemove(id, person);
                    }

                var response = await TeamsService.Update(id, team);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }

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
