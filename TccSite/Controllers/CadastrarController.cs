using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using TccSite.Application.Interfaces;
using TccSite.Application.Services;
using TccSite.Domain.Entities;
using TccSite.Web.ViewModels;

namespace TccSite.Controllers
{
    public class CadastrarController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioSenhaService _usuarioSenhaService;
        private readonly IPessoaCadastroService _pessoaService;

        public CadastrarController(IUsuarioService usuarioService, 
            IUsuarioSenhaService usuarioSenhaService, 
            IPessoaCadastroService pessoaService)
        {
            _usuarioService = usuarioService;
            _usuarioSenhaService = usuarioSenhaService;
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarUsuario([FromForm] UsuarioCadastroViewModel model)
        {
            try
            {
                // =====================
                // VALIDAÇÃO BÁSICA
                // =====================
                if (!ModelState.IsValid)
                    return BadRequest(new { mensagem = "Todos os campos são obrigatórios." });

                // =====================
                // VERIFICA EMAIL JÁ EXISTENTE
                // =====================
                var usuarioExistente = _usuarioService.ObterUsuarioPorEmail(model.Email);
                if (usuarioExistente != null)
                    return BadRequest(new { mensagem = "Email já cadastrado." });

                // =====================
                // CRIA PESSOA
                // =====================
                var pessoa = new PessoaCadastro
                {
                    Nome = model.Nome.Trim(),
                    Sobrenome = model.Sobrenome.Trim(),
                    DataNascimento = model.DataNascimento
                };

                _pessoaService.AdicionarPessoaCadastro(pessoa);

                // =====================
                // CRIA USUARIO
                // =====================
                var usuario = new Usuario
                {
                    CodPessoaCadastro = pessoa.CodPessoaCadastro,
                    Email = model.Email.Trim(),
                    Ativo = true
                };

                _usuarioService.AdicionarUsuario(usuario);

                // =====================
                // CRIA USUARIO SENHA
                // =====================
                var usuarioSenha = new UsuarioSenha
                {
                    CodUsuario = usuario.CodUsuario,
                    
                };

                _usuarioSenhaService.CriarSenhaInicial(usuario.CodUsuario, "teste");

                return Json(new { mensagem = "Cadastro realizado com sucesso!" });
            }
            catch (Exception ex)
            {
                // Log de erro aqui se desejar
                return StatusCode(500, new { mensagem = ex.Message });
            }
        }

    }
}
