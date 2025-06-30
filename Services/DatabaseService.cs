using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace Prueba_Tecnica_Italika.Services
{
    public class DatabaseService
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DatabaseService(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        // Para INSERT, UPDATE, DELETE
        public async Task<int> ExecuteNonQueryAsync(string storedProcedure, Action<SqlParameterCollection> addParameters)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(storedProcedure, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            addParameters(cmd.Parameters);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        // Para obtener datos (SELECT, por ejemplo para consultas)
        public async Task<DataTable> ExecuteQueryAsync(string storedProcedure, Action<SqlParameterCollection> addParameters = null)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(storedProcedure, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            addParameters?.Invoke(cmd.Parameters);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }
    }
}
