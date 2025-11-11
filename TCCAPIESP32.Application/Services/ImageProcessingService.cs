using System.Diagnostics;
using System.Threading.Tasks;

namespace TCCAPIESP32.Application.Services
{
    public class ImageProcessingService
    {
        public async Task<string> ProcessImageAsync(string imagePath)
        {
            var pythonPath = "python"; // ou caminho completo do python.exe se precisar
            var scriptPath = Path.Combine(AppContext.BaseDirectory, "Python", "process_image.py");

            var psi = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = $"\"{scriptPath}\" \"{imagePath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (!string.IsNullOrWhiteSpace(error))
                return $"Erro ao processar imagem: {error}";

            return $"Resultado IA: {output}";
        }
    }
}
