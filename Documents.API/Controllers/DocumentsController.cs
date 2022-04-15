using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;

namespace Documents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            List<Document> data = new();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                var columnCount = worksheet.Dimension.End.Column;

                var rowCount = worksheet.Dimension.End.Row;

                for (var row = 2; row <= rowCount; row++)
                {
                    DateTime? date = null, schedule = null;

                    var dateValid = DateTime.TryParse(worksheet.Cells[row, 2].Value?.ToString(), out var dateParse);
                    var scheduleValid = DateTime.TryParse(worksheet.Cells[row, 21].Value?.ToString(), out var scheduleParse);

                    if (dateValid)
                        date = dateParse;

                    if (scheduleValid)
                        schedule = scheduleParse;

                    Document dado = new();

                    dado.Data = date;
                    dado.Status = worksheet.Cells[row, 2].Value?.ToString();
                    dado.Auditado = worksheet.Cells[row, 3].Value?.ToString();
                    dado.CopReverteu = worksheet.Cells[row, 4].Value?.ToString();
                    dado.Log = worksheet.Cells[row, 5].Value?.ToString();
                    dado.Pdf = worksheet.Cells[row, 6].Value?.ToString();
                    dado.Foto = worksheet.Cells[row, 7].Value?.ToString();
                    dado.Contrato = worksheet.Cells[row, 8].Value?.ToString();
                    dado.Wo = worksheet.Cells[row, 9].Value?.ToString();
                    dado.Os = worksheet.Cells[row, 10].Value?.ToString();
                    dado.Assinante = worksheet.Cells[row, 11].Value?.ToString();
                    dado.Tecnicos = worksheet.Cells[row, 12].Value?.ToString();
                    dado.Login = worksheet.Cells[row, 13].Value?.ToString();
                    dado.Matricula = worksheet.Cells[row, 14].Value?.ToString();
                    dado.Cop = worksheet.Cells[row, 15].Value?.ToString();
                    dado.UltimoAlterar = worksheet.Cells[row, 16].Value?.ToString();
                    dado.Local = worksheet.Cells[row, 17].Value?.ToString();
                    dado.PontoCasaApt = worksheet.Cells[row, 18].Value?.ToString();
                    dado.Cidade = worksheet.Cells[row, 19].Value?.ToString();
                    dado.Base = worksheet.Cells[row, 20].Value?.ToString();
                    dado.Horario = schedule;
                    dado.Segmento = worksheet.Cells[row, 22].Value?.ToString();
                    dado.Servico = worksheet.Cells[row, 23].Value?.ToString();
                    dado.TipoDeServico = worksheet.Cells[row, 24].Value?.ToString();
                    dado.TipoOs = worksheet.Cells[row, 25].Value?.ToString();
                    dado.GrupoDeServico = worksheet.Cells[row, 26].Value?.ToString();
                    dado.Endereco = worksheet.Cells[row, 27].Value?.ToString();
                    dado.Numero = worksheet.Cells[row, 28].Value?.ToString();
                    dado.Complemento = worksheet.Cells[row, 29].Value?.ToString();
                    dado.Cep = worksheet.Cells[row, 30].Value?.ToString();
                    dado.Node = worksheet.Cells[row, 31].Value?.ToString();
                    dado.Bairro = worksheet.Cells[row, 32].Value?.ToString();
                    dado.Pacote = worksheet.Cells[row, 33].Value?.ToString();
                    dado.Cod = worksheet.Cells[row, 34].Value?.ToString();
                    dado.Telefone1 = worksheet.Cells[row, 35].Value?.ToString();
                    dado.Telefone2 = worksheet.Cells[row, 36].Value?.ToString();
                    dado.Obs = worksheet.Cells[row, 37].Value?.ToString();
                    dado.ObsTecnico = worksheet.Cells[row, 38].Value?.ToString();
                    dado.Equipamento = worksheet.Cells[row, 39].Value?.ToString();

                    data.Add(dado);
                }

                return Ok(data);
            }
        }
    }
}