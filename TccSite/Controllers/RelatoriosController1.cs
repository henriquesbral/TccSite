using Microsoft.AspNetCore.Mvc;

namespace TccSite.Controllers
{
    public class RelatoriosController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Relatorios()
        {
            return View();
        }
    }
}
