using Microsoft.Data.SqlClient;

namespace BasketballAcademy.Repository
{
    /// <summary>
    /// Base class for managing SqlConnection in the repository classes.
    /// </summary>
    public class Connection
    {
        protected SqlConnection SqlConnection { get; private set; }
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor that initializes a new instance of the Connection class.
        /// </summary>
        /// <param name="configuration">The IConfiguration instance to retrieve connection string.</param>
        public Connection(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("connect");
            SqlConnection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Opens the database connection if it is not already open.
        /// </summary>
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

        /// <summary>
        /// Closes the database connection if it is not already closed.
        /// </summary>
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
