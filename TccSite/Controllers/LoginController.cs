using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TccSite.Entities;

namespace TccSite.Controllers
{
    public class LoginController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Logar([FromBody] Login request)
        {
            var res = new RetornoJson { success = false };

            try
            {
                if (request.Usuario == "admin@gmail.com" && request.Senha == "teste")
                {
                    res.success = true;
                }
                else
                {
                    res.msg = "Usuário ou senha inválidos.";
                }
            }
            catch (Exception)
            {
                res.msg = msgErroPadrao;
            }

            return Json(res); 
        }
    }
}
