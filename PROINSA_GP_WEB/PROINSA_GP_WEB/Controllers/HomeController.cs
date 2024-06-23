using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;

namespace PROINSA_GP_WEB.Controllers
{
    /// <summary>
    /// Ese código inicial lo que identifica es que cuando una persona cierra sesión no 
    /// permite que entre al sistema si no está autenticado.En NET Core no es recomendable
    /// instanciar el modelo directamente porque es ineficiente y para ello es mejor usar 
    /// interfaces por medio de una inyección de dependencias.
    /// </summary>
    /// <param name="_iUsuarioModel">Es para manejar interfaces en lugar del modelo directo</param>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController(IUsuarioModel _iUsuarioModel) : Controller
    {
        /// <summary>
        /// Es la página inicial al entrar a la aplicación, también se envía el dato
        /// del correo recuperado por Identity para traer los datos de el usuario que
        /// inició sesión.
        /// </summary>
        /// <returns>Vista</returns>
        [Seguridad][HttpGet]
        public IActionResult Principal()
        {
            var correoEmpleado = User.Identity?.Name;
            if (correoEmpleado != null)
            {
                var respuesta = _iUsuarioModel.ConsultarDatosEmpleado(correoEmpleado);
                if (respuesta!.CODIGO == 1)
                {
                    var usuario = JsonSerializer.Deserialize<Usuario>((JsonElement)respuesta.CONTENIDO!);                    
                    var rolUsuario = usuario!.NOMBREROL;
                    var idRol = usuario!.IDROL;
                    var nombreUsuario = usuario.NOMBRECOMPLETO;
                    HttpContext.Session.SetString("RolUsuario", rolUsuario!);
                    HttpContext.Session.SetInt32("IdRol", idRol!);
                    HttpContext.Session.SetString("NombreUsuario", nombreUsuario!);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
