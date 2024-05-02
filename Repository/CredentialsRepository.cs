using BasketballAcademy.Model;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BasketballAcademy.Repository.Base;
using BasketballAcademy.Repository.Interface;
using System.Reflection.Metadata;

namespace BasketballAcademy.Repository
{
    public class CredentialsRepository : RepositoryBase
    {
       
        public CredentialsRepository(string conncetion) : base(conncetion)
        {
            
        }


        public async Task<LoginResponse> Signin(Credentials credentials)
        {

            var dataMapper = new IDataMapper<LoginResponse>();
            await ExecuteSP("sp_ValidateUser", async p =>
            {
                p.AddWithValue("username", credentials.Username);
                p.AddWithValue("password", Encrypt(credentials.Password));
              
            },dataMapper);
          return dataMapper.Data;
        }

        public async Task<string> Forgot(Forget forget)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;

            await ExecuteSP("[dbo].[sp_ForgotPassword]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@email", forget.Email);
                parameters.AddWithValue("@username", forget.Username);
                parameters.AddWithValue("@password", Encrypt(forget.Password));
                parameters.Add(outputParameter);

            });

            message = outputParameter.Value.ToString();
            return message;
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
