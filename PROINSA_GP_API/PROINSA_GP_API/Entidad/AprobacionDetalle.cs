namespace PROINSA_GP_API.Entidad
{
    public class AprobacionDetalle
    {
        public long ID_SOLICITUD {  get; set; }
        public int ID_EMPLEADO { get; set; }
        public string? SOLICITANTE { get; set; }
        public string? TIPOPERMISO { get; set; }
        public int? DIAS {  get; set; }
        public string? COMENTARIO { get; set; }
        public string? DETALLE {  get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FINAL {  get; set; }
    }
}
