using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Diagnostics;

namespace PROINSA_GP_WEB.Controllers
{
    public class HomeController(IEmpleadoModel _iEmpleadoModel) : Controller
    {

        public IActionResult Index()
        {
            var correoEmpleado = User.Identity!.Name;
            if (correoEmpleado != null)
            {
                var respuesta = _iEmpleadoModel.ObtenerDatosEmpleado(correoEmpleado);
                if (respuesta != null)
                {
                    ViewBag.Rol = respuesta.DATO.NOMBREROL;                   
                }
            }            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
