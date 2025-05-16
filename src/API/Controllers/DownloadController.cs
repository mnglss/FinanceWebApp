using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entities;
using FinanceWebApp.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    public class DownloadController(IMovementService movementService) : BaseApiController
    {
        [HttpPost("Excel")]
        public async Task<FileResult> Excel([FromBody, Required] MovementByUserIdRequest request)
        {
            List<Movement> movementlist = [];
            var result = await movementService.GetByUserIdAsync(request);
            if (result!.IsFailure)
                movementlist.Add(new Movement { Year = DateTime.Now.Year, Month = DateTime.Now.Month, Date = DateOnly.FromDateTime(DateTime.Now), Amount = 0, Description = "Nessun Risultato" });
            else
                movementlist = result.Value;
            var file = ExcelService.CreateFile(movementlist);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ListaMovimenti.xlsx");
        }

        [HttpPost("Pdf")]
        public async Task<FileResult> Pdf([FromBody, Required] MovementByUserIdRequest request)
        {
            List<Movement> movementlist = [];
            var result = await movementService.GetByUserIdAsync(request);
            if (result!.IsFailure)
                movementlist.Add(new Movement { Year = DateTime.Now.Year, Month = DateTime.Now.Month, Date = DateOnly.FromDateTime(DateTime.Now), Amount = 0, Description = "Nessun Risultato" });
            else
                movementlist = result.Value;
            var pdf = PdfService.MovementPdf(movementlist);

            MemoryStream stream = new MemoryStream();
            pdf.Save(stream);

            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-length", stream.Length.ToString());
            byte[] file = stream.ToArray();
            stream.Close();

            return File(file, "application/pdf", "Lista_Movimenti.pdf");
        }
    }
}
