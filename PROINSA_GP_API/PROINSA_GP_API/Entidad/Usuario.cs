namespace PROINSA_GP_API.Entidad
{
    /// <summary>
    /// Esta es la entidad que se encargará de traer los datos de los usuarios que
    /// se encuentran registrados en el sistema y también los datos de interés.
    /// </summary>
    /// <author>Brandon Ruiz Miranda</author>
    /// <version>1.0</version>
    public class Usuario
    {
        public long ID_EMPLEADO { get; set; }
        public int? IDENTIFICACION { get; set; }
        public string? NOMBRECOMPLETO { get; set; }
        public string? FOTO { get; set; }
        public string? NOMBRE_CARGO { get; set; }
        public string? CORREO { get; set; }
        public string? NOMBRE_DEPARTAMENTO { get; set; }
        public int IDROL { get; set; }
        public string? NOMBREROL { get; set; }
        public string? DIRRECION { get; set; }
        public string? TELEFONO { get; set; }
    }
}
