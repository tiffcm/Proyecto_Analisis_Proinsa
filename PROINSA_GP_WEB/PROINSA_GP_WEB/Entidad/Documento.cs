namespace PROINSA_GP_WEB.Entidad
{
    public class Documento
    {
        public long ID_EMPLEADODOCUMENTO { get; set; }
        public string? NOMBRE_DOCUMENTO { get; set; }
        public string? COMENTARIO { get; set; }
        public Byte[]? DOCUMENTO { get; set; }
        public string? VER_DOCUMENTO { get; set; }
        public long? TIPODOCUMENTO_ID { get; set; }
        public string? NOMBRE_TIPODOCUMENTO { get; set; }
        public long? EMPLEADO_ID { get; set; }
    }
}
