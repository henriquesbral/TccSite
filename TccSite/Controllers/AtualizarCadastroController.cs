using Microsoft.AspNetCore.Mvc;
using TccSite.Application.Interfaces;
using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Web.ViewModels;

namespace TccSite.Controllers
{
    public class AtualizarCadastroController : BaseController
    {
        private readonly DataContext _context;
        private readonly IUsuarioService _usuarioService;
        private readonly IPessoaCadastroService _pessoaService;
        private readonly ICidadeService _cidadeService;
        private readonly string _pastaImagens;

        public AtualizarCadastroController(
            DataContext context,
            IConfiguration configuration,
            IUsuarioService usuarioService,
            IPessoaCadastroService pessoaService,
            ICidadeService cidadeService)
        {
            _context = context;
            _usuarioService = usuarioService;
            _pessoaService = pessoaService;
            _cidadeService = cidadeService;
            _pastaImagens = configuration["Arquivos:ImagensUsuarios"]!;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var codUsuario = ObterCodUsuarioLogado();
            var usuario = _usuarioService.ObterUsuario(codUsuario);
            var pessoa = _pessoaService.Obter(usuario.CodPessoaCadastro);

            // Converte caminho físico para URL
            string imagemUrl = null;
            if (!string.IsNullOrEmpty(pessoa.CaminhoImagemUsuario))
            {
                var nomeArquivo = Path.GetFileName(pessoa.CaminhoImagemUsuario);
                imagemUrl = $"/ImagensUsuarios/{nomeArquivo}";
            }

            var retorno = new UsuarioCadastroViewModel
            {
                CodUsuario = usuario.CodUsuario,
                CodPessoaCadastro = usuario.CodPessoaCadastro,
                Nome = pessoa.Nome,
                Sobrenome = pessoa.Sobrenome,
                CPF = pessoa.CPF,
                Email = usuario.Email,
                Endereco = pessoa.Endereco,
                CEP = pessoa.CEP,
                Telefone = pessoa.Telefone,
                Ativo = usuario.Ativo,
                DataNascimento = pessoa.DataNascimento,
                ImagemUrl = imagemUrl,
                Cidades = new List<Cidade>(),
                Estados = new List<Estado>()
            };

            return View(retorno);
        }


        [HttpPost]
        public IActionResult AtualizarUsuario(UsuarioCadastroViewModel user)
        {
            if (!ModelState.IsValid)
                return View("Index", user);

            // Atualiza pessoa e retorna resultado
            var pessoaAtualizada = AtualizaCadastro(user);

            // Atualiza imagem se houver novo upload
            if (user.ImagemPerfil != null)
            {
                var novaImagemUrl = AtualizaImagem(user.ImagemPerfil);
                if (!string.IsNullOrEmpty(novaImagemUrl))
                {
                    pessoaAtualizada.CaminhoImagemUsuario = novaImagemUrl;
                }
            }

            _pessoaService.AtualizarPessoaCadastro(pessoaAtualizada);

            TempData["msg"] = "Cadastro atualizado com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult BuscarImagemUsuario()
        {
            var codUsuario = ObterCodUsuarioLogado();
            var usuario = _usuarioService.ObterUsuario(codUsuario);
            var pessoa = _pessoaService.Obter(usuario.CodPessoaCadastro);

            return Json(new { imagemUrl = pessoa.CaminhoImagemUsuario });
        }

        [HttpGet]
        public JsonResult ObterEstados()
        {
            var estados = _context.Estado
                .Select(e => new { e.CodEstado, e.NomeEstado })
                .ToList();

            return Json(estados);
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

        // -------------------------
        // MÉTODOS PRIVADOS AUXILIARES
        // -------------------------

        private PessoaCadastro AtualizaCadastro(UsuarioCadastroViewModel vmPessoa)
        {
            var pessoa = _pessoaService.Obter(vmPessoa.CodPessoaCadastro);
            if (pessoa == null)
                throw new Exception("Pessoa não encontrada para atualização.");

            pessoa.Nome = vmPessoa.Nome;
            pessoa.Sobrenome = vmPessoa.Sobrenome;
            pessoa.CPF = vmPessoa.CPF;
            pessoa.Telefone = vmPessoa.Telefone;
            pessoa.Endereco = vmPessoa.Endereco;
            pessoa.CEP = vmPessoa.CEP;
            pessoa.CodCidade = vmPessoa.CodCidade;

            return pessoa;
        }

        private string AtualizaImagem(IFormFile imagem)
        {
            if (imagem == null || imagem.Length == 0)
                return null;

            var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), _pastaImagens);

            if (!Directory.Exists(pastaDestino))
                Directory.CreateDirectory(pastaDestino);

            var extensao = Path.GetExtension(imagem.FileName);
            var nomeArquivo = $"foto_{DateTime.Now:yyyyMMdd_HHmmss}{extensao}";
            var caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

            // Salva o arquivo
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                imagem.CopyTo(stream);
            }

            // Caminho relativo para salvar no banco
            var caminhoRelativo = Path.Combine(_pastaImagens.Replace("wwwroot", ""), nomeArquivo)
                                  .Replace("\\", "/");

            return caminhoRelativo;
        }
    }
}