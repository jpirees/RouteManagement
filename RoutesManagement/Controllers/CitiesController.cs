using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoutesManagement.Models;
using RoutesManagement.Services;

namespace RoutesManagement.Controllers
{
    [Authorize]
    public class CitiesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var cities = await CitiesService.Get();

            return View(cities);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var city = await CitiesService.Get(id);

            return View(city);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,State")] CityViewModel city)
        {
            if (ModelState.IsValid)
            {
                var cityExist = await CitiesService.GetByName(city.Name.ToUpper());

                if (cityExist.Id != null)
                {
                    TempData["cityFailedCreate"] = "Cidade já cadastrada!";
                    return View(city);
                }

                city.Name = city.Name.ToUpper().Trim();
                city.State = city.State.ToUpper().Trim();

                var response = await CitiesService.Create(city);

                switch ((int)response.StatusCode)
                {
                    case 201:
                        TempData["citySuccess"] = "Cidade adicionada com sucesso!";
                        return View(city);

                    case 400:
                        TempData["cityFailedCreate"] = "Cidade já cadastrada!";
                        return View(city);

                    default:
                        TempData["registerError"] = "Falha ao tentar adicionar uma cidade!";
                        return View(city);
                }
            }

            TempData["registerError"] = "Falha ao tentar adicionar uma cidade!";
            return View(city);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var person = await CitiesService.Get(id);

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,State")] CityViewModel city)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(city.Name) || string.IsNullOrEmpty(city.State))
                    return View(city);

                var response = await CitiesService.Update(id, city);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }

            return View(city);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var response = await CitiesService.Delete(id);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View();
        }
    }
}
