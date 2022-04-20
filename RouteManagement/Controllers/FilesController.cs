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
        [HttpPost]
        public IActionResult Index()
        {
            string[,] routes = null;
            List<string> headers = new();

            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (ExcelPackage package = new(files[0].OpenReadStream()))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    var columnCount = worksheet.Dimension.End.Column;
                    var rowCount = worksheet.Dimension.End.Row;

                    routes = new string[rowCount, columnCount];

                    for (var row = 1; row < rowCount; row++)
                        for (var column = 1; column < columnCount; column++)
                            routes[row, column] = worksheet.Cells[row, column].Value?.ToString() ?? "";

                    for (var column = 1; column < columnCount; column++)
                        headers.Add(worksheet.Cells[1, column].Value?.ToString());

                    ViewBag.Headers = headers;
                    ViewBag.Data = routes;

                    return View();
                }
            }

            return RedirectToAction(nameof(Upload));
        }

        public IActionResult Upload()
        {
            return View();
        }
    }
}