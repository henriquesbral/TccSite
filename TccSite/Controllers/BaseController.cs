using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TccSite.Controllers
{
    public abstract class BaseController : Controller
    {
        // Mensagem padrão de erro
        protected readonly string msgErroPadrao = "Ocorreu um erro interno. Por gentileza, acione o desenvolvedor do projeto!";

        /// <summary>
        /// Retorna o ID (CodUsuario) do usuário autenticado no sistema.
        /// </summary>
        /// <returns>O código do usuário logado, ou 0 se não autenticado.</returns>
        protected int ObterCodUsuarioLogado()
        {
            if (User?.Identity?.IsAuthenticated != true)
                return 0;

            var claim = User.FindFirst(ClaimTypes.Sid);
            if (claim == null)
                return 0;

            // tenta converter o valor da claim em int
            if (int.TryParse(claim.Value, out int codUsuario))
                return codUsuario;

            return 0;
        }

        /// <summary>
        /// Retorna o nome do usuário logado (usado em layouts, logs, etc).
        /// </summary>
        protected string ObterNomeUsuario()
        {
            if (User?.Identity?.IsAuthenticated != true)
                return "Visitante";

            return User.Identity.Name ?? "Usuário";
        }

        /// <summary>
        /// Retorna um objeto padrão de erro para respostas JSON.
        /// </summary>
        protected JsonResult RetornarErroPadrao()
        {
            return Json(new RetornoJson
            {
                success = false,
                msg = msgErroPadrao
            });
        }

        /// <summary>
        /// Estrutura padrão de retorno JSON.
        /// </summary>
        public struct RetornoJson
        {
            public bool success { get; set; }
            public string msg { get; set; }
        }
    }
}
