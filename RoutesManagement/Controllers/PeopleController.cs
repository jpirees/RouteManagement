using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoutesManagement.Models;
using RoutesManagement.Services;

namespace RoutesManagement.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var people = await PeopleService.Get();

            return View(people);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var person = await PeopleService.Get(id);

            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, IsAvailable")] PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                var response = await PeopleService.Create(person);

                switch ((int)response.StatusCode)
                {
                    case 201:
                        TempData["personSuccess"] = "Pessoa adicionada com sucesso!";
                        return View(person);

                    default:
                        TempData["registerError"] = "Falha ao tentar adicionar uma pessoa!";
                        return View(person);
                }
            }

            TempData["registerError"] = "Falha ao tentar adicionar uma pessoa!";
            return View(person);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var person = await PeopleService.Get(id);

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,IsAvailable,Team")] PersonViewModel person)
        {
            if (!person.IsAvailable)
            {
                var personTeam = await PeopleService.Get(person.Id);
                person.Team = personTeam.Team;
            }

            var response = await PeopleService.Update(id, person);

            switch ((int)response.StatusCode)
            {
                case 204:
                    TempData["personSuccessEdit"] = "Os dados foram alterados com sucesso!";
                    return View(person);

                default:
                    TempData["registerErrorEdit"] = "Falha ao tentar editar dados da pessoa!";
                    return View(person);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var response = await PeopleService.Delete(id);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View();
        }
    }
}
