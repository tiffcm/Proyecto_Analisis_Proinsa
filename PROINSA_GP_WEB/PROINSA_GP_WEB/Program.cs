using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var supportedCultures = new[] { new CultureInfo("es-ES"), new CultureInfo("en-US") };

// Configuraci�n de JSON para evitar la conversi�n de nombres en camelCase
builder.Services.AddControllers().AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNamingPolicy = null; });

// Servicios para la inyecci�n de dependencias
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

// Configuraci�n para autenticaci�n con Microsoft Account (Azure)
builder.Services.AddAuthentication().AddMicrosoftAccount(opciones =>
{
    opciones.ClientId = builder.Configuration["MicrosoftClientId"]!;
    opciones.ClientSecret = builder.Configuration["MicrosoftSecretId"]!;
    opciones.CallbackPath = "/signin-microsoft"; // Aseg�rate que coincide con el Redirect URI en Azure
});

// Configuraci�n de sesi�n (tiempo de vida y cookies)
builder.Services.AddDistributedMemoryCache(); // Almac�n de sesi�n en memoria (en un entorno de producci�n puedes usar Redis u otro)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiraci�n de la sesi�n
    options.Cookie.HttpOnly = true; // Las cookies solo ser�n accesibles v�a HTTP
    options.Cookie.IsEssential = true; // Esencial para el funcionamiento de la sesi�n
    options.Cookie.SameSite = SameSiteMode.None; // SameSite deshabilitado para evitar problemas en la autenticaci�n cruzada
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Aseg�rate de que las cookies solo se env�an v�a HTTPS
});

// Configuraci�n de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

// Configuraci�n de Identity (con autenticaci�n de cuentas no confirmadas)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configuraci�n de cookies de autenticaci�n (ruta de login y acceso denegado)
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones =>
    {
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath = "/usuarios/login";
        opciones.Cookie.SameSite = SameSiteMode.None; // Configuraci�n para evitar problemas con autenticaci�n cruzada
        opciones.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Aseg�rate de que las cookies se transmiten v�a HTTPS
    });

// Configuraci�n de localizaci�n (idiomas soportados)
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configuraci�n del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Usar redirecci�n a HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Configuraci�n del middleware en el orden correcto
app.UseSession(); // Habilitar sesiones

app.UseAuthentication(); // Siempre va antes que Authorization
app.UseAuthorization();

// Definici�n de las rutas del controlador
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

app.Run();
