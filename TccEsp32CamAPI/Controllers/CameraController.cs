using Microsoft.AspNetCore.Mvc;

namespace TccEsp32CamAPI.Controllers
{
    public class CameraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
