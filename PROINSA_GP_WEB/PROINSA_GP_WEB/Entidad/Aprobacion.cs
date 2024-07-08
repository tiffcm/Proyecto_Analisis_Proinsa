namespace PROINSA_GP_WEB.Entidad
{
    public class Aprobacion
    {
        public long? ID_SOLICITUD { get; set; }
        public int IDENTIFICACION { get; set; }
        public string? SOLICITANTE { get; set; }
        public string? TIPOPERMISO { get; set; }
        public string? RESPUESTASOLICITUD { get; set; }
        public DateTime? FECHA_SOLICITUD { get; set; }
    }
}