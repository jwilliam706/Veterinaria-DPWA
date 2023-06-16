using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Veterinaria.Utils;

namespace Veterinaria.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReporteCitas()
        {
            return View();
        }
        public IActionResult GenerarReporteCita(string fecha)
        {

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:7264/");

            var response = cliente.GetAsync("/getReportCitas/"+fecha);
            var json_response = response.Result.Content.ReadAsStringAsync().Result.ToString();
            var reportReponse = JsonConvert.DeserializeObject<ReportResponse>(json_response);

            //devolever archivo pdf como resultado de archivo
            byte[] fileBytes = Convert.FromBase64String(reportReponse.ContentReport);
            return File(fileBytes, "application/pdf", "ReporteCitas.pdf");


        }
        [HttpGet]
        [Route("Reportes/GenerarExpediente/{idMascota}")]
        public IActionResult GenerarReporteExpediente(int idMascota)
        {

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:7264/");

            var response = cliente.GetAsync("/getReportExp/" + idMascota);
            var json_response = response.Result.Content.ReadAsStringAsync().Result.ToString();
            var reportReponse = JsonConvert.DeserializeObject<ReportResponse>(json_response);

            //devolever archivo pdf como resultado de archivo
            byte[] fileBytes = Convert.FromBase64String(reportReponse.ContentReport);
            return File(fileBytes, "application/pdf", "ExpedienteMascota.pdf");



        }
    }
}
