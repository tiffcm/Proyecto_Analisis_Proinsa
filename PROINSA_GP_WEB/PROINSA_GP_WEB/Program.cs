using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var supportedCultures = new[] { new CultureInfo("es-ES"), new CultureInfo("en-US") };

// Configuración de JSON para evitar la conversión de nombres en camelCase
builder.Services.AddControllers().AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNamingPolicy = null; });

// Servicios para la inyección de dependencias
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

// Servicios Singleton
builder.Services.AddSingleton<IUsuarioModel, UsuarioModel>();
builder.Services.AddSingleton<ISolicitudModel, SolicitudModel>();
builder.Services.AddSingleton<IAprobacionModel, AprobacionModel>();
builder.Services.AddSingleton<IDocumentoModel, DocumentoModel>();
builder.Services.AddSingleton<IActividadModel, ActividadModel>();
builder.Services.AddSingleton<INominaModel, NominaModel>();
builder.Services.AddSingleton<IReporteModel, ReporteModel>();

// Configuración para autenticación con Microsoft Account (Azure)
builder.Services.AddAuthentication().AddMicrosoftAccount(opciones =>
{
    opciones.ClientId = builder.Configuration["MicrosoftClientId"]!;
    opciones.ClientSecret = builder.Configuration["MicrosoftSecretId"]!;
    opciones.CallbackPath = "/signin-microsoft"; // Asegúrate que coincide con el Redirect URI en Azure
});

// Configuración de sesión (tiempo de vida y cookies)
builder.Services.AddDistributedMemoryCache(); // Almacén de sesión en memoria (en un entorno de producción puedes usar Redis u otro)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Las cookies solo serán accesibles vía HTTP
    options.Cookie.IsEssential = true; // Esencial para el funcionamiento de la sesión
    options.Cookie.SameSite = SameSiteMode.None; // SameSite deshabilitado para evitar problemas en la autenticación cruzada
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Asegúrate de que las cookies solo se envían vía HTTPS
});

// Configuración de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

// Configuración de Identity (con autenticación de cuentas no confirmadas)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configuración de cookies de autenticación (ruta de login y acceso denegado)
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones =>
    {
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath = "/usuarios/login";
        opciones.Cookie.SameSite = SameSiteMode.None; // Configuración para evitar problemas con autenticación cruzada
        opciones.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Asegúrate de que las cookies se transmiten vía HTTPS
    });

// Configuración de localización (idiomas soportados)
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Usar redirección a HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Configuración del middleware en el orden correcto
app.UseSession(); // Habilitar sesiones

app.UseAuthentication(); // Siempre va antes que Authorization
app.UseAuthorization();

// Definición de las rutas del controlador
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

app.Run();
