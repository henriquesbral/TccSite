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
            return View();
        }

        [HttpGet]
        public IActionResult BuscarRelatorio(DateTime dataInicio, DateTime dataFim, int tipoAlerta)
        {
            var res = new RetornoJson { success = false };

            try
            {
                var dadosRelatorio = _alertaRepository.BuscarDados(dataInicio, dataFim, tipoAlerta);

                return View(dadosRelatorio);
            }
            catch (Exception ex)
            {
                res.msg = ($"{msgErroPadrao}: {ex.Message}");
                return Json(res);
            }
        }
    }
}
