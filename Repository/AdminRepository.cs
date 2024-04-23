using BasketballAcademy.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace BasketballAcademy.Repository
{
    public class AdminRepository:Connection
    {
        protected readonly IConfiguration Configuration;
        public AdminRepository(IConfiguration configuration) : base(configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Adds a new admin to the system.
        /// </summary>
        /// <param name="admin">The Admin object to be added.</param>
        /// <returns>True if the admin is added successfully, false if the username already exists.</returns>
        public bool AddAdmin(Admin admin)
        {
            try
                {
                    OpenConnection();
                    using (SqlCommand checkCmd = new SqlCommand("sp_CheckExistingByUsername", SqlConnection))
                    {
                        checkCmd.CommandType = CommandType.StoredProcedure;
                        checkCmd.Parameters.AddWithValue("@username", admin.email);
                        int userCount = (int)checkCmd.ExecuteScalar();
                        if (userCount > 0)
                        {
                            return false; 
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("sp_addAdmin", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fullName", admin.fullName);
                        cmd.Parameters.AddWithValue("@username", admin.username);
                        cmd.Parameters.AddWithValue("@email", admin.email);
                        cmd.Parameters.AddWithValue("@role", 0);
                        cmd.Parameters.AddWithValue("@password", Encrypt(admin.password));
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
        /// Retrieves a list of all admins in the system.
        /// </summary>
        /// <returns>List of Admin objects representing all admins.</returns>
        public List<Admin> ViewAdmin()
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_viewAdmin", SqlConnection))
                    {
                        List<Admin> adminlist = new List<Admin>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@role", 0);
                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            adminlist.Add(
                                new Admin
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    fullName = Convert.ToString(dr["fullName"]),
                                    username = Convert.ToString(dr["username"]),
                                    email = Convert.ToString(dr["email"]),
                                });
                        }
                        return adminlist;
                    }
                }
                finally
                {
                    CloseConnection() ;
                }
            
        }

        /// <summary>
        /// Deletes an admin from the system.
        /// </summary>
        /// <param name="id">The ID of the admin to be deleted.</param>
        /// <returns>The number of rows affected (should be 1 if deletion is successful).</returns>
        public int DeleteAdmin(int id)
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_removeAdmin", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
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
        /// Saves a message in the system.
        /// </summary>
        /// <param name="contact">The Contact object representing the message.</param>
        /// <returns>True if the message is saved successfully.</returns>
        public bool Message(Contact contact)
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_EnterMessage", SqlConnection))
                        {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", contact.Name);
                        cmd.Parameters.AddWithValue("@Email", contact.Email);
                        cmd.Parameters.AddWithValue("@Phone", contact.Phone);
                        cmd.Parameters.AddWithValue("@Message", contact.Message);
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
        /// Retrieves a list of all messages in the system.
        /// </summary>
        /// <returns>List of Contact objects representing all messages.</returns>
        public List<Contact> ViewMessage()
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_ViewMessage", SqlConnection))
                        {
                        List<Contact> message = new List<Contact>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            message.Add(
                                new Contact
                                {
                                    Id = Convert.ToInt32(dr["ID"]),
                                    Name = Convert.ToString(dr["Name"]),
                                    Email = Convert.ToString(dr["Email"]),
                                    Phone = Convert.ToString(dr["Phone"]),
                                    Message = Convert.ToString(dr["Message"]),
                                });
                        }
                        return message;
                    }
                }
                finally
                {
                CloseConnection();
                }
            
        }

        /// <summary>
        /// Deletes a message from the system.
        /// </summary>
        /// <param name="id">The ID of the message to be deleted.</param>
        /// <returns>The number of rows affected (should be 1 if deletion is successful).</returns>
        public int DeleteMessage(int id)
        {
               try
                {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_DeleteMessage", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
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
        /// Encrypts the given plaintext using AES encryption.
        /// </summary>
        /// <param name="plaintext">The plaintext to be encrypted.</param>
        /// <returns>The encrypted ciphertext.</returns>
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
