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

// Configuración de servicios
builder.Services.AddControllers().AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNamingPolicy = null; });
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

// Inyección de dependencias
builder.Services.AddSingleton<IUsuarioModel, UsuarioModel>();
builder.Services.AddSingleton<ISolicitudModel, SolicitudModel>();
builder.Services.AddSingleton<IAprobacionModel, AprobacionModel>();
builder.Services.AddSingleton<IDocumentoModel, DocumentoModel>();
builder.Services.AddSingleton<IActividadModel, ActividadModel>();
builder.Services.AddSingleton<INominaModel, NominaModel>();
builder.Services.AddSingleton<IReporteModel, ReporteModel>();

// Configuración de autenticación con OpenID Connect
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie() // Para gestionar las cookies de autenticación
.AddOpenIdConnect(options =>
{
    options.ClientId = builder.Configuration["MicrosoftClientId"];
    options.ClientSecret = builder.Configuration["MicrosoftSecretId"];
    options.Authority = "https://login.microsoftonline.com/common/v2.0"; // Reemplaza {tenant-id} por tu Tenant ID
    options.ResponseType = "code"; // Flujo de código para mayor seguridad
    options.SaveTokens = true; // Guardar los tokens
    options.UsePkce = true; // Habilita PKCE para mayor seguridad

    // Solo pedir el correo electrónico
    options.Scope.Add("openid");
    options.Scope.Add("email");

    options.CallbackPath = "/signin-oidc"; // Callback predeterminado
});

// Configuración de cookies de autenticación
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, opciones =>
{
    opciones.Cookie.SameSite = SameSiteMode.None;
    opciones.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Cookies solo por HTTPS
    opciones.Cookie.HttpOnly = true;
    opciones.LoginPath = "/usuarios/login";
    opciones.AccessDeniedPath = "/usuarios/login";
});

// Configuración de sesión
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configuración de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

// Configuración de Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configuración de localización
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Middleware de la aplicación
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Debe ir antes de Authentication para que las sesiones estén habilitadas

app.UseAuthentication();
app.UseAuthorization();

// Configuración de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

app.Run();
