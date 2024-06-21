using Microsoft.Data.SqlClient;

namespace PROINSA_GP_API.DbConnection
{
    /// <summary>
    ///  Esta interfaz se encargará de crear una conexión más sofisticada
    ///  para no exponer la cadena de conexión a la red.
    /// </summary>
    /// <author>Brandon Ruiz Miranda</author>
    /// <version>1.0</version>
    public interface IDbConnection
    {
        SqlConnection CreateConnection();
    }

    /// <summary>
    /// Se crea el SqlDbConnection para almaacenar la cadena de conexión
    /// </summary>
    public class SqlDbConnection : IDbConnection
    {
        private readonly string _connectionString;

        public SqlDbConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Db_Connection")
                              ?? throw new ArgumentNullException(nameof(_connectionString), "Connection string cannot be null");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
