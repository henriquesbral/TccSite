using System.Net;

namespace TCCAPIESP32.Services
{
    public class CameraService
    {
        private readonly string _esp32Url;
        private readonly string _pastaImagens;

        public CameraService(IConfiguration configuration)
        {
            _esp32Url = configuration["AppSettings:UrlEsp32Cam"] ?? throw new Exception("UrlEsp32Cam não configurada!");

            _pastaImagens = configuration["AppSettings:PastaImagens"] ?? "wwwroot/images";
        }

        public async Task<string?> CapturePhotoAsync()
        {
            try
            {
                using var client = new HttpClient();
                var bytes = await client.GetByteArrayAsync(_esp32Url);

                if (bytes == null || bytes.Length == 0)
                    return null;

                var folder = Path.Combine(Directory.GetCurrentDirectory(), _pastaImagens);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = $"foto_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                var filePath = Path.Combine(folder, fileName);

                await File.WriteAllBytesAsync(filePath, bytes);

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao capturar imagem: {ex.Message}");
                return null;
            }
        }
    }
}
