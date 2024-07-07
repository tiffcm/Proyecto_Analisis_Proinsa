namespace PROINSA_GP_WEB.Entidad
{
    public class AprobacionCambioHorario
    {
        public long ID_SOLICITUD { get; set; }
        // public string? SOLICITANTE { get; set; }
        public string? COMENTARIO { get; set; }
        public string? DETALLE { get; set; }
        public long ID_HORARIOLABORAL { get; set; }
        public string? HORARIO { get; set; }
        // public int ID_EMPLEADO { get; set; }
        public long? SOLICITANTE_ID { get; set; }


    }
}
