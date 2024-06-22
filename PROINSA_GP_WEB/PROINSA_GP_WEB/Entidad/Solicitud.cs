namespace PROINSA_GP_WEB.Entidad
{
    public class Solicitud
    {

        public long? ID { get; set; }
        public string? DESCRIPCION { get; set; }
        public DateTime? FECHA_INICIO { get; set; }
        public DateTime? FECHA_FINAL { get; set; }

        public string? COMENTARIO { get; set; }

        public int? DIAS { get; set; }

        public string? DETALLE { get; set; }

        public string? ESTADO_SOLICITUD { get; set; }

        public long? SOLICITANTE_ID { get; set; }

        public long? TIPOSOLICITUD_ID { get; set; }
    }
}
