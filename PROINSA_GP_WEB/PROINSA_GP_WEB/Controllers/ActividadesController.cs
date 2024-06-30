using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class ActividadesController : Controller
    {
        [HttpGet]
        public IActionResult RegistroActividades()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditarActividades()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HistorialActividades()
        {
            return View();
        }
    }
}
