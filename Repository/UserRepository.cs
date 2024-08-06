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

        public async Task<IEnumerable<User>> ViewUser()
        {
            var dataMapper = new CollectionDataMapper<User>();
            await ExecuteSP("[dbo].[sp_viewUser]", (SqlParameterCollection parameters) =>
            {
            }, dataMapper);

            var user = dataMapper.Data;
            return user;
        }

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
