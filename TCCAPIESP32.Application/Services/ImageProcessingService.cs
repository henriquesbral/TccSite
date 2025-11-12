using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TCCAPIESP32.Application.Services
{
    public class ImageProcessingService
    {
        private readonly string _pythonExePath;
        private readonly string _pythonScriptsPath;
        private readonly ILogger<ImageProcessingService> _logger;

        public ImageProcessingService(IConfiguration configuration, ILogger<ImageProcessingService> logger)
        {
            _logger = logger;
            _pythonExePath = configuration["AppSettings:PythonExePath"] ?? "python"; // fallback para 'python' no PATH
            _pythonScriptsPath = configuration["AppSettings:PythonScriptsPath"]
                                 ?? Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Python"));

            _logger.LogInformation("Python path configurado: {PythonPath}", _pythonExePath);
            _logger.LogInformation("Pasta de scripts Python: {ScriptsPath}", _pythonScriptsPath);
        }

        public async Task<string> ProcessImageAsync(string imagePath)
        {
            try
            {
                var scriptPath = Path.Combine(_pythonScriptsPath, "processar.py");

                if (!File.Exists(scriptPath))
                {
                    _logger.LogError("Script Python não encontrado em: {ScriptPath}", scriptPath);
                    return $"Erro: Script não encontrado em {scriptPath}";
                }

                _logger.LogInformation("Iniciando processamento Python para imagem: {ImagePath}", imagePath);

                var psi = new ProcessStartInfo
                {
                    FileName = _pythonExePath,
                    Arguments = $"\"{scriptPath}\" \"{imagePath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = _pythonScriptsPath
                };

                using var process = Process.Start(psi);

                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    _logger.LogError("Erro no processamento Python: {Error}", error);
                    return $"Erro ao processar imagem: {error}";
                }

                return output.Trim();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao processar imagem no Python");
                return $"Erro interno: {ex.Message}";
            }
        }
    }
}
