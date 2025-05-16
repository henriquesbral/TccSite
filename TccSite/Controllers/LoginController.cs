using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;

namespace TccSite.Controllers
{
    public class LoginController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult Logar(string usuario, string senha)
        {
            var res = new RetornoJson { success = false };
            try
            {
                //var autenticado = Autenticar(usuario, senha);

                res.success = true;
            }
            catch (Exception)
            {
                res.msg = msgErroPadrao;
            }

            var json = Json(res);
            json.Value = Int32.MaxValue;

            return json;
        }
    }
}
