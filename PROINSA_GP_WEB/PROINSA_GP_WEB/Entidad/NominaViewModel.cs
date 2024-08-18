namespace PROINSA_GP_WEB.Entidad
{
    public class NominaViewModel
    {
        public string? NOMBRE_EMPLEADO { get; set; }
        public DateTime FECHA_NOMINA { get; set; }
        public List<IngresosDeduccionesDetalle>? DetalleNomina { get; set; }

    }
}
