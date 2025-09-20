using Microsoft.AspNetCore.Mvc;
using TCCAPIESP32.Services;
using TCCAPIESP32.Models;

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
            _logger.LogInformation("Iniciando o processamento da RotinaImagens", DateTime.Now);
            var resultado = await RotinaImagens();
            return Ok(new { Mensagem = "Rotina Finalizada", Resultado = resultado });
        }

        [HttpGet("ImagensSalvar")]
        public IActionResult ListarImagens()
        {
            var lista = _imagemEspService.Listar();
            return Ok(lista);
        }

        private async Task<Dictionary<string, bool>> RotinaImagens()
        {
            var retorno = new Dictionary<string, bool>();
            var horaAtual = DateTime.Now;

            //InicioRotina
            var tempoFim = horaAtual.AddHours(1);

            while (DateTime.Now <= tempoFim)
            {
                try
                {
                    var imagePath = await _cameraService.CapturePhotoAsync();

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

                    //var resultadoIA = await _imageProcessingService.ProcessImageAsync(imagePath);
                    //retorno["ProcessamentoIA"] = resultadoIA != null;
                }
                catch (Exception ex)
                {
                    retorno[ex.Message + DateTime.Now] = false;
                    var log = new LogImagensEsp32
                    {
                        CodEventoImagem = 0,
                        MensagemProcessamentoStatus = ex.Message,
                        DataLog = DateTime.Now
                    };
                    _LogImagensEsp32Service.SalvarLog(log);
                    break;
                }

                await Task.Delay(60000);
            }

            return retorno;
        }
    }
}
