using Microsoft.EntityFrameworkCore;
using TCCAPIESP32.Data;
using TCCAPIESP32.Models;
using TCCAPIESP32.Services;
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
