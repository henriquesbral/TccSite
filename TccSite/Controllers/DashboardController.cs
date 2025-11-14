using Microsoft.AspNetCore.Mvc;
using TccSite.Application.Interfaces;
using TccSite.Domain.Enums;
using TccSite.Web.ViewModels;


namespace TccSite.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAlertaService _alertaService;

        public DashboardController(IAlertaService alertaService)
        {
            _alertaService = alertaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Alerta(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var dados = _alertaService.GerarRelatorioNivelRio(dataInicio, dataFim);
                return Json(new { success = true, data = dados });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }
    }
}
