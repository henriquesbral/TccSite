using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.FileProviders;
using System.Globalization;
using TccSite.Application;
using TccSite.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Cultura Global: pt-BR
// =======================
var defaultCulture = new CultureInfo("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture },
    SupportedUICultures = new List<CultureInfo> { defaultCulture }
};

// 👇 Garantir que o Model Binder e toda a thread usem pt-BR
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

// =======================
// Serviços MVC e injeções
// =======================
builder.Services.AddControllersWithViews();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// =======================
// Autenticação e Autorização
// =======================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Página de login
        options.AccessDeniedPath = "/AcessoNegado"; // Página de acesso negado
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdministradorPolicy", policy =>
        policy.RequireRole("Administrador"));

    options.AddPolicy("UsuarioPolicy", policy =>
        policy.RequireRole("Usuario"));
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// =======================
// Arquivos estáticos
// =======================
app.UseStaticFiles();

var pastaImagens = builder.Configuration["Arquivos:ImagensUsuarios"];
if (!string.IsNullOrEmpty(pastaImagens))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(pastaImagens),
        RequestPath = "/ImagensUsuarios"
    });
}

// =======================
// Middleware Pipeline
// =======================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRequestLocalization(localizationOptions);
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
