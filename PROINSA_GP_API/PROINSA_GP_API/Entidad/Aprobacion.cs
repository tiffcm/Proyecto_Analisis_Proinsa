namespace PROINSA_GP_API.Entidad
{
    public class Aprobacion
    {
        public long? ID_SOLICITUD { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string? SOLICITANTE { get; set; }
        public string? TIPOPERMISO { get; set; }
        public string? RESPUESTASOLICITUD { get; set; }
        public DateTime? FECHA_SOLICITUD { get; set; }
    }
}