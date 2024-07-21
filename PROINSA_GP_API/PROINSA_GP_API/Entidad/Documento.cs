namespace PROINSA_GP_API.Entidad
{
    public class Documento
    {
        public long ID_EMPLEADODOCUMENTO {  get; set; }
        public string? NOMBRE_DOCUMENTO { get; set; }
        public string? COMENTARIO {  get; set; }
        public Byte[]? DOCUMENTO {  get; set; }
        public string? VER_DOCUMENTO {  get; set; }
        public string? DESCRIPCION {  get; set; }
        public long? TIPODOCUMENTO_ID { get; set; }
        public long? EMPLEADO_ID { get; set; }        
    }
}
