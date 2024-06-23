using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;

namespace PROINSA_GP_WEB.Controllers
{
    public class UsuariosController (IUsuarioModel _iUsuarioModel) : Controller 
    {
        [Seguridad][HttpGet]
        public IActionResult MiCuenta()
        {
            var correoEmpleado = User.Identity?.Name;
            if (correoEmpleado != null)
            {
                var respuesta = _iUsuarioModel.ConsultarDatosEmpleado(correoEmpleado);
                if (respuesta!.CODIGO == 1)
                {
                    var usuario = JsonSerializer.Deserialize<Usuario>((JsonElement)respuesta.CONTENIDO!);
                    if (usuario != null)
                    {
                        return View(usuario);
                    }                    
                }
            }
            return View();
        }
        [Seguridad][HttpPost]
        public IActionResult MiCuenta(Usuario entidad)
        {
            var respuesta = _iUsuarioModel.ActualizarDatosUsuario(entidad);
            return View();
        }

        [Administrador][Seguridad][HttpGet]
        public IActionResult AdministrarUsuarios()
        {
            return View();
        }

        [Administrador][Seguridad][HttpGet]
        public IActionResult MantenimientoUsuario()
        {
            return View();
        }
    }
}
