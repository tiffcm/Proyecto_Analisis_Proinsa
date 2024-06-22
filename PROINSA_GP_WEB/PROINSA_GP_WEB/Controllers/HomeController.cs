using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;

namespace PROINSA_GP_WEB.Controllers
{
    /// <summary>
    /// Ese c�digo inicial lo que identifica es que cuando una persona cierra sesi�n no 
    /// permite que entre al sistema si no est� autenticado.En NET Core no es recomendable
    /// instanciar el modelo directamente porque es ineficiente y para ello es mejor usar 
    /// interfaces por medio de una inyecci�n de dependencias.
    /// </summary>
    /// <param name="_iUsuarioModel">Es para manejar interfaces en lugar del modelo directo</param>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController(IUsuarioModel _iUsuarioModel) : Controller
    {
        /// <summary>
        /// Es la p�gina inicial al entrar a la aplicaci�n, tambi�n se env�a el dato
        /// del correo recuperado por Identity para traer los datos de el usuario que
        /// inici� sesi�n.
        /// </summary>
        /// <returns>Vista</returns>
        [HttpGet]
        public IActionResult Principal()
        {
            var correoEmpleado = User.Identity?.Name;
            if (correoEmpleado != null)
            {
                var respuesta = _iUsuarioModel.ConsultarDatosEmpleado(correoEmpleado);
                if (respuesta != null)
                {
                   
                    if (respuesta.CONTENIDO is JsonElement jsonElement)
                    {
                        var usuario = JsonSerializer.Deserialize<Usuario>(jsonElement.GetRawText());
                        if (usuario != null)
                        {
                            var rolUsuario = usuario.NOMBREROL;
                            var idUsuario = usuario.ID_EMPLEADO;
                            ViewBag.Rol = rolUsuario;
                            HttpContext.Session.SetString("rol", rolUsuario);
                            HttpContext.Session.SetString("idusuario", idUsuario.ToString());
                        }
                    }
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
