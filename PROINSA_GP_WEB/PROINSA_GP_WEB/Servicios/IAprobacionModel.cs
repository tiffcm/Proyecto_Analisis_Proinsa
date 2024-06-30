using System.Data;

namespace PROINSA_GP_WEB.Servicios
{
    public interface IAprobacionModel
    {
        DataTable ObtenerSolicitudesEmpleado(int idEmpleado);
    }
}
