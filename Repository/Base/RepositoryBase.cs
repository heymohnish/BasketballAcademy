using BasketballAcademy.Repository.Interface;
using System.Data.SqlClient;
using System.Data;

namespace BasketballAcademy.Repository.Base
{
    public class RepositoryBase : IRepository
    {
        private string _connectionStrings;
        public RepositoryBase(string connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        protected async Task ExecuteSP(string spName, Action<SqlParameterCollection> parameterize = null, IDataMapper dataMapper = null, Action<SqlParameterCollection> onCompleted = null)
        {
            using (SqlConnection connection = await BuildSQLConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand(spName, connection))
                {
                    cmd.CommandTimeout = 360;
                    cmd.CommandType = CommandType.StoredProcedure;
                    parameterize?.Invoke(cmd.Parameters);
                    using (SqlDataReader reader = await ExecuteReader(() => cmd.ExecuteReaderAsync()))
                    {
                        if (dataMapper != null)
                            await dataMapper.Map(reader);
                        onCompleted?.Invoke(cmd.Parameters);

                    }
                }
            }
        }

        private async Task<SqlConnection> BuildSQLConnection(string connectionString)
        {
            try
            {
                var connection = new SqlConnection(BuildSqlConnectionString(connectionString));
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string BuildSqlConnectionString(string connectionString)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            sqlConnectionStringBuilder.ConnectTimeout = 120;
            sqlConnectionStringBuilder.ConnectRetryCount = 6;
            sqlConnectionStringBuilder.ConnectRetryInterval = 20;
            return sqlConnectionStringBuilder.ToString();
        }

        private async Task<T> ExecuteReader<T>(Func<Task<T>> executeReader)
        {
            try
            {
                return await executeReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
        }
    }
}
