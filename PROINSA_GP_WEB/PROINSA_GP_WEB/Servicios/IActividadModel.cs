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

        ///

        Respuesta? AgregarProyecto(Actividad entidad);
        Respuesta? ModificarProyecto(Actividad entidad);
        Respuesta? ListarProyectos();
        Respuesta? DetallarProyecto(long? IdPROYECTO);
        Respuesta? CambiarEstadoProyecto(long? IdPROYECTO);

    }
}
