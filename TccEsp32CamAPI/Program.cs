using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoApiEsp32Cam.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços
builder.Services.AddScoped<CameraService>();
builder.Services.AddScoped<ImageProcessingService>();

var app = builder.Build();

app.MapGet("/", () => "API ESP32-CAM .NET 8 rodando 🚀");

// Endpoint para capturar foto e processar
app.MapPost("/capturar", async (CameraService cameraService, ImageProcessingService imgService) =>
{
    var imagePath = await cameraService.CapturePhotoAsync();

    if (imagePath is null)
        return Results.BadRequest("Não foi possível capturar a imagem.");

    var result = await imgService.ProcessImageAsync(imagePath);

    return Results.Ok(new
    {
        imagePath,
        resultadoIA = result
    });
});

app.Run();
