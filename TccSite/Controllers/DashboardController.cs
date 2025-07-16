using Microsoft.AspNetCore.Mvc;
using TccSite.Models.Interfaces;

namespace TccSite.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAlertaRepository _alertaRepository;

        public DashboardController(IAlertaRepository alertaRepository)
        {
            _alertaRepository = alertaRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Alerta()
        {
            var dados = _alertaRepository.BuscarAlertas();

            var data = dados;

            return Json(data);
        }
    }
}
