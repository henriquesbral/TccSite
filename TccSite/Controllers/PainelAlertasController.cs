using Microsoft.AspNetCore.Mvc;

namespace TccSite.Controllers
{
    public class PainelAlertasController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
