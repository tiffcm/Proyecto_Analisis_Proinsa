﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)][Authorize]
    public class ActividadesController(IActividadModel _iActividadModel) : Controller
	{
		/// <summary>
		/// /// USUARIO
		/// </summary>
		/// <returns></returns>

		[Seguridad]
		[HttpGet]
		public IActionResult RegistroActividades()
		{
			long? ID_EMPLEADO = HttpContext.Session.GetInt32("ID_EMPLEADO");
			ProyectosEmpleadoLista(ID_EMPLEADO!.Value);
			
			var viewModel = new Actividad
			{
				EMPLEADO_ID = ID_EMPLEADO.HasValue ? ID_EMPLEADO.Value : 0,  // Preasignar EMPLEADO_ID si es posible
			};

			return View(viewModel);
		}

		[Seguridad]
		[HttpPost]
		public IActionResult RegistroActividades(Actividad entidad)
		{
			long? ID_EMPLEADO = HttpContext.Session.GetInt32("ID_EMPLEADO");

			if (!ID_EMPLEADO.HasValue)
			{
				// Manejar el caso en el que el ID de empleado no esté en la sesión
				ViewBag.msj = "No se pudo obtener el ID del empleado de la sesión.";
				return View();
			}

			entidad.EMPLEADO_ID = ID_EMPLEADO.Value;  // Asigna el ID del empleado al modelo

			var respuesta = _iActividadModel.IngresorRegistroActividad(entidad);

			if (respuesta!.CODIGO == 1)
			{
				return RedirectToAction("HistorialActividades", "Actividades");
			}
			ViewBag.msj = respuesta.MENSAJE;
			return View();
		}

		public IActionResult ProyectosEmpleadoLista(long ID_EMPLEADO)
		{
			var proyectoEMList = _iActividadModel.ListarProyectosPorEmpleado(ID_EMPLEADO);
			ViewBag.proyectoEMList = proyectoEMList!;

			var viewModel = new Actividad
			{
				ID_PROYECTO = 1,
			};

			return View(viewModel);
		}

		[Seguridad]
		[HttpGet]
		public IActionResult EditarActividades(long? Id_REGISTROACTIVIDAD)
		{
			if (Id_REGISTROACTIVIDAD != null)
			{
				long? ID_EMPLEADO = HttpContext.Session.GetInt32("ID_EMPLEADO");

				ProyectosEmpleadoLista(ID_EMPLEADO.Value);

				var respuesta = _iActividadModel.DetallarRegistroActividadPorID(Id_REGISTROACTIVIDAD);
				if (respuesta!.CODIGO == 1)
				{
					var datos = JsonSerializer.Deserialize<Actividad>((JsonElement)respuesta.CONTENIDO!);

					return View(datos);
				}
			}
			return View(new Actividad());
		}

		[Seguridad]
		[HttpPost]
		public IActionResult EditarActividades(Actividad entidad)
		{
			var respuesta = _iActividadModel.ModificarRegistroActividad(entidad);

			if (respuesta!.CODIGO == 1)
			{
				return RedirectToAction("HistorialActividades", "Actividades");
			}

			ViewBag.msj = respuesta.MENSAJE;
			return View(entidad);
		}


		[Seguridad]
		[HttpGet]
		public IActionResult HistorialActividades()
		{
			long? ID_EMPLEADO = HttpContext.Session.GetInt32("ID_EMPLEADO");

			//Falta el SP de ListarActividadesPorEmpleado para que funcione
			var respuesta = _iActividadModel.ListarActividadesPorEmpleado(ID_EMPLEADO);
			if (respuesta!.CODIGO == 1)
			{
				var datos = JsonSerializer.Deserialize<List<Actividad>>((JsonElement)respuesta.CONTENIDO!);

				return View(datos);
			}
			return View(new List<Actividad>());
		}
		public IActionResult CambiarEstadoListaActividad(long Id_REGISTROACTIVIDAD) // en el view se agrega, este es para modificar en lista
		{
			var respuesta = _iActividadModel.CambiarEstadoRegistroActividad(Id_REGISTROACTIVIDAD);

			if (respuesta?.CODIGO == 1)
			{
				return RedirectToAction("HistorialActividades", "Actividades");
			}
			ViewBag.msj = respuesta.MENSAJE;
			return RedirectToAction("HistorialActividades", "Actividades");

		}


		/// <summary>
		/// //// Admin
		/// </summary>
		/// <returns></returns>
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
				return RedirectToAction("ListaClientes", "Actividades");
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
			return View(new Actividad());
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
			return View(entidad);
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

			AsignacionEmpleadosContactoLista();
			ProyectoClientesLista();
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
				return RedirectToAction("ListaProyectos", "Actividades");
			}

			AsignacionEmpleadosContactoLista();
			ProyectoClientesLista();
			ViewBag.msj = respuesta.MENSAJE;
			return View();
		}

		[Administrador]
		[Seguridad]
		[HttpGet]
		public IActionResult EditarProyecto(long? IdPROYECTO)
		{
			AsignacionEmpleadosContactoLista();
			ProyectoClientesLista();
			if (IdPROYECTO != null)
			{
				//ListarEmpleadosPorProyecto(); // Para una tabla que enliste todos los usuarios asignados al proyecto - pending, no estaba en las HU
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
			AsignacionEmpleadosContactoLista();
			ProyectoClientesLista();
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


		[Administrador]
		[Seguridad]
		[HttpPost]
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


		// CREAR VISTA PARA LA ASIGNACION DE PROYECTOS A EMPLEADOS

		public IActionResult AsignacionProyectoLista()
		{

			var ProyectoList = _iActividadModel.ListarProyectosNombres();
			ViewBag.ProyectoList = ProyectoList!;

			var viewModel = new Actividad
			{
				ID_PROYECTO = 1,

			};

			return View(viewModel);
		}


		[Administrador]
		[Seguridad]
		[HttpGet]
		public IActionResult ListarEmpleadosPorProyecto()
		{
			var respuesta = _iActividadModel.ListarEmpleadosPorProyecto();
			if (respuesta!.CODIGO == 1)
			{
				var datos = JsonSerializer.Deserialize<List<Actividad>>((JsonElement)respuesta.CONTENIDO!);

				return View(datos);
			}
			return View(new List<Actividad>());
		}

		//  VISTA PARA LA ASIGNACION DE PROYECTOS A EMPLEADOS

		[Administrador]
		[Seguridad]
		[HttpGet]
		public IActionResult AsignacionProyectoEmpleaado()
		{
			AsignacionProyectoLista();
			AsignacionEmpleadosLista();
			return View();
		}

		[Administrador]
		[Seguridad]
		[HttpPost]
		public IActionResult AsignacionProyectoEmpleaado(Actividad entidad)
		{
			var respuesta = _iActividadModel.AgregarEmpleadoProyecto(entidad);

			if (respuesta!.CODIGO == 1)
			{
				return RedirectToAction("Principal", "Home");
			}
			AsignacionProyectoLista();
			AsignacionEmpleadosLista();
			ViewBag.msj = respuesta.MENSAJE;
			return View();
		}


		public IActionResult ProyectoClientesLista()
		{
			var clientesList = _iActividadModel.ListarClientesNombres();
			ViewBag.ClientesList = clientesList!;

			var viewModel = new Actividad
			{
				ID_PROYECTO = 1,
			};

			return View(viewModel);
		}

		public IActionResult AsignacionEmpleadosLista()
		{
			var EmpleadoList = _iActividadModel.MostrarTodosEmpleados();
			ViewBag.EmpleadoList = EmpleadoList!;

			var viewModel = new Actividad
			{
				EMPLEADO_ID = 1,
			};

			return View(viewModel);
		}

		public IActionResult AsignacionEmpleadosContactoLista()
		{
			var empleadoContactList = _iActividadModel.MostrarTodosEmpleados();
			ViewBag.EmpleadoContactList = empleadoContactList!;

			var viewModel = new Actividad
			{
				CONTACTO_ID = 1,
			};

			return View(viewModel);
		}

	}
}
