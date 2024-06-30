﻿using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Data;
using System.Text.Json;

namespace PROINSA_GP_WEB.Controllers
{
    public class SolicitudesController (ISolicitudModel iSolicitudModel): Controller
    {
        [HttpGet]
        public IActionResult RegistrarSolicitud()
        {
            CargarTiposSolicitud();
            ObtenerIdEmpleado();
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarSolicitud(Solicitud ent)
        {
            var resp = iSolicitudModel.RegistrarSolicitud(ent);

            if (resp!.CODIGO == 1)
            {
                return RedirectToAction("Principal", "Home");
            }
            else
            {
                ViewBag.msj = resp.MENSAJE;
            }            
            //Hay que controlar error en caso de no tener saldo de vacaciones
            return View();
        }


        public ActionResult CargarTiposSolicitud()
        {
            var tipoSolicitudList = iSolicitudModel.ObtenerTipoSolicitud();
            ViewBag.TipoSolicitudList = tipoSolicitudList;

            var viewModel = new Solicitud
            {
                ID = 2 
            };

            return View(viewModel);
        }

        public ActionResult CargarHorariosDisponibles()
        {
            var idEmpleadoSession = HttpContext.Session.GetInt32("ID_EMPLEADO");


            int idEmpleado = idEmpleadoSession ?? 0;
            var horarioDisponibleList = iSolicitudModel.ObtenerHorarioDisponibles(idEmpleado);
            ViewBag.HorarioDisponibleList = horarioDisponibleList;

            var viewModel = new Solicitud
            {
                ID = 2
            };

            return View(viewModel);
        }


        public ActionResult ObtenerIdEmpleado()
        {
            
            long? ID_EMPLEADO = HttpContext.Session.GetInt32("ID_EMPLEADO");

            
            var viewModel = new Solicitud
            {
                SOLICITANTE_ID = ID_EMPLEADO ?? 0 
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Constancias()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CambiosHorario()
        {            
            CargarHorariosDisponibles();
           
            var correoEmpleado = User.Identity?.Name;
            if (correoEmpleado != null)
            {
                var respuesta = iSolicitudModel.ObtenerHorarioEmpleado(correoEmpleado);
                var horario = JsonSerializer.Deserialize<Solicitud>((JsonElement)respuesta!.CONTENIDO!);
                long? ID_EMPLEADO = HttpContext.Session.GetInt32("ID_EMPLEADO");
                var viewModel = new Solicitud
                {
                    SOLICITANTE_ID = ID_EMPLEADO,
                    HORARIO = horario!.HORARIO,
                };                
                return View(viewModel);
            }          
            return View(new Solicitud());
        }

        [HttpPost]
        public IActionResult CambiosHorario(Solicitud ent)
        {
            var resp = iSolicitudModel.RegistrarSolicitudCambioHorario(ent);

            if (resp!.CODIGO == 1)
            {
                return RedirectToAction("Principal", "Home");
            }               
            ViewBag.msj = resp.MENSAJE;
            return View();
        }

        [HttpGet]
        public IActionResult ReporteSolicitudAntiguedad()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HistorialSolicitudes()
        {
            var idEmpleadoSession = HttpContext.Session.GetInt32("ID_EMPLEADO");


            int idEmpleado = idEmpleadoSession ?? 0;


            DataTable solicitudes = iSolicitudModel.ObtenerSolicitudesEmpleado(idEmpleado)!;


            return View(solicitudes);
        }
    }
}