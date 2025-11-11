using Microsoft.AspNetCore.Mvc;
using TCCAPIESP32.Application.Services;
using TCCAPIESP32.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace TCCAPIESP32.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CameraController : ControllerBase
    {
        private readonly CameraService _cameraService;
        private readonly ImageProcessingService _imageProcessingService;
        private readonly ImagensEsp32Service _imagemEspService;
        private readonly LogImagensEsp32Service _LogImagensEsp32Service;
        private readonly ILogger<CameraController> _logger;

        const string RetornoPositivo = "Sucesso";
        const string RetornoNegativo = "Erro";

        public CameraController(
            CameraService cameraService,
            ImageProcessingService imageProcessingService,
            ImagensEsp32Service metadataService,
            LogImagensEsp32Service logImagensEsp32Service,
            ILogger<CameraController> logger)
        {
            _cameraService = cameraService;
            _imageProcessingService = imageProcessingService;
            _imagemEspService = metadataService;
            _LogImagensEsp32Service = logImagensEsp32Service;
            _logger = logger;
        }

        [HttpPost("Capturar")]
        public async Task<IActionResult> CapturarFoto()
        {
            _logger.LogInformation("Iniciando o processamento da RotinaCapturaImagensAsync");
            var resultado = await RotinaCapturaImagensAsync();
            _logger.LogInformation("Finalizando o processamento da RotinaCapturaImagensAsync");
            return Ok(new { Mensagem = "Rotina Finalizada", Resultado = resultado });
        }

        [HttpGet("ImagensSalvar")]
        public IActionResult ListarImagens()
        {
            var lista = _imagemEspService.Listar();
            return Ok(lista);
        }

        private async Task<Dictionary<string, bool>> RotinaCapturaImagensAsync()
        {
            var retorno = new Dictionary<string, bool>();
            var horaAtual = DateTime.Now;

            //InicioRotina
            var tempoFim = horaAtual.AddHours(1);

            while (DateTime.Now <= tempoFim)
            {
                try
                {
                    _logger.LogInformation("Iniciando o processamento da CapturarImagemAsync");
                    var imagePath = await _cameraService.CapturarImagemAsync();

                    if (imagePath is null)
                    {
                        retorno["Captura"] = false;
                        var log = new LogImagensEsp32
                        {
                            CodEventoImagem = 1,
                            MensagemProcessamentoStatus = "ImagemPath is null",
                            DataLog = DateTime.Now
                        };
                        _LogImagensEsp32Service.SalvarLog(log);
                        break;
                    }

                    var metadata = await _imagemEspService.SalvarImagemAsync(imagePath);
                    retorno["Imagem salva"] = metadata != null;

                    if (metadata != null)
                    {
                        var log = new LogImagensEsp32
                        {
                            CodEventoImagem = metadata.CodEventoImagem,
                            MensagemProcessamentoStatus = $"Imagem salva: {metadata.NomeArquivo}",
                            DataLog = DateTime.Now
                        };

                        _LogImagensEsp32Service.SalvarLog(log);
                    }

                    var resultadoia = await _imageProcessingService.ProcessImageAsync(imagePath);
                    retorno["processamentoia"] = resultadoia != null;
                }
                catch (Exception ex)
                {
                    retorno[ex.Message + DateTime.Now] = false;
                    _logger.LogInformation($"Ocorreu um erro: {ex.Message}");
                    var log = new LogImagensEsp32
                    {
                        CodEventoImagem = 0,
                        MensagemProcessamentoStatus = $"Ocorreu um erro: {ex.Message}",
                        DataLog = DateTime.Now
                    };
                    _LogImagensEsp32Service.SalvarLog(log);
                    break;
                }

            }
            await Task.Delay(5000);

            return retorno;
        }
    }
}
