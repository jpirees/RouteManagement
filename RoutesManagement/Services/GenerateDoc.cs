using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using RoutesManagement.Models;

namespace RoutesManagement.Services
{
    public static class GenerateDoc
    {
        public static async Task<string> Write(List<List<string>> routes, List<string> dataOptionsSelected, List<TeamViewModel> teamsSelected, string serviceSelected, CityViewModel citySelected, string rootPath)
        {
            var serviceColumn = routes[0].FindIndex(column => column == "SERVIÇO");
            var cityColumn = routes[0].FindIndex(column => column == "CIDADE");
            var cepColumn = routes[0].FindIndex(column => column == "CEP");

            var routesCount = routes.Count;
            var allColumns = routes[0];

            for (var i = 0; i < routesCount; i++)
            {
                routes.Remove(routes.Find(route => route[cityColumn].ToUpper() != citySelected.Name.ToUpper()));
                routes.Remove(routes.Find(route => route[serviceColumn].ToUpper() != serviceSelected.ToUpper()));
            }

            var division = routes.Count / teamsSelected.Count;
            var restDivision = routes.Count % teamsSelected.Count;

            var indexGeneral = 0;

            var pathFiles = $"{rootPath}//files";

            if (!Directory.Exists(pathFiles))
                Directory.CreateDirectory(pathFiles);

            var filename = $"Rota-{serviceSelected}-{citySelected.Name}.docx";

            var pathFile = $"{pathFiles}//{filename}";

            await using (FileStream fileStream = new(pathFile, FileMode.Create))
            {
                using (StreamWriter sw = new(fileStream, Encoding.UTF8))
                {
                    sw.WriteLine($"{serviceSelected} - {DateTime.Now:dd/MM/yyyy}\n{citySelected.Name}\n\n");

                    foreach (var team in teamsSelected)
                    {
                        sw.WriteLine("Equipe: " + team.Name);
                        sw.WriteLine("--------------------------------------------------------------");
                        sw.WriteLine("Rotas:");
                        sw.WriteLine("--------------------------------------------------------------");

                        for (int i = 0; i < division; i++)
                        {
                            if (i == 0 && restDivision > 0)
                                division++;

                            if (i == 0)
                                restDivision--;

                            foreach (var index in dataOptionsSelected)
                                sw.WriteLine($"{allColumns[int.Parse(index)]}: {routes[i + indexGeneral][int.Parse(index)]}");

                            if ((i + 1) >= division)
                                indexGeneral += 1 + i;

                            sw.WriteLine("\n");
                        }

                        if (restDivision >= 0)
                            division--;

                        sw.WriteLine("--------------------------------------------------------------");
                    }

                    sw.Close();
                }

                fileStream.Close();
            }

            return filename;
        }
    }
}
