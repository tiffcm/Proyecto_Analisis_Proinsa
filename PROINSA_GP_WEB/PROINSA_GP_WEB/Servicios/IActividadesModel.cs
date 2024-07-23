using PROINSA_GP_WEB.Entidad;

namespace PROINSA_GP_WEB.Servicios
{
    public interface IActividadesModel
    {
        Respuesta? AgregarCliente(Actividades entidad);
        Respuesta? ModificarCliente(Actividades entidad);
        Respuesta? ListarClientes();
        Respuesta? DetallarCliente(long? IdCLIENTE);
        Respuesta? CambiarEstadoCliente(long? IdCLIENTE);

        ///

        Respuesta? AgregarProyecto(Actividades entidad);
        Respuesta? ModificarProyecto(Actividades entidad);
        Respuesta? ListarProyectos();
        Respuesta? DetallarProyecto(long? IdPROYECTO);
        Respuesta? CambiarEstadoProyecto(long? IdPROYECTO);

    }
}
