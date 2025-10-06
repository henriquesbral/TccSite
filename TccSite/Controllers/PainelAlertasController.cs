using Microsoft.AspNetCore.Mvc;
using TccSite.Data.Repository;
using TccSite.Domain.Interfaces;

namespace TccSite.Controllers
{
    public class PainelAlertasController : BaseController
    {
        private readonly IAlertaRepository _alertaRepository;

        public PainelAlertasController(IAlertaRepository alertaRepository)
        {
            _alertaRepository = alertaRepository ?? throw new ArgumentNullException(nameof(alertaRepository));
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
                var dadosRelatorioAlerta = _alertaRepository.BuscarDados(dataInicio, dataFim, tipoAlerta, tipoRelatorio);

                return Json(dadosRelatorioAlerta);
            }
            catch (Exception ex)
            {
                res.msg = $"Erro ao buscar relatório: {ex.Message}";
                return Json(res);
            }
        }

        [HttpGet]
        public JsonResult BuscarRelatorioImagens(DateTime dataInicio, DateTime dataFim, int tipoAlerta, int tipoRelatorio)
        {
            var res = new RetornoJson { success = false };

            try
            {
                var dadosRelatorio = _alertaRepository.BuscarDados(dataInicio, dataFim, tipoAlerta, tipoRelatorio);

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
