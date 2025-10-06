using Microsoft.AspNetCore.Mvc;
using TccSite.Data.Context;
using TccSite.Domain.ViewModels;

namespace TccSite.Controllers
{
    public class AtualizarCadastroController : BaseController
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public AtualizarCadastroController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index(int codUsuario)
        {
            var usuario = _context.Usuario
                .Where(u => u.CodUsuario == codUsuario)
                .Select(u => new EditarCadastroViewModel
                {
                    CodUsuario = u.CodUsuario,
                    CodPessoaCadastro = u.CodPessoaCadastro,
                    Nome = u.PessoaCadastro.Nome,
                    Sobrenome = u.PessoaCadastro.Sobrenome,
                    CPF = u.PessoaCadastro.CPF,
                    Telefone = u.PessoaCadastro.Telefone,
                    Endereco = u.PessoaCadastro.Endereco,
                    CEP = u.PessoaCadastro.CEP,
                    CodCidade = u.PessoaCadastro.CodCidade,
                    Email = u.Email,
                    ImagemUrl = "/Assets/imagens/user.jpeg" // ou campo se tiver imagem salva
                }).FirstOrDefault();

            if (usuario == null) return NotFound();

            usuario.Estados = _context.Estado.ToList();
            //usuario.Cidades = _context.Cidade
            //    .Where(c => c.CodEstado == _context.Cidade.FirstOrDefault(x => x.CodCidade == usuario.CodCidade)?.CodEstado)
            //    .ToList();

            usuario.CodEstadoSelecionado = usuario.Cidades.FirstOrDefault()?.CodEstado ?? 0;

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Index(EditarCadastroViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var usuario = _context.Usuario.Find(vm.CodUsuario);
            if (usuario == null) return NotFound();

            var pessoa = _context.PessoaCadastro.Find(vm.CodPessoaCadastro);
            if (pessoa == null) return NotFound();

            // Atualiza PessoaCadastro
            pessoa.Nome = vm.Nome;
            pessoa.Sobrenome = vm.Sobrenome;
            pessoa.CPF = vm.CPF;
            pessoa.Telefone = vm.Telefone;
            pessoa.Endereco = vm.Endereco;
            pessoa.CEP = vm.CEP;
            pessoa.CodCidade = vm.CodCidade;

            // Atualiza Usuario
            usuario.Email = vm.Email;

            // Upload da imagem
            if (vm.ImagemPerfil != null)
            {
                var pasta = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(vm.ImagemPerfil.FileName)}";
                var caminho = Path.Combine(pasta, nomeArquivo);

                using (var stream = new FileStream(caminho, FileMode.Create))
                {
                    vm.ImagemPerfil.CopyTo(stream);
                }

                // Salvar caminho da imagem (pode criar campo na PessoaCadastro ou Usuario)
                // Exemplo: pessoa.ImagemUrl = $"/uploads/{nomeArquivo}";
            }

            _context.SaveChanges();

            TempData["msg"] = "Cadastro atualizado com sucesso!";
            return RedirectToAction("Index", new { codUsuario = vm.CodUsuario });
        }

        [HttpGet]
        public JsonResult ObterCidades(int codEstado)
        {
            var cidades = _context.Cidade
                .Where(c => c.CodEstado == codEstado)
                .Select(c => new { c.CodCidade, c.NomeCidade })
                .ToList();

            return Json(cidades);
        }

    }
}
