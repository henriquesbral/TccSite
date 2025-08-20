using System.Net;

namespace TccEsp32CamAPI.Services
{
    public class CameraService
    {
        private readonly string _esp32Url = "http://192.168.0.120/capture"; // IP do ESP32-CAM

        public async Task<string?> CapturePhotoAsync()
        {
            try
            {
                using var client = new HttpClient();
                var bytes = await client.GetByteArrayAsync(_esp32Url);

                if (bytes == null || bytes.Length == 0)
                    return null;

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
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
