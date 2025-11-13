using Microsoft.EntityFrameworkCore;
using TCCAPIESP32.Infrastructure.Data;
using TCCAPIESP32.Domain.Entities;
using TCCAPIESP32.Application.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A ConnectionString não foi configurada no appsettings.json");
}

builder.Host.UseSerilog();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<CameraService>();
builder.Services.AddScoped<ImageProcessingService>();
builder.Services.AddScoped<ImagensEsp32Service>();
builder.Services.AddScoped<LogImagensEsp32Service>();
builder.Services.AddScoped<AlertaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowTccSite", policy =>
        policy.WithOrigins("https://localhost:7031") // Porta do seu projeto MVC
              .AllowAnyHeader()
              .AllowAnyMethod());
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowTccSite");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();