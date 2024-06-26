namespace PROINSA_GP_WEB.Entidad
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
        public string? ESTADO { get; set; }
        public string? USER_ID { get; set; }
        public decimal? SALARIO { get; set; }
        public List<Telefono>? TELEFONOS { get; set; }
        public List<Usuario>? USUARIOS { get; set; }
        public List<Cargo>? CARGOS { get; set; }
        public List<Departamento>? DEPARTAMENTOS { get; set; }
        public long ID_CARGO { get; set; } //
        public string? NOMBRE_HL { get; set; }
    }

    public class Telefono
    {
        public long ID_TELEFONO { get; set; }
        public string? TELEFONO { get; set; }
    }

    public class Cargo
    {
        public long ID_CARGO { get; set; }
        public string? NOMBRE_CARGO { get; set; }

    }

    public class Departamento
    {
        public long ID_DEPARTAMENTO { get; set; }
        public string? NOMBRE_DEPARTAMENTO { get; set; }

    }

}