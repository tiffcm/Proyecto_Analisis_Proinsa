using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Models;

namespace PROINSA_GP_WEB.Controllers
{
    /// <summary>
    /// Pantalla principal y otros datos de interés
    /// </summary>
    /// <param name="_iUsuarioModel">Es para manejar interfaces en lugar del modelo directo</param>
    /// <version>1.2</version>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
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
            return View();
        }
    }
}
