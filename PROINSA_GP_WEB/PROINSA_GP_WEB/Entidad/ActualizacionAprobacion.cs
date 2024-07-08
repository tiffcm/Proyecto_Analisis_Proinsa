namespace PROINSA_GP_WEB.Entidad
{
    public class ActualizacionAprobacion
    {
        public long ID_SOLICITUD { get; set; }
        public long? ID_EMPLEADO { get; set; }        
        public string? RESPUESTASOLICITUD { get; set; }
        public string? COMENTARIO { get; set; }
    }
}
