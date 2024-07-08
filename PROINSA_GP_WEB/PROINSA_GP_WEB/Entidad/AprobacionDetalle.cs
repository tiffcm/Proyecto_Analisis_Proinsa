namespace PROINSA_GP_WEB.Entidad
{
    public class AprobacionDetalle
    {
        public long ID_SOLICITUD { get; set; }
        public long ID_EMPLEADO { get; set; }
        public int IDENTIFICACION { get; set; }
        public string? SOLICITANTE { get; set; }
        public string? TIPOPERMISO { get; set; }
        public int DIAS { get; set; }
        public string? COMENTARIO { get; set; }
        public string? JUSTIFICACION { get; set; }
        public string? RESPUESTASOLICITUD { get; set; }
        public string? DETALLE { get; set; }
        public string? HORARIO_NUEVO { get; set; }
        public string? HORARIO_ACTUAL { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FINAL { get; set; }
    }
}
