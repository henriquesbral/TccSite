using Microsoft.AspNetCore.Mvc;
using TccSite.Models.Interfaces;
using TccSite.Models.Entities;
using TccSite.Models.ViewModels;
using TccSite.Models.Enums;

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
            var alertas = _alertaRepository.BuscarAlertas().Where(alerta => alerta.Ativo == "1").OrderByDescending(x => x.DataCadastro).ToList();

            return View(alertas);
        }

        [HttpGet]
        public JsonResult Alerta()
        {
            var dados = _alertaRepository.BuscarAlertas();

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
