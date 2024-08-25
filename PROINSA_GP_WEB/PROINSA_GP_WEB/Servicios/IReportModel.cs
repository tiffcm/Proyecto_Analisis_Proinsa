using PROINSA_GP_WEB.Entidad;
using System.Data;

namespace PROINSA_GP_WEB.Servicios
{
    public interface IReportModel
    {

        Respuesta DatosEmpleadoNominaReporte(long EMPLEADO_ID);
        Respuesta DatosNominaEmpleadoDeduccionesReporte(Report report);
        Respuesta DatosNominaEmpleadoIngresosReporte(Report report);
        Respuesta DatosNominaEmpleadoReporte(Report report);
        Respuesta DatosNominaGeneralReporte(string fechaSeleccionada);
        Respuesta EmpleadosReporte();
        Respuesta ConsultarTiposDocumento();
        Respuesta NominaGeneralReporte(string fechaSeleccionada);
        Respuesta ObtenerSolicitudEmpleadoPeriodoReporte(Report report);
        Respuesta ConsultarNombreEmpleado(long EMPLEADO_ID);

    }
}
