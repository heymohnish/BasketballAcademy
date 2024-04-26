using BasketballAcademy.Model;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using BasketballAcademy.Repository.Base;
using BasketballAcademy.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using BasketballAcademy.Services;

namespace BasketballAcademy.Repository
{
    public class AdminRepository : RepositoryBase
    {
   
        public AdminRepository(string connectionStrings) : base(connectionStrings)
        {
            
        }

        /// <summary>
        /// Adds a new admin to the system.
        /// </summary>
        /// <param name="admin">The Admin object to be added.</param>
        /// <returns>True if the admin is added successfully, false if the username already exists.</returns>
        public async Task<string> AddAdmin(Admin admin)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;

            await ExecuteSP("[dbo].[sp_AddAdmin]", (SqlParameterCollection parameters) =>
                {
                parameters.AddWithValue("@fullName", admin.fullName);
                parameters.AddWithValue("@email", admin.email);
                parameters.AddWithValue("@username", admin.username);
                parameters.AddWithValue("@password", Encrypt(admin.password));
                parameters.Add(outputParameter);
            
            });

            message = outputParameter.Value.ToString();
            return message;
        }


        /// <summary>
        /// Retrieves a list of all admins in the system.
        /// </summary>
        /// <returns>List of Admin objects representing all admins.</returns>
        public async Task<IEnumerable<Admin>> ViewAdmin()
        {
                var dataMapper = new CollectionDataMapper<Admin>();
                await ExecuteSP("[dbo].[sp_viewAdmin]", (SqlParameterCollection parameters) =>
                {
                }, dataMapper);
            
                var admins = dataMapper.Data;
                return admins;
        }


        /// <summary>
        /// Deletes an admin from the system.
        /// </summary>
        /// <param name="id">The ID of the admin to be deleted.</param>
        /// <returns>The number of rows affected (should be 1 if deletion is successful).</returns>

        public async Task<string> DeleteAdmin(int id)
        {
            await ExecuteSP("sp_removeAdmin", (SqlParameterCollection parameters) =>
                {
                parameters.AddWithValue("@ID", id);
            });
            
            return "Deleted";
        }



        ///// <summary>
        ///// Saves a message in the system.
        ///// </summary>
        ///// <param name="contact">The Contact object representing the message.</param>
        ///// <returns>True if the message is saved successfully.</returns>
        public async Task<string> Message(Contact contact)
                        {
            await ExecuteSP("[dbo].[sp_EnterMessage]", (SqlParameterCollection parameters) =>
                {
                parameters.AddWithValue("@Name", contact.Name);
                parameters.AddWithValue("@Email", contact.Email);
                parameters.AddWithValue("@Phone", contact.Phone);
                parameters.AddWithValue("@Message", contact.Message);

            });

            return "message sent successfully";
                }
            
            //        try
            //        {
            //            OpenConnection();
            //            using (SqlCommand cmd = new SqlCommand("sp_EnterMessage", SqlConnection))
            //                {
            //                cmd.CommandType = CommandType.StoredProcedure;
            //                cmd.Parameters.AddWithValue("@Name", contact.Name);
            //                cmd.Parameters.AddWithValue("@Email", contact.Email);
            //                cmd.Parameters.AddWithValue("@Phone", contact.Phone);
            //                cmd.Parameters.AddWithValue("@Message", contact.Message);
            //                int rowsAffected = cmd.ExecuteNonQuery();
            //                return rowsAffected >= 1; 
            //                }
            //        }
            //        finally
            //        {
            //        CloseConnection();
            //        }

            //}

            ///// <summary>
            ///// Retrieves a list of all messages in the system.
            ///// </summary>
            ///// <returns>List of Contact objects representing all messages.</returns>
            public async Task<IEnumerable<Contact>> ViewMessage()
                                {
            var dataMapper = new CollectionDataMapper<Contact>();
            await ExecuteSP("[dbo].[sp_ViewMessage]", (SqlParameterCollection parameters) =>
                {
            }, dataMapper);
            var messages = dataMapper.Data;
            return messages;
                }
            


        ///// <summary>
        ///// Deletes a message from the system.
        ///// </summary>
        ///// <param name="id">The ID of the message to be deleted.</param>
        ///// <returns>The number of rows affected (should be 1 if deletion is successful).</returns>
        public async Task<string> DeleteMessage(int id)
                    {
          await ExecuteSP("sp_DeleteMessage", (SqlParameterCollection parameters) =>
                {
                parameters.AddWithValue("ID", id);
            });
            return "Message Deleted";
        }

        ///// <summary>
        ///// Encrypts the given plaintext using AES encryption.
        ///// </summary>
        ///// <param name="plaintext">The plaintext to be encrypted.</param>
        ///// <returns>The encrypted ciphertext.</returns>
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
