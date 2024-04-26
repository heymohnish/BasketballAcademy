using BasketballAcademy.Model;
using BasketballAcademy.Repository.Base;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Reflection.Metadata;
using BasketballAcademy.Repository.Interface;

namespace BasketballAcademy.Repository
{
    public class UserRepository : RepositoryBase
    {
        public UserRepository(string connectionStrings) : base(connectionStrings)
        {

        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="user">The user object containing registration information.</param>
        /// <returns>True if registration is successful, false if the username already exists.</returns>
        public async Task<string> RegisterUser(User user)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@outputMessage", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;

            await ExecuteSP("[sp_insertUser]", (SqlParameterCollection Parameters) =>
            {
                Parameters.AddWithValue("@fullName", user.FullName);
                Parameters.AddWithValue("@username", user.username);
                Parameters.AddWithValue("@email", user.Email);
                Parameters.AddWithValue("@password", Encrypt(user.Password));
                Parameters.Add(outputParameter);
            });

            message = outputParameter.Value.ToString();
            return message;
        }


        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="ID">The ID of the user to be deleted.</param>
        /// <returns>The number of rows affected (should be 1 if deletion is successful).</returns>
        public async Task<string> DeleteUser(int ID)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@outputMessage", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;

            await ExecuteSP("sp_removeUser", (SqlParameterCollection Parameter) =>
            {
                Parameter.AddWithValue("@Id", ID);
                Parameter.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }

        ///// <summary>
        ///// Retrieves a list of users with the role ID 3 (assuming role 3 represents standard users).
        ///// </summary>
        ///// <returns>A list of User objects.</returns>
        public async Task<IEnumerable<User>> ViewUser() 
        {
            var dataMapper = new CollectionDataMapper<User>();
            await ExecuteSP("[dbo].[sp_viewUser]", (SqlParameterCollection parameters) =>
            {
            }, dataMapper);

            var user = dataMapper.Data;
            return user;
        }
            //        try
            //        {
            //            OpenConnection();
            //            List<User> userlist = new List<User>();
            //            SqlCommand cmd = new SqlCommand("sp_viewUser", SqlConnection);
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            cmd.Parameters.AddWithValue("@role", 3);
            //            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            //            DataTable dt = new DataTable();
            //            sd.Fill(dt);
            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                userlist.Add(
            //                    new User
            //                    {
            //                        id = Convert.ToInt32(dr["ID"]),
            //                        FullName = Convert.ToString(dr["FullName"]),
            //                        username = Convert.ToString(dr["username"]),
            //                        Email = Convert.ToString(dr["Email"]),
            //                    });
            //            }
            //            return userlist;
            //        }
            //        finally
            //        {
            //        CloseConnection();
            //    }
            //}

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

