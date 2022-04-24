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
                await PeopleService.Create(person);

                return RedirectToAction(nameof(Index));
            }

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
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,IsAvailable")] PersonViewModel person)
        {
            if (!id.Equals(person.Id))
                return RedirectToAction(nameof(Index));

            var response = await PeopleService.Update(id, person);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(person);
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
