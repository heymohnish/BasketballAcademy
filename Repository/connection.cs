using System.Data.SqlClient;

namespace BasketballAcademy.Repository
{

    public class Connection
    {
        protected SqlConnection SqlConnection { get; private set; }
        private readonly IConfiguration _configuration;

         public Connection(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("connect");
            SqlConnection = new SqlConnection(connectionString);
        }

        
        public void OpenConnection()
        {
            try
            {
                if (SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    SqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Error opening connection: {ex.Message}");
                throw;
            }
        }

       
        public void CloseConnection()
        {
            try
            {
                if (SqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    SqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Error closing connection: {ex.Message}");
                throw;
            }
        }

    }
}
