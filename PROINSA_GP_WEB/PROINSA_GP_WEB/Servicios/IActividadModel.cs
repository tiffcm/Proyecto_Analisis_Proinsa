
﻿using Microsoft.AspNetCore.Mvc.Rendering;
using PROINSA_GP_WEB.Entidad;
using static PROINSA_GP_WEB.Entidad.Actividad;

namespace PROINSA_GP_WEB.Servicios
{
    public interface IActividadModel
    {
        Respuesta? AgregarCliente(Actividad entidad);
        Respuesta? ModificarCliente(Actividad entidad);
        Respuesta? ListarClientes();
        Respuesta? DetallarCliente(long? IdCLIENTE);
        Respuesta? CambiarEstadoCliente(long? IdCLIENTE);
        List<SelectListItem> ListarClientesNombres();


        ///

        Respuesta? AgregarProyecto(Actividad entidad);
        Respuesta? ModificarProyecto(Actividad entidad);
        Respuesta? ListarProyectos();
        Respuesta? DetallarProyecto(long? IdPROYECTO);
        Respuesta? CambiarEstadoProyecto(long? IdPROYECTO);
        List<SelectListItem> ListarProyectosNombres();
        Respuesta? ListarEmpleadosPorProyecto();
        Respuesta? AgregarEmpleadoProyecto(Actividad entidad);
        List<SelectListItem> MostrarTodosEmpleados();
        //List<SelectListItem> MostrarTodosEmpleadosContactos();

        //////////////////////// USUARIO
        ///
        Respuesta? IngresorRegistroActividad(Actividad entidad);
        List<SelectListItem> ListarProyectosPorEmpleado(long? ID_EMPLEADO);
        //List<SelectListItem> ListarActividadesPorEmpleado(long? ID_EMPLEADO);
        Respuesta? ListarActividadesPorEmpleado(long? ID_EMPLEADO);
		Respuesta? ModificarRegistroActividad(Actividad entidad);
        Respuesta? ListarTodasActividades();
        Respuesta? DetallarRegistroActividadPorID(long? ID_REGISTROACTIVIDAD);
        Respuesta? CambiarEstadoRegistroActividad(long? ID_REGISTROACTIVIDAD);

    }
}
