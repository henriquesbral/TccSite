using Microsoft.AspNetCore.Mvc;
using TccSite.Application.Interfaces;
using TccSite.Domain.Interfaces;

namespace TccSite.Controllers
{
    public class PainelAlertasController : BaseController
    {
        private readonly IAlertaService _alertaService;

        public PainelAlertasController(IAlertaService alertaService)
        {
            _alertaService = alertaService ?? throw new ArgumentNullException(nameof(alertaService));
        }
        public IActionResult Index()
        {
            //var relatorios =;
            return View();//return View(relatorios);
        }

        [HttpGet]
        public JsonResult BuscarRelatorioAlerta(DateTime dataInicio, DateTime dataFim, int tipoAlerta, int tipoRelatorio)
        {
            var res = new RetornoJson { success = false };

            try
            {
                var dadosRelatorioAlerta = _alertaService.GerarRelatorio(dataInicio, dataFim);

                return Json(dadosRelatorioAlerta);
            }
            catch (Exception ex)
            {
                res.msg = $"Erro ao buscar relatório: {ex.Message}";
                return Json(res);
            }
        }

        [HttpGet]
        public JsonResult BuscarRelatorioImagens(DateTime dataInicio, DateTime dataFim)
        {
            var res = new RetornoJson { success = false };

            try
            {
                var dadosRelatorio = _alertaService.GerarRelatorio(dataInicio, dataFim);

                return Json(dadosRelatorio);
            }
            catch (Exception ex)
            {
                res.msg = $"Erro ao buscar relatório: {ex.Message}";
                return Json(res);
            }
        }
    }
}
