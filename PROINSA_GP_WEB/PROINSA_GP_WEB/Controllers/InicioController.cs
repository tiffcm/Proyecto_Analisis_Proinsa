﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Buffers.Text;
using System.Security.Claims;
using System.Text.Json;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    /// <summary>
    /// Lógica de inicio de sesión y validación de usuarios con Entra ID
    /// </summary>
    /// <author>Tiffany Camacho Monge, Brandon Ruiz Miranda</author>
    /// <version>1.3</version>
    public class InicioController(SignInManager<IdentityUser> signInManager, IUsuarioModel _iUsuarioModel, IConfiguration iConfiguration, UserManager<IdentityUser> userManager) : Controller
    {
        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? mensaje = null)
        {

            if (mensaje == null)
            {
                ViewData["mensaje"] = mensaje;
            }

            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult LoginExterno(string proveedor, string? urlRetorno = null)
        {
            var urlRedireccion = Url.Action("RegistrarUsuarioExterno", values: new
            { urlRetorno });

            var propiedades = signInManager.ConfigureExternalAuthenticationProperties(proveedor, urlRedireccion);
            return new ChallengeResult(proveedor, propiedades);

        }


        [AllowAnonymous]
        public async Task<IActionResult> RegistrarUsuarioExterno(string? urlRetorno = null,
        string? remoteError = null)
        {
            urlRetorno = urlRetorno ?? Url.Content("~/");
            var mensaje = "";

            if (remoteError != null)
            {
                mensaje = $"Error from external provider: {remoteError}";
                return RedirectToAction("Home/Principal", routeValues: new { mensaje });
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                mensaje = "Error loading external login information.";
                return RedirectToAction("IniciarSesion", "Inicio", routeValues: new { mensaje });
            }

            var resultadoLoginExterno = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            // Ya la cuenta existe
            if (resultadoLoginExterno.Succeeded)
            {
                var correoEmpleado = User.Identity?.Name;
                if (correoEmpleado != null)
                {
                    var respuesta = _iUsuarioModel.ConsultarDatosEmpleado(correoEmpleado);
                    if (respuesta!.CODIGO == 1)
                    {
                        var datosUsuario = JsonSerializer.Deserialize<Usuario>((JsonElement)respuesta.CONTENIDO!);

                        HttpContext.Session.SetInt32("ID_EMPLEADO", (int)datosUsuario!.ID_EMPLEADO);
                        HttpContext.Session.SetString("NombreUsuario", datosUsuario!.NOMBRECOMPLETO!);
                        HttpContext.Session.SetInt32("IdRol", datosUsuario!.IDROL!);
                        HttpContext.Session.SetString("RolUsuario", datosUsuario!.NOMBREROL!);
                        HttpContext.Session.Set("BLOB", datosUsuario!.FOTO!);
                        string base64 = Convert.ToBase64String(datosUsuario!.FOTO!);
                        string extension = datosUsuario.TIPO_FOTO ?? "NoFoto";
                        datosUsuario.FOTO_VISTA = $"data:{extension};base64,{base64}";
                        HttpContext.Session.SetString("FOTO", datosUsuario!.FOTO_VISTA!);
                        HttpContext.Session.SetString("EXTENSION", extension);
                    }
                    else
                    {
                        ViewBag.MensajePantalla = respuesta.MENSAJE;
                        return RedirectToAction("IniciarSesion", "Inicio");
                    }
                }
                return RedirectToAction("Principal", "Home");
            }

            string email = "";


            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                email = info.Principal.FindFirstValue(ClaimTypes.Email)!;
            }
            else
            {
                mensaje = "Error leyendo el email del usuario del proveedor.";
                return RedirectToAction("IniciarSesion", "Inicio", routeValues: new { mensaje });
            }

            var usuario = new IdentityUser() { Email = email, UserName = email };

            var resultadoCrearUsuario = await userManager.CreateAsync(usuario);
            if (!resultadoCrearUsuario.Succeeded)
            {
                mensaje = resultadoCrearUsuario.Errors.First().Description;
                return RedirectToAction("IniciarSesion", "Inicio", routeValues: new { mensaje });
            }

            var resultadoAgregarLogin = await userManager.AddLoginAsync(usuario, info);

            if (resultadoAgregarLogin.Succeeded)
            {
                await signInManager.SignInAsync(usuario, isPersistent: false, info.LoginProvider);

                return RedirectToAction("Principal", "Home");
            }

            mensaje = "Ha ocurrido un error agregando el login.";
            return RedirectToAction("IniciarSesion", "Inicio", routeValues: new { mensaje });
        }

        [Seguridad]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Cerrar sesión a nivel de la aplicación
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Redirigir al endpoint de cierre de sesión de Entra ID
            var callbackUrl = Url.Action("IniciarSesion", "Inicio", null, Request.Scheme);
            var tenantId = iConfiguration.GetSection("MicrosoftTenantID").Value;
            var logoutUrl = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/logout?post_logout_redirect_uri={callbackUrl}";

            // Redirigir a la página de cierre de sesión de Entra ID
            return Redirect(logoutUrl);
        }

    }
}