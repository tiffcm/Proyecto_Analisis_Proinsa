using PROINSA_GP_WEB.Entidad;
using System.Data;

namespace PROINSA_GP_WEB.Servicios
{
    public interface IReporteModel
    {
        Respuesta DatosEmpleadoNominaReporte(long EMPLEADO_ID);
        Respuesta DatosNominaEmpleadoDeduccionesReporte(Reporte report);
        Respuesta DatosNominaEmpleadoIngresosReporte(Reporte report);
        Respuesta DatosNominaEmpleadoReporte(Reporte report);
        Respuesta DatosNominaGeneralReporte(string fechaSeleccionada);
        Respuesta EmpleadosReporte();
        Respuesta ConsultarTiposDocumento();
        Respuesta NominaGeneralReporte(string fechaSeleccionada);
        Respuesta ObtenerSolicitudEmpleadoPeriodoReporte(Reporte report);
        Respuesta ConsultarNombreEmpleado(long EMPLEADO_ID);
    }
}
