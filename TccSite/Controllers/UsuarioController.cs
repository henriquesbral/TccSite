using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TccSite.Domain.Interfaces;
using TccSite.Domain.ViewModels;

namespace TccSite.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _context;

        public UsuarioController(IUsuarioRepository context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.GetUsuariosAsync();
            return View(usuarios);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _context.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
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

            _context.AtualizarUsuarioAsync(usuario);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int codUsuario)
        {
            _context.RemoverUsuarioAsync(codUsuario);
            return RedirectToAction("Index");
        }
    }

}
