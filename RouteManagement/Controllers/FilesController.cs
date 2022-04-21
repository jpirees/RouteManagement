using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using OfficeOpenXml;
using RouteManagement.Models;
using RouteManagement.Services;

namespace RouteManagement.Controllers
{
    public class FilesController : Controller
    {
        public static List<List<string>> routes = new();
        public static readonly List<string> headers = new();
        public static string serviceName;
        public static string cityId;

        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult UploadFile()
        {
            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using ExcelPackage package = new(files[0].OpenReadStream());
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                var columnCount = worksheet.Dimension.End.Column;
                var rowCount = worksheet.Dimension.End.Row;

                int columnCep = 0;

                for (var column = 1; column <= columnCount; column++)
                {
                    headers.Add(worksheet.Cells[1, column].Value.ToString());

                    if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("CEP"))
                        columnCep = column - 1;
                }

                worksheet.Cells[2, 1, rowCount, columnCount].Sort(columnCep, false);

                package.Save();

                List<string> columnContent = new();

                for (var row = 2; row < rowCount; row++)
                {
                    for (var column = 2; column < columnCount; column++)
                    {
                        var content = worksheet.Cells[row, column].Value?.ToString() ?? "";
                        columnContent.Add(content.ToString());
                    }

                    routes.Add(columnContent);
                }

                return RedirectToAction(nameof(OperatingCity));
            }

            return RedirectToAction(nameof(Upload));
        }

        public async Task<IActionResult> OperatingCity()
        {
            IEnumerable<CityViewModel> cities = await CitiesService.Get();
            ViewBag.Cities = cities;

            return View();
        }

        [HttpPost]
        public IActionResult GetTeamsByOperatingCity()
        {
            serviceName = Request.Form["serviceName"].ToString();
            cityId = Request.Form["OperatingCity"].ToString();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<TeamViewModel> teams = await TeamsService.GetTeamsByCity(cityId);

            var teamsAvailable =
                (from team in teams
                 where team.IsAvailable == true
                 select team);

            ViewBag.TeamsAvailable = teamsAvailable;
            ViewBag.Headers = headers;

            return View();
        }
















    }
}