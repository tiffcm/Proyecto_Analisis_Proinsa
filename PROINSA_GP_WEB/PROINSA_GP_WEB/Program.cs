using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);
var supportedCultures = new[] { new CultureInfo("es-ES"), new CultureInfo("en-US") };


builder.Services.AddControllers().AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNamingPolicy = null; });
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IUsuarioModel, UsuarioModel>();
builder.Services.AddSingleton<ISolicitudModel, SolicitudModel>();
builder.Services.AddSingleton<IAprobacionModel, AprobacionModel>();
builder.Services.AddSingleton<IDocumentoModel, DocumentoModel>();
builder.Services.AddSingleton<IActividadModel, ActividadModel>();
builder.Services.AddSingleton<INominaModel, NominaModel>();
builder.Services.AddSingleton<IReporteModel, ReporteModel>();

// Para autenticación con AZURE
builder.Services.AddAuthentication().AddMicrosoftAccount(opciones =>
{
    opciones.ClientId = builder.Configuration["MicrosoftClientId"]!;
    opciones.ClientSecret = builder.Configuration["MicrosoftSecretId"]!;
});

// Para uso de las variables sesión
builder.Services.AddDistributedMemoryCache(); // Usar una memoria cache distribuida en memoria
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Hacer que la cookie solo sea accesible a través de HTTP
    options.Cookie.IsEssential = true; // Hacer que la cookie sea esencial
});

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones =>
    {
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath = "/usuarios/login";
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Habilitar sesiones

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

app.Run();

