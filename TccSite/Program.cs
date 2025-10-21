using Microsoft.AspNetCore.Authentication.Cookies;
using TccSite.Application;
using TccSite.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add MVC
builder.Services.AddControllersWithViews();

// Add Application and Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// ========== AUTENTICAÇÃO E AUTORIZAÇÃO ==========
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Página de login
        options.AccessDeniedPath = "/AcessoNegado"; // Página de acesso negado
    });

// Definição de Policies (opcional, mas boa prática)
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
// Middleware Pipeline
// =======================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ORDEM IMPORTANTE:
app.UseAuthentication(); // Primeiro: autenticação
app.UseAuthorization();  // Depois: autorização

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
