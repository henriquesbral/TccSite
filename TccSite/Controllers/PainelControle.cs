using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using TccSite.Application.Interfaces;
using TccSite.Controllers;
using TccSite.Web.Models.ViewModels;
using TccSite.Web.ViewModels;

namespace TccSite.Web.Controllers
{
    public class PainelControle : BaseController
    {
        private readonly IImagensEsp32Service _imagensEsp32Service;
        private readonly IWebHostEnvironment _env;

        public PainelControle(IImagensEsp32Service imagensEsp32Service, IWebHostEnvironment env)
        {
            _imagensEsp32Service = imagensEsp32Service;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult UltimaImagem()
        {
            var imagens = _imagensEsp32Service.GetImagens();

            if (imagens == null || !imagens.Any())
                return Json(new { sucesso = false, mensagem = "Nenhuma imagem encontrada." });

            var ultima = imagens.OrderByDescending(i => i.DataEnvio).First();
            var caminhoOriginal = ultima.CaminhoArquivo;

            if (!System.IO.File.Exists(caminhoOriginal))
                return Json(new { sucesso = false, mensagem = "Arquivo de imagem não encontrado." });

            // Pasta pública (wwwroot/Imagens)
            var pastaPublica = Path.Combine(_env.WebRootPath, "Imagens");

            if (!Directory.Exists(pastaPublica))
                Directory.CreateDirectory(pastaPublica);

            LimparImagensAntigas(pastaPublica);

            var nomeArquivo = Path.GetFileName(caminhoOriginal);
            var destino = Path.Combine(pastaPublica, nomeArquivo);
            System.IO.File.Copy(caminhoOriginal, destino, true);
            var urlImagem = Url.Content($"~/Imagens/{nomeArquivo}");

            var viewModel = new PainelControleViewModel
            {
                NomeArquivo = nomeArquivo,
                CaminhoAbsoluto = caminhoOriginal,
                UrlImagem = urlImagem,
                DataCadastro = ultima.DataEnvio 
            };

            return Json(new { sucesso = true, imagem = viewModel });
        }

        private void LimparImagensAntigas(string pasta, int max = 5, int remover = 3)
        {
            var arquivos = Directory.GetFiles(pasta)
                                    .OrderBy(f => new FileInfo(f).CreationTime)
                                    .ToList();

            if (arquivos.Count <= max) return;

            foreach (var arquivo in arquivos.Take(remover))
            {
                try { System.IO.File.Delete(arquivo); }
                catch (Exception ex) { Console.WriteLine($"Erro ao deletar {arquivo}: {ex.Message}"); }
            }
        }

    }
}