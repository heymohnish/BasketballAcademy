using BasketballAcademy.Model;
using BasketballAcademy.Repository.Base;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using BasketballAcademy.Repository.Interface;

namespace BasketballAcademy.Repository
{
    public class CoachRepository : RepositoryBase
    {

        public CoachRepository(string connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<string> RegisterCoach(Admin admin)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;


            await ExecuteSP("[dbo].[sp_addCoach]", (SqlParameterCollection parameters) =>
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

        public async Task<string> DeleteCoach(int Id)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;

            await ExecuteSP("[dbo].[sp_removeCoach]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@ID", Id);
                parameters.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }

        public async Task<string> UpdateCoach(Coach coach)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;


            await ExecuteSP("[dbo].[sp_updateCoach]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@Id", coach.Id);
                parameters.AddWithValue("@FullName", coach.FullName);
                parameters.AddWithValue("@DateOfBirth", coach.DateOfBirth);
                parameters.AddWithValue("@Gender", coach.Gender);
                parameters.AddWithValue("@Address", coach.Address);
                parameters.AddWithValue("@PrimarySkill", coach.PrimarySkill);
                parameters.AddWithValue("@Phone", coach.PhoneNumber);
                parameters.AddWithValue("@Email", coach.Email);
                parameters.AddWithValue("@Experience", coach.Experience);
                parameters.AddWithValue("@photo", coach.photo);
                parameters.AddWithValue("@idproof", coach.idproof);
                parameters.AddWithValue("@CertificateProof", coach.CertificateProof);
                parameters.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }

        public async Task<IEnumerable<Coach>> ViewCoach()
        {
            var dataMapper = new CollectionDataMapper<Coach>();
            await ExecuteSP("sp_viewCoach", (SqlParameterCollection parameter) =>
            {
            }, dataMapper);
            var coach = dataMapper.Data;
            return coach;
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
