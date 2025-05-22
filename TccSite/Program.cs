using TccSite.Data.Repository;
using TccSite.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

#region Services Configuration

// MVC Controllers + Views
builder.Services.AddControllersWithViews();

// Dependency Injection
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Adicione aqui novos serviços (ex: DbContext, Identity, AutoMapper, etc.)
// builder.Services.AddDbContext<...>();
// builder.Services.AddScoped<IOutroRepositorio, OutroRepositorio>();
// builder.Services.AddAutoMapper(typeof(Program));

#endregion

var app = builder.Build();

#region Middleware Pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTPS Strict Transport Security
}

//app.UseHttpsRedirection(); // Reative se necessário
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Endpoint Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

#endregion

app.Run();
