using System.Net;

namespace TCCAPIESP32.Services
{
    public class CameraService
    {
        private readonly string _esp32Url;
        private readonly string _pastaImagens;
        private readonly ILogger<CameraService> _logger;

        public CameraService(IConfiguration configuration, ILogger<CameraService> logger)
        {
            _esp32Url = configuration["AppSettings:UrlEsp32Cam"] ?? throw new Exception("UrlEsp32Cam não configurada!");
            _pastaImagens = configuration["AppSettings:PastaImagens"] ?? "wwwroot/images";
            _logger = logger;
        }

        public async Task<string?> CapturePhotoAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando captura de imagem do ESP32 em {Hora}", DateTime.Now);

                using var client = new HttpClient();
                var bytes = await client.GetByteArrayAsync(_esp32Url);

                if (bytes == null || bytes.Length == 0)
                {
                    _logger.LogWarning("A captura da imagem retornou vazia do ESP32 em {Hora}", DateTime.Now);
                    return null;
                }

                var folder = Path.Combine(Directory.GetCurrentDirectory(), _pastaImagens);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                    _logger.LogInformation("Diretório criado: {Folder}", folder);
                }

                var fileName = $"foto_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                var filePath = Path.Combine(folder, fileName);

                await File.WriteAllBytesAsync(filePath, bytes);

                _logger.LogInformation("Imagem salva com sucesso em {FilePath}", filePath);
                return filePath;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao capturar imagem do ESP32");
                return null;
            }
        }
    }
}