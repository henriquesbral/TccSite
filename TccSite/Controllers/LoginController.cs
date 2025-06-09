using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TccSite.Models.Entities;
using TccSite.Models.Interfaces;

namespace TccSite.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogDeAcessosRepository _logDeAcessosRepository;

        public LoginController(IUsuarioRepository usuarioRepository, ILogDeAcessosRepository logDeAcessosRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _logDeAcessosRepository = logDeAcessosRepository ?? throw new ArgumentNullException(nameof(logDeAcessosRepository));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Autenticar([FromBody] Usuario request)
        {
            var res = new RetornoJson { success = false };

            try
            {
                if (request.Email == null || request.Senha == null)
                {
                    throw new Exception("Usuário ou senha não podem ser vazio.");
                }

                var usuario = _usuarioRepository.ObterAutenticar(request.Email, request.Senha);

                if (usuario == null)
                    res.msg = "Usuario não encontrado, por favor acione a equipe de desenvolvimento.";

                if (request.Email == usuario.Email && request.Senha == usuario.Senha && usuario.Ativo)
                {
                    res.success = true;
                    res.msg = "Acesso validado !";
                }
                else
                {
                    //var log = _logDeAcessosRepository.Obter(usuario.CodUsuario);

                    //if (log != null && log.CodUsuario == usuario.CodUsuario)
                    //{
                    //    log.DataUltimoAcesso = DateTime.Now;
                    //    _logDeAcessosRepository.Update(log);
                    //}
                    //else
                    //{
                    //    var newLog = new LogDeAcessos();

                    //    newLog.CodUsuario = usuario.CodUsuario;
                    //    newLog.DataUltimoAcesso = DateTime.Now;
                    //    _logDeAcessosRepository.Add(newLog);

                    //    res.success = true;
                    //}
                    res.success = true;
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
