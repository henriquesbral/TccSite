using Microsoft.EntityFrameworkCore;
using TccSite.Data.Context;
using TccSite.Data.Repository;
using TccSite.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Service Configuration
// =======================

// Add MVC with views
builder.Services.AddControllersWithViews();

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application repositories (Dependency Injection)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<IStatusAlertaRepository, StatusAlertaRepository>();
builder.Services.AddScoped<IPessoaCadastroRepository, PessoaCadastroRepository>();
builder.Services.AddScoped<ILogDeAcessosRepository, LogDeAcessosRepository>();
builder.Services.AddScoped<IImagemStatusRepository, ImagemStatusRepository>();
builder.Services.AddScoped<IConfiguracoesRepository, ConfiguracoesRepository>();


var app = builder.Build();

// =======================
// Middleware Pipeline
// =======================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enforces HTTPS in production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Define default route pattern
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
