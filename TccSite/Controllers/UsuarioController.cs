using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TccSite.Application.Interfaces;
using TccSite.Domain.Entities;
using TccSite.Domain.Enums;
using TccSite.Web.ViewModels;

namespace TccSite.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ICidadeService _cidadeService;
        private readonly IEstadoService _estadoService;

        public UsuarioController(IUsuarioService usuarioService, 
            ICidadeService cidadeService, 
            IEstadoService estadoService )
        {
            _usuarioService = usuarioService;
            _cidadeService = cidadeService;
            _estadoService = estadoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var usuariosCadastrados = _usuarioService.GetUsuarios();

            var usuarios = new List<UsuarioViewModel>();

            foreach (var u in usuariosCadastrados)
            {
                usuarios.Add(new UsuarioViewModel
                    {
                        CodUsuario = u.CodUsuario,
                        //Nome = $"{u.Nome} {u.SobreNome}",

                    }
                );
            }

            return View(usuarios);
        }

        [HttpPost]
        public IActionResult EditarUsuario(UsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var usuario = _usuarioService.ObterUsuario(vm.CodUsuario);

            if (usuario is null)
            {
                return Json(new {success = false, message = "Usuario não identificado." });
            }

            usuario.Email = vm.Email;
            usuario.CodPerfilUsuario = vm.CodPerfilUsuario;
            usuario.Ativo = vm.Ativo;

            usuario.PessoaCadastro.Nome = vm.Nome;
            usuario.PessoaCadastro.Sobrenome = vm.Sobrenome;
            usuario.PessoaCadastro.CPF = vm.CPF;
            usuario.PessoaCadastro.Telefone = vm.Telefone;

            _usuarioService.AtualizarUsuario(usuario);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int codUsuario)
        {
            _usuarioService.RemoverUsuario(codUsuario);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Criar (UsuarioViewModel usuario)
        {
            try
            {
                var validar = _usuarioService.ObterUsuarioPorCPF(usuario.CPF);

                if (validar is not null)
                {
                    return Json(new { success = false, message = "O CPF informado ja possui cadastro na base de dados !" });
                }

                var pessoa = Adicionar(usuario);
                var usuarioNovo = new Usuario()
                {
                    Email = usuario.Email,
                    DataCadastro = DateTime.Now,
                    Ativo = true,
                    CodPerfilUsuario = (int)PefilUsuarioEnum.PrimeiroAcesso
                };

                _usuarioService.AdicionarUsuario(usuarioNovo);
                return Json(new {success = true, message = "Usuario cadastrado com sucesso !" });
            }
            catch (Exception ex) 
            {
                return Json(new {success = false, message = $"Ocorreu um erro ao criar o usuário: {ex.Message}" });
            }
        }

        private PessoaCadastro Adicionar(UsuarioViewModel pessoaAdd)
        {
            var cidade = _cidadeService.GetCidadePorId(pessoaAdd.CodCidade);

            var pessoa = new PessoaCadastro();
            pessoa.Nome = pessoaAdd.Nome;
            pessoa.Sobrenome = pessoaAdd.Sobrenome;
            pessoa.CPF = pessoaAdd.CPF;
            pessoa.Telefone = pessoaAdd.Telefone;
            pessoa.CEP = pessoaAdd.CEP;
            pessoa.CodCidade = pessoaAdd.CodCidade;

            return pessoa;
        }
    }

}
