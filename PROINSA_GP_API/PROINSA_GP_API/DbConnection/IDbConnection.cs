using Microsoft.Data.SqlClient;

namespace PROINSA_GP_API.DbConnection
{
    public interface IDbConnection
    {
        SqlConnection CreateConnection();
    }

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
