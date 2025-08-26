using Microsoft.AspNetCore.Mvc;
using TccSite.Data.Repository;
using TccSite.Models.Interfaces;

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
        public JsonResult BuscarRelatorio(DateTime dataInicio, DateTime dataFim, int tipoAlerta)
        {
            var res = new RetornoJson { success = false };

            try
            {
                var dadosRelatorio = _alertaRepository.BuscarDados(dataInicio, dataFim, tipoAlerta);

                // retorna como JSON para o JS consumir
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
