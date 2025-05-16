using Microsoft.AspNetCore.Mvc;

namespace TccSite.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
