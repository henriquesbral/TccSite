using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace TCCAPIESP32.Application.Services
{
    public class CameraService
    {
        private readonly string _esp32BaseUrl;
        private readonly string _captureEndpoint;
        private readonly string _controlEndpoint;
        private readonly string _pastaImagens;
        private readonly int _defaultFrameSize;
        private readonly ILogger<CameraService> _logger;

        public CameraService(IConfiguration configuration, ILogger<CameraService> logger)
        {
            _esp32BaseUrl = configuration["AppSettings:Esp32CamBaseUrl"]!;
            _captureEndpoint = configuration["AppSettings:Esp32CaptureEndpoint"]!;
            _controlEndpoint = configuration["AppSettings:Esp32ControlEndpoint"]!;
            _defaultFrameSize = int.Parse(configuration["AppSettings:DefaultFrameSize"]!);
            _pastaImagens = configuration["AppSettings:PastaImagens"]!;
            _logger = logger;
        }

        private async Task<bool> SetResolutionAsync(HttpClient client)
        {
            var url = $"{_esp32BaseUrl}{_controlEndpoint}?var=framesize&val={_defaultFrameSize}";
            var resp = await client.GetAsync(url);

            if (!resp.IsSuccessStatusCode)
            {
                _logger.LogError("Falha ao configurar resolução para {FrameSize}. Status: {StatusCode}", _defaultFrameSize, resp.StatusCode);
                return false;
            }

            _logger.LogInformation("Resolução configurada para UXGA (1600x1200).");
            return true;
        }

        public async Task<string?> CapturarImagemAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando captura de imagem do ESP32 em {Hora}", DateTime.Now);

                using var client = new HttpClient();

                // Configura resolução
                if (!await SetResolutionAsync(client))
                    return null;

                // Captura imagem
                var resp = await client.GetAsync($"{_esp32BaseUrl}{_captureEndpoint}");
                if (!resp.IsSuccessStatusCode)
                {
                    _logger.LogError("Falha ao capturar imagem. Status: {StatusCode}", resp.StatusCode);
                    return null;
                }

                var bytes = await resp.Content.ReadAsByteArrayAsync();

                if (bytes == null || bytes.Length == 0)
                {
                    _logger.LogWarning("A captura da imagem retornou vazia.");
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
