using Microsoft.AspNetCore.Mvc;

namespace TccSite.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
