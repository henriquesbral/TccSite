using Microsoft.AspNetCore.Mvc;
using TccSite.Web.ViewModels;

namespace TccSite.Controllers
{
    public class RelatoriosController : BaseController
    {
        public IActionResult Index()
        {
            var vm = new RelatorioViewModel
            {
                DataInicio = DateTime.Now.AddDays(-7),
                DataFim = DateTime.Now,
                Dados = new List<DadoRelatorio>() // garante que não seja null
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Relatorios(DateTime dataInicio, DateTime dataFim)
        {
            // Aqui você vai buscar os dados do banco via Repository/EF
            var vm = new RelatorioViewModel
            {
                DataInicio = dataInicio,
                DataFim = dataFim,
                Dados = new List<DadoRelatorio>() // se ainda não houver dados
            };

            return View("Index", vm);
        }
    }

}
