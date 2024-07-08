using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Models;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ActividadesController : Controller
    {
        [Seguridad][HttpGet]
        public IActionResult RegistroActividades()
        {
            return View();
        }

        [Seguridad][HttpGet]
        public IActionResult EditarActividades()
        {
            return View();
        }

        [Seguridad][HttpGet]
        public IActionResult HistorialActividades()
        {
            return View();
        }
    }
}
