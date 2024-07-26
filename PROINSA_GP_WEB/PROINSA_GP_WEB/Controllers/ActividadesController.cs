using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ActividadesController (IActividadModel _iActividadModel) : Controller
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

        // Admin
        [Administrador]
        [Seguridad]
        [HttpGet]
        public IActionResult IngresoClientes()
        {
            return View();
        }

        [Administrador]
        [Seguridad]
        [HttpPost]
        public IActionResult IngresoClientes(Actividad entidad)
        {
            var respuesta = _iActividadModel.AgregarCliente(entidad);
            
            if (respuesta!.CODIGO == 1)
            {
                return RedirectToAction("Principal", "Home");
            }
            
            ViewBag.msj = respuesta.MENSAJE;
            return View();
        }

        [Administrador]
        [Seguridad]
        [HttpGet]
        public IActionResult EditarCliente(long? IdCLIENTE)
        {
            if (IdCLIENTE != null)
            {
                var respuesta = _iActividadModel.DetallarCliente(IdCLIENTE);
                if (respuesta!.CODIGO == 1)
                {
                    var datos = JsonSerializer.Deserialize<Actividad>((JsonElement)respuesta.CONTENIDO!);
                     return View(datos);
                }
            }
            return View();
        }

        [Administrador]
        [Seguridad]
        [HttpPost]
        public IActionResult EditarCliente(Actividad entidad)
        {
            var respuesta = _iActividadModel.ModificarCliente(entidad);

            if (respuesta!.CODIGO == 1)
            {
                return RedirectToAction("ListaClientes", "Actividades");
            }

            ViewBag.msj = respuesta.MENSAJE;
            return View();
        }

        [Administrador]
        [Seguridad]
        [HttpGet]
        public IActionResult ListaClientes()
        {
            var respuesta = _iActividadModel.ListarClientes();
            if (respuesta!.CODIGO == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Actividad>>((JsonElement)respuesta.CONTENIDO!);

                return View(datos);
            }
            return View(new List<Actividad>());
        }

        public IActionResult CambiarEstadoCliente(long IdCLIENTE) // en el view se agrega, este es para modificar individual
        {
            var respuesta = _iActividadModel.CambiarEstadoCliente(IdCLIENTE);
            if (respuesta.CODIGO == 1)
            {
                return RedirectToAction("EditarCliente", new { IdCLIENTE });
            }

            ViewBag.msj = respuesta.MENSAJE;
            return RedirectToAction("EditarCliente", new { IdCLIENTE });


        }

		[Administrador]
		[Seguridad]
		[HttpPost]
		public IActionResult CambiarEstadoLista(long Id_CLIENTE) // en el view se agrega, este es para modificar en lista
        {
            var respuesta = _iActividadModel.CambiarEstadoCliente(Id_CLIENTE);

			if (respuesta.CODIGO == 1)
			{
				return RedirectToAction("ListaClientes", "Actividades");
			}
			ViewBag.msj = respuesta.MENSAJE;
			return RedirectToAction("ListaClientes", "Actividades");

        }

        /// <summary>
        /// PROYECTO
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        /// 

        [Administrador]
        [Seguridad]
        [HttpGet]
        public IActionResult RegistrarProyecto()
        {
            return View();
        }

        [Administrador]
        [Seguridad]
        [HttpPost]
        public IActionResult RegistrarProyecto(Actividad entidad)
        {
            var respuesta = _iActividadModel.AgregarProyecto(entidad);

            if (respuesta!.CODIGO == 1)
            {
                return RedirectToAction("Principal", "Home");
            }

            ViewBag.msj = respuesta.MENSAJE;
            return View();
        }

        [Administrador]
        [Seguridad]
        [HttpGet]
        public IActionResult EditarProyecto(long? IdPROYECTO)
        {
            if (IdPROYECTO != null)
            {
                var respuesta = _iActividadModel.DetallarProyecto(IdPROYECTO);
                if (respuesta!.CODIGO == 1)
                {
                    var datos = JsonSerializer.Deserialize<Actividad>((JsonElement)respuesta.CONTENIDO!);
                    return View(datos);
                }
            }
            return View();
        }

        [Administrador]
        [Seguridad]
        [HttpPost]
        public IActionResult EditarProyecto(Actividad entidad)
        {
            var respuesta = _iActividadModel.ModificarProyecto(entidad);

            if (respuesta!.CODIGO == 1)
            {
                return RedirectToAction("ListaProyectos", "Actividades");
            }

            ViewBag.msj = respuesta.MENSAJE;
            return View();
        }

        [Administrador]
        [Seguridad]
        [HttpGet]
        public IActionResult ListaProyectos()
        {
            var respuesta = _iActividadModel.ListarProyectos();
            if (respuesta!.CODIGO == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Actividad>>((JsonElement)respuesta.CONTENIDO!);

                return View(datos);
            }
            return View(new List<Actividad>());
        }

        public IActionResult CambiarEstadoProyecto(long IdPROYECTO) // en el view se agrega, este es para modificar individual
        {
            var respuesta = _iActividadModel.CambiarEstadoProyecto(IdPROYECTO);
            if (respuesta.CODIGO == 1)
            {
                return RedirectToAction("EditarProyecto", new { IdPROYECTO });
            }

            ViewBag.msj = respuesta.MENSAJE;
            return RedirectToAction("EditarProyecto", new { IdPROYECTO });


        }
    }
}
