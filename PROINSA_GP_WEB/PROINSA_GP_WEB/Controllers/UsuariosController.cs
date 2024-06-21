using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Servicios;
using System.Security.Claims;

namespace PROINSA_GP_WEB.Controllers
{
    public class UsuariosController (IUsuarioModel _iUsuarioModel) : Controller 
    {

        [HttpGet]
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

            var correoEmpleado = User.Identity!.Name;
            
            var respuesta = _iUsuarioModel.ConsultarDatosEmpleado(correoEmpleado);
            if (respuesta?.CODIGO == 1)
            {                
                return View(respuesta.CONTENIDO);
            }
            else
            {
                ViewBag.MjsPantalla = respuesta?.MENSAJE;
                return View();
            }

        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdministrarUsuarios()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MantenimientoUsuario()
        {
            return View();
        }
    }
}
