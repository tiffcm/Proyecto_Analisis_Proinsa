using PROINSA_GP_WEB.Entidad;

namespace PROINSA_GP_WEB.Servicios
{
    public interface IActividadModel
    {
        Respuesta? AgregarCliente(Actividad entidad);
        Respuesta? ModificarCliente(Actividad entidad);
        Respuesta? ListarClientes();
        Respuesta? DetallarCliente(long? IdCLIENTE);
        Respuesta? CambiarEstadoCliente(long? IdCLIENTE);
<<<<<<< Updated upstream
=======
        List<SelectListItem> ListarClientesNombres();
>>>>>>> Stashed changes

        ///

        Respuesta? AgregarProyecto(Actividad entidad);
        Respuesta? ModificarProyecto(Actividad entidad);
        Respuesta? ListarProyectos();
        Respuesta? DetallarProyecto(long? IdPROYECTO);
        Respuesta? CambiarEstadoProyecto(long? IdPROYECTO);
<<<<<<< Updated upstream
=======
        List<SelectListItem> ListarProyectosNombres();
        Respuesta? ListarEmpleadosPorProyecto();
        Respuesta? AgregarEmpleadoProyecto(Actividad entidad);
        List<SelectListItem> MostrarTodosEmpleados();
        //List<SelectListItem> MostrarTodosEmpleadosContactos();

        //////////////////////// USUARIO
        ///
        Respuesta? IngresorRegistroActividad(Actividad entidad);
        List<SelectListItem> ListarProyectosPorEmpleado(long? ID_EMPLEADO);
        Respuesta? ModificarRegistroActividad(Actividad entidad);
        Respuesta? ListarTodasActividades();
        Respuesta? DetallarRegistroActividadPorID(long? ID_REGISTROACTIVIDAD);
        Respuesta? CambiarEstadoRegistroActividad(long? ID_REGISTROACTIVIDAD);
>>>>>>> Stashed changes

    }
}
