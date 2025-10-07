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
            var alertas = _alertaService.BuscarAlertas().Where(alerta => alerta.Ativo == "1").OrderByDescending(x => x.DataCadastro).ToList();

            return View(alertas);
        }

        [HttpGet]
        public JsonResult Alerta()
        {
            var dados = _alertaService.BuscarAlertas();

            var data = dados
                .Select(x => new AlertaGraficoViewModel
                {
                    CodAlerta = x.CodAlerta,
                    DataCadastro = x.DataCadastro,
                    NivelRio = x.NivelRio,
                    StatusAlerta = ((StatusAlertaEnum)x.CodStatusAlerta).ToString()
                })
                .ToList();

            return Json(data);
        }
    }
}
