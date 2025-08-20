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

        public CameraController(
            CameraService cameraService,
            ImageProcessingService imageProcessingService,
            ImagensEsp32Service metadataService)
        {
            _cameraService = cameraService;
            _imageProcessingService = imageProcessingService;
            _imagemEspService = metadataService;
        }

        [HttpPost("capturar")]
        public async Task<IActionResult> CapturarFoto()
        {
            var imagePath = await _cameraService.CapturePhotoAsync();

            if (imagePath is null)
                return BadRequest("Não foi possível capturar a imagem do ESP32-CAM.");

            // Salva os metadados no banco
            var metadata = await _imagemEspService.SalvarImagemAsync(imagePath);

            // Processa a imagem com IA
            var resultadoIA = await _imageProcessingService.ProcessImageAsync(imagePath);

            return Ok(new
            {
                metadata,
                resultadoIA
            });
        }

        [HttpGet("ImagensSalvar")]
        public IActionResult ListarImagens()
        {
            var lista = _imagemEspService.Listar();
            return Ok(lista);
        }
    }
}
