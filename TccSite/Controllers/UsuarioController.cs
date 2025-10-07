using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TccSite.Application.Interfaces;
using TccSite.Web.ViewModels;

namespace TccSite.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _context;

        public UsuarioController(IUsuarioService context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var usuarios = _context.GetUsuarios();
            return View(usuarios);
        }

        [HttpPost]
        public IActionResult EditarUsuario(UsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var usuario = _context.ObterUsuario(vm.CodUsuario);

            if (usuario == null) return NotFound();

            usuario.Email = vm.Email;
            usuario.CodPerfilUsuario = vm.CodPerfilUsuario;
            usuario.Ativo = vm.Ativo;

            usuario.PessoaCadastro.Nome = vm.Nome;
            usuario.PessoaCadastro.Sobrenome = vm.Sobrenome;
            usuario.PessoaCadastro.CPF = vm.CPF;
            usuario.PessoaCadastro.Telefone = vm.Telefone;

            _context.AtualizarUsuario(usuario);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int codUsuario)
        {
            _context.RemoverUsuario(codUsuario);
            return RedirectToAction("Index");
        }
    }

}
