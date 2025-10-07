using Microsoft.AspNetCore.Mvc;

namespace TccSite.Controllers
{
    public class BaseController : Controller
    {
        protected string msgErroPadrao = "Ocorreu um erro interno. Por gentileza acione o desenvolvedor do projeto !";

        public struct RetornoJson
        {
            public bool success { get; set; }
            public string msg { get; set; }
        }
    }
}
