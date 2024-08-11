using PROINSA_GP_WEB.Entidad;

namespace PROINSA_GP_WEB.Servicios
{
    public interface INominaModel
    {
        Respuesta? RegistrarNomina(Nomina entidad);
        Respuesta? CalculoNominaInicial(Nomina entidad);
        Respuesta? CalculoNominaFinal(Nomina entidad);
        Respuesta? ConsultarTiposNomina();
        Respuesta? RegistrarIngresosNominaDetalle(Nomina entidad);
        Respuesta? RegistrarDeduccionNominaDetalle(Nomina entidad);
        Respuesta? ObtenerNominaEmpleado(int EMPLEADO_ID);
        Respuesta? ObtenerNominaMensualEmpleados(DateTime fechapago);
        Respuesta? RevisionNomina(Nomina entidad);
        Respuesta? AprobacionNomina(Nomina entidad);
    }
}
