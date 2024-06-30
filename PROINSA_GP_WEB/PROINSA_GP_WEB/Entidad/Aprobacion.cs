namespace PROINSA_GP_WEB.Entidad
{
    public class Aprobacion
    {
        public long ID_SOLICITUD { get; set; }
        public DateTime FECHA_SOLICITUD { get; set; }
        public string? SOLICITANTE { get; set; }
        public string? IDENTIFICACION { get; set; }
        public string? TIPOPERMISO { get; set; }
        public string? HORARIO { get; set; }
        public int DIAS { get; set; }
        public string? RESPUESTASOLICITUD { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }
        public string? COMENTARIO { get; set; }
        public string? DETALLE { get; set; }
        public string? HORARIONUEVO { get; set; }
        public string? HORARIOACTUAL { get; set; }
        public string? DEPARTAMENTO { get; set; }
        public string? NOMBRECOMPLETO { get; set; }
        public int SECUENCIA { get; set; }
    }
}