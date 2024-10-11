using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var supportedCultures = new[] { new CultureInfo("es-ES"), new CultureInfo("en-US") };

// Configuraci�n de servicios
builder.Services.AddControllers().AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNamingPolicy = null; });
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

// Inyecci�n de dependencias
builder.Services.AddSingleton<IUsuarioModel, UsuarioModel>();
builder.Services.AddSingleton<ISolicitudModel, SolicitudModel>();
builder.Services.AddSingleton<IAprobacionModel, AprobacionModel>();
builder.Services.AddSingleton<IDocumentoModel, DocumentoModel>();
builder.Services.AddSingleton<IActividadModel, ActividadModel>();
builder.Services.AddSingleton<INominaModel, NominaModel>();
builder.Services.AddSingleton<IReporteModel, ReporteModel>();

// Configuraci�n de autenticaci�n con OpenID Connect
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie() // Para gestionar las cookies de autenticaci�n
.AddOpenIdConnect(options =>
{
    options.ClientId = builder.Configuration["MicrosoftClientId"];
    options.ClientSecret = builder.Configuration["MicrosoftSecretId"];
    options.Authority = "https://login.microsoftonline.com/common/v2.0"; // Reemplaza {tenant-id} por tu Tenant ID
    options.ResponseType = "code"; // Flujo de c�digo para mayor seguridad
    options.SaveTokens = true; // Guardar los tokens
    options.UsePkce = true; // Habilita PKCE para mayor seguridad

    // Solo pedir el correo electr�nico
    options.Scope.Add("openid");
    options.Scope.Add("email");

    options.CallbackPath = "/signin-oidc"; // Callback predeterminado
});

// Configuraci�n de cookies de autenticaci�n
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, opciones =>
{
    opciones.Cookie.SameSite = SameSiteMode.None;
    opciones.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Cookies solo por HTTPS
    opciones.Cookie.HttpOnly = true;
    opciones.LoginPath = "/usuarios/login";
    opciones.AccessDeniedPath = "/usuarios/login";
});

// Configuraci�n de sesi�n
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configuraci�n de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

// Configuraci�n de Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configuraci�n de localizaci�n
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Middleware de la aplicaci�n
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Debe ir antes de Authentication para que las sesiones est�n habilitadas

app.UseAuthentication();
app.UseAuthorization();

// Configuraci�n de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

app.Run();
