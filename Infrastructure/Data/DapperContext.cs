using System.Data.SqlClient;

namespace Infrastructure.Data
{
    public class DapperContext
    {        
        private readonly string _connectionString;
        public DapperContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
