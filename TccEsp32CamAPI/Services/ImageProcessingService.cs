using System.Threading.Tasks;

namespace TccEsp32CamAPI.Services
{
    public class ImageProcessingService
    {
        public async Task<string> ProcessImageAsync(string imagePath)
        {
            // Aqui você pode integrar com:
            // - ML.NET
            // - OpenCVSharp
            // - API externa (Azure Vision, OpenAI, etc.)

            await Task.Delay(500); // simula tempo de processamento

            // Exemplo fictício: detectar se a imagem tem água (para projeto IoT)
            return $"Imagem {Path.GetFileName(imagePath)} processada com sucesso. (Exemplo: Detecção concluída)";
        }
    }
}
