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
            /**
             * Se encarga de llamar al modelo de empleado por medio de la interfaz
             * Se devuelve la respuesta y se evalúa, si es 00 significa que es positiva
             * y se devuelve la información que viene del API como respuesta.Dato y se
             * deserializa de Json a archivo legible por HTML.
             * 
             * Se pasa la información a la Vista MiCuenta en caso de ser positivo
             **/
            var correoEmpleado = User.Identity?.Name;
            if (correoEmpleado != null)
            {
                var respuesta = _iUsuarioModel.ConsultarDatosEmpleado(correoEmpleado);
                if (respuesta!.CODIGO == 1)
                {
                    var usuario = JsonSerializer.Deserialize<Usuario>((JsonElement)respuesta.CONTENIDO!);
                    if (usuario != null)
                    {
                        // Pasa el objeto Usuario a la vista
                        return View(usuario);
                    }                    
                }
                else
                {
                    ViewBag.MjsPantalla = respuesta?.MENSAJE;
                }
            }
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
