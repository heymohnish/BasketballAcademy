using BasketballAcademy.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BasketballAcademy.Repository
{
    public class CredentialsRepository : Connection
    {
        protected readonly IConfiguration Configuration;
        public CredentialsRepository(IConfiguration configuration) : base(configuration)
        {
            this.Configuration = configuration;
        }


        public bool Signin(Credentials credentials, out int result, out int id, out string name, out string email)
        {
            try
            {
                    OpenConnection();
                    SqlCommand cmd = new SqlCommand("sp_ValidateUser", SqlConnection);                
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", credentials.Username);
                    cmd.Parameters.AddWithValue("@password", Encrypt(credentials.Password));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = (int)reader["role"];
                            id = (int)reader["ID"];
                            name = (string)reader["fullName"];
                            email = (string)reader["email"];
                            return true;
                        }
                        else
                        {
                            result = 5;
                            id = 0;
                            name = "@#$";
                            email = "wfcw";
                            return false;
                        }
                    }
                
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool Forgot(Forget forget)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("sp_ForgotPassword", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", forget.Username);
                    cmd.Parameters.AddWithValue("@password", Encrypt(forget.Password));
                    cmd.Parameters.AddWithValue("@email", forget.Email);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected >= 1;
            }
            finally
            {
                CloseConnection();
            }
        }

        private string Encrypt(string plaintext)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] clearBytes = Encoding.Unicode.GetBytes(plaintext);
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}
