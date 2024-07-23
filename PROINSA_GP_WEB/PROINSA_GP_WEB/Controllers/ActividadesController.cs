using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ActividadesController (IActividadesModel _iActividadesModel) : Controller
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
