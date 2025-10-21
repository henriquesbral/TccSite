using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TccSite.Application.Interfaces;
using TccSite.Application.DTOs;
using TccSite.Domain.Entities;
using TccSite.Domain.Enums;

namespace TccSite.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogDeAcessosService _logDeAcessosService;
        private readonly IUsuarioSenhaService _usuarioSenhaService;

        public LoginController(IUsuarioService usuarioService, 
            ILogDeAcessosService logDeAcessosService, 
            IUsuarioSenhaService usuarioSenhaService)
        {
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
            _logDeAcessosService = logDeAcessosService ?? throw new ArgumentNullException(nameof(logDeAcessosService));
            _usuarioSenhaService = usuarioSenhaService ?? throw new ArgumentNullException(nameof(usuarioSenhaService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Autenticar([FromBody] LoginDTORequest request)
        {
            var res = new RetornoJson();

            try
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
                    throw new Exception("Usuário ou senha não podem ser vazios.");

                var autenticado = await AutenticarUsuario(request.Email, request.Senha);

                if (autenticado)
                { 
                    res.success = autenticado;
                    res.msg = "Acesso validado!";
                }
                else
                {
                    res.success = autenticado;
                    res.msg = "Usuário não encontrado ou inativo.";
                }
            }
            catch (Exception ex)
            {
                res.msg = $"{msgErroPadrao}: {ex.Message}";
            }

            return Json(res);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        private async Task<bool> AutenticarUsuario(string email, string senha)
        {
            bool autenticado;

            var user = _usuarioService.ObterAutenticar(email);
            var validarSenha = _usuarioSenhaService.ValidarSenha(user.CodUsuario, senha);

            if (user != null && user.Ativo && validarSenha)
            {
                // Atualiza ou cria log de acesso
                var log = _logDeAcessosService.Obter(user.CodUsuario);
                if (log != null)
                {
                    log.DataUltimoAcesso = DateTime.Now;
                    _logDeAcessosService.Update(log);
                }
                else
                {
                    _logDeAcessosService.Add(new LogDeAcessos
                    {
                        CodUsuario = user.CodUsuario,
                        DataUltimoAcesso = DateTime.Now
                    });
                }

                // Converte o codigo do perfil para o Enum correspondente
                var perfilEnum = (PefilUsuarioEnum)user.CodPerfilUsuario;

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, perfilEnum.ToString()), // "Administrador", "Usuario" ou "PrimeiroAcesso"
                        new Claim("CodUsuario", user.CodUsuario.ToString())
                    };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Autentica o usuario no sistema
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                autenticado = true;
            }
            else
            {
                autenticado = false;
            }

            return autenticado;
        }
    }
}
