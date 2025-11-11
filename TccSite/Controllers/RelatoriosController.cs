using Microsoft.AspNetCore.Mvc;
using TccSite.Application.Interfaces;
using TccSite.Web.ViewModels;

namespace TccSite.Controllers
{
    public class RelatoriosController : BaseController
    {
        private readonly IAlertaService _alertaService;

        public RelatoriosController(IAlertaService alertaService)
        {
            _alertaService = alertaService;
        }

        public IActionResult Index()
        {
            var vm = new RelatorioViewModel
            {
                DataInicio = DateTime.Now.AddDays(-7),
                DataFim = DateTime.Now,
                Dados = new List<DadoRelatorio>()
            };
            return View(vm);
        }

        [HttpGet]
        public JsonResult BuscarRelatorioAlertas(DateTime dataInicio, DateTime dataFim, int tipoAlerta)
        {
            try
            {
                var dados = _alertaService.GerarRelatorio(dataInicio, dataFim, tipoAlerta);
                return Json(new { success = true, data = dados });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult BuscarNivelRio(DateTime dataInicio, DateTime dataFim)
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
