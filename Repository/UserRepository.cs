using BasketballAcademy.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace BasketballAcademy.Repository
{
    public class UserRepository:Connection
    {
        protected readonly IConfiguration Configuration;
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="user">The user object containing registration information.</param>
        /// <returns>True if registration is successful, false if the username already exists.</returns>
        public bool RegisterUser(User user)
        {
                try
                {
                OpenConnection();
                using (SqlCommand checkCmd = new SqlCommand("sp_CheckExistingByUsername", SqlConnection))
                    {
                        checkCmd.CommandType = CommandType.StoredProcedure;
                        checkCmd.Parameters.AddWithValue("@username", user.username);
                        int userCount = (int)checkCmd.ExecuteScalar();
                        if (userCount > 0)
                        {
                            return false;
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("sp_insertUser", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fullName", user.FullName);
                        cmd.Parameters.AddWithValue("@username", user.username);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@role", 3); 
                        cmd.Parameters.AddWithValue("@password", Encrypt(user.Password));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected >= 1;
                    }
                }
                finally
                {
                CloseConnection();
            }
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="ID">The ID of the user to be deleted.</param>
        /// <returns>The number of rows affected (should be 1 if deletion is successful).</returns>
        public int DeleteUser(int ID)
        {
                try
                {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_removeUser", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", ID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
                finally
                {
                CloseConnection();
            }
        }

        /// <summary>
        /// Retrieves a list of users with the role ID 3 (assuming role 3 represents standard users).
        /// </summary>
        /// <returns>A list of User objects.</returns>
        public List<User> ViewUser()
        {
                try
                {
                    OpenConnection();
                    List<User> userlist = new List<User>();
                    SqlCommand cmd = new SqlCommand("sp_viewUser", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@role", 3);
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        userlist.Add(
                            new User
                            {
                                id = Convert.ToInt32(dr["ID"]),
                                FullName = Convert.ToString(dr["FullName"]),
                                username = Convert.ToString(dr["username"]),
                                Email = Convert.ToString(dr["Email"]),
                            });
                    }
                    return userlist;
                }
                finally
                {
                CloseConnection();
            }
        }

        /// <summary>
        /// Encrypts the given plaintext using AES encryption.
        /// </summary>
        /// <param name="plaintext">The text to be encrypted.</param>
        /// <returns>The encrypted text.</returns>
        private string Encrypt(string plaintext)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(plaintext);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    plaintext = Convert.ToBase64String(ms.ToArray());
                }
            }
            return plaintext;
        }
    }
}
