using Microsoft.AspNetCore.Mvc;
using TccSite.Application.DTOs;
using TccSite.Application.Interfaces;
using TccSite.Controllers;

namespace TccSite.Web.Controllers
{
    public class ResetSenhaController : BaseController
    {
        private readonly IResetSenhaService _resetSenhaService;

        public ResetSenhaController(IResetSenhaService resetSenhaService)
        {
            _resetSenhaService = resetSenhaService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarResetSenha([FromBody] ResetSenhaDTORequest request)
        {
            var res = new RetornoJson();

            try
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NovaSenha) || string.IsNullOrEmpty(request.ConfirmacaoSenha))
                    throw new Exception("Informações não podem ser vazios.");

                _resetSenhaService.SalvarNovaSolicitacao(request.Email, request.NovaSenha, false);

                res.success = true;
                res.msg = "Solicitação salva com sucesso !";
            }
            catch (Exception ex)
            {
                res.success = false;
                res.msg = $"{msgErroPadrao}: {ex.Message}";
            }

            return Json(res);
        }
    }
}
