﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RouteManagement.Models;

namespace RouteManagement.Services
{
    public static class GenerateDoc
    {
        public static async Task Write(List<List<string>> routes, List<string> dataOptionsSelected, List<TeamViewModel> teamsSelected, string seviceSelected, CityViewModel citySelected)
        {
            var serviceColumn = routes[0].FindIndex(coluna => coluna == "SERVIÇO");
            var cityColumn = routes[0].FindIndex(coluna => coluna == "CIDADE");
            var cepColumn = routes[0].FindIndex(coluna => coluna == "CEP");

            var routesCount = routes.Count;
            var allColumns = routes[0];

            for (var i = 0; i < routesCount; i++)
            {
                routes.Remove(routes.Find(route => route[cityColumn].ToUpper() != citySelected.Name.ToUpper()));
                routes.Remove(routes.Find(route => route[cityColumn].ToUpper() != citySelected.Name.ToUpper()));
            }

            var division = routes.Count / teamsSelected.Count;
            var restDivision = routes.Count % teamsSelected.Count;

            var indexGeneral = 0;

            await using (StreamWriter sw = new($@"C:\Users\Junior\Desktop\Rota-{seviceSelected}-{DateTime.Now:dd-MM-yyyy}.docx"))
            {
                sw.WriteLine($"{seviceSelected} - {DateTime.Now:dd/MM/yyyy}\n{citySelected.Name}\n\n");

                foreach (var team in teamsSelected)
                {
                    sw.WriteLine("Equipe: " + team.Name + "\nRotas:\n");

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
            }
        }
    }
}
