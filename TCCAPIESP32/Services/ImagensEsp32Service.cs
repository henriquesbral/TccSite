using Microsoft.Extensions.Configuration;
using TCCAPIESP32.Data;
using TCCAPIESP32.Models;

namespace TCCAPIESP32.Services
{
    public class ImagensEsp32Service
    {
        private readonly AppDbContext _context;
        private readonly string _pastaImagens;
        private readonly string _prefixoArquivo;

        public ImagensEsp32Service(AppDbContext context, IConfiguration configuration)
        {
            _context = context;

            // Lê direto do appsettings.json
            _pastaImagens = configuration["AppSettings:PastaImagens"] ?? "wwwroot/images";
            _prefixoArquivo = configuration["AppSettings:PrefixoArquivo"] ?? "foto_";
        }

        public async Task<ImagensEsp32> SalvarImagemAsync(string filePath)
        {
            var nomeArquivo = Path.GetFileName(filePath);

            var imagem = new ImagensEsp32
            {
                NomeArquivo = nomeArquivo,
                CaminhoArquivo = Path.Combine(_pastaImagens, nomeArquivo),
                DataEnvio = DateTime.Now
            };

            _context.ImagensEsp32.Add(imagem);
            await _context.SaveChangesAsync();

            return imagem;
        }

        public IEnumerable<ImagensEsp32> Listar()
        {
            return _context.ImagensEsp32.ToList();
        }
    }
}
