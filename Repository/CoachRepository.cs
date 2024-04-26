using BasketballAcademy.Model;
using BasketballAcademy.Repository.Base;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace BasketballAcademy.Repository
{
    public class CoachRepository: RepositoryBase
    {

        public CoachRepository(string connectionStrings) : base(connectionStrings)
        {

        }

        /// <summary>
        /// Registers a new coach in the system.
        /// </summary>
        /// <param name="admin">The Admin object containing coach information.</param>
        /// <returns>
        /// True if the coach is successfully registered; otherwise, false.
        /// Returns false if a coach with the same email already exists.
        /// </returns>

        //public async Task<string> RegisterCoach(Admin admin)
        //{
        //    string message = null;
        //    SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
        //    outputParameter.Direction = ParameterDirection.Output;


        //    await ExecuteSP("[dbo].[sp_addCoach]", (SqlParameterCollection parameters) =>
        //    {
        //        parameters.AddWithValue("@fullName", admin.fullName);
        //        parameters.AddWithValue("@email", admin.email);
        //        parameters.AddWithValue("@username", admin.username);
        //        parameters.AddWithValue("@password", Encrypt(admin.password));
        //        parameters.Add(outputParameter);
        //    });

            //try
            //{
            //    OpenConnection();
            //    using (SqlCommand checkCmd = new SqlCommand("sp_CheckExistingByUsername", SqlConnection))
            //    {
            //        checkCmd.CommandType = CommandType.StoredProcedure;
            //        checkCmd.Parameters.AddWithValue("@username", admin.email);
            //        int userCount = (int)checkCmd.ExecuteScalar();
            //        if (userCount > 0)
            //        {
            //            return false;
            //        }
            //    }
            //    using (SqlCommand cmd = new SqlCommand("sp_addCoach", SqlConnection))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@fullName", admin.fullName);
            //        cmd.Parameters.AddWithValue("@username", admin.username);
            //        cmd.Parameters.AddWithValue("@Email", admin.email);
            //        cmd.Parameters.AddWithValue("@role", 2);
            //        cmd.Parameters.AddWithValue("@Password", Encrypt(admin.password));
            //        int rowsAffected = cmd.ExecuteNonQuery();
            //        return rowsAffected >= 1;
            //    }
            //}
            //finally
            //{
            //    CloseConnection();
            //}
        }

        /// <summary>
        /// Deletes a coach with the specified ID from the database.
        /// </summary>
        /// <param name="Id">The ID of the coach to delete.</param>
        /// <returns>The number of rows affected by the deletion.</returns>
        //public int DeleteCoach(int Id)
        //{
        //    try
        //    {
        //        OpenConnection();
        //        using (SqlCommand cmd = new SqlCommand("sp_removeCoach", SqlConnection))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Id", Id);
        //            int i = cmd.ExecuteNonQuery();
        //            return i;
        //        }
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}



        ///// <summary>
        ///// Updates coach information in the database.
        ///// </summary>
        ///// <returns>True if the coach information was updated successfully; otherwise, false.</returns>
        //public bool UpdateCoach(Coach coach, int id)
        //{
        //    try
        //    {
        //        OpenConnection();
        //        using (SqlCommand cmd = new SqlCommand("sp_updateCoach", SqlConnection))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Id", id);
        //            cmd.Parameters.AddWithValue("@FullName", coach.FullName);
        //            cmd.Parameters.AddWithValue("@DateOfBirth", coach.DateOfBirth);
        //            cmd.Parameters.AddWithValue("@age", coach.Age);
        //            cmd.Parameters.AddWithValue("@Gender", coach.Gender);
        //            cmd.Parameters.AddWithValue("@Address", coach.Address);
        //            cmd.Parameters.AddWithValue("@PrimarySkill", coach.PrimarySkill);
        //            cmd.Parameters.AddWithValue("@Phone", coach.PhoneNumber);
        //            cmd.Parameters.AddWithValue("@Email", coach.Email);
        //            cmd.Parameters.AddWithValue("@Experience", coach.Experience);
        //            cmd.Parameters.AddWithValue("@photo", coach.photo);
        //            cmd.Parameters.AddWithValue("@idproof", coach.idproof);
        //            cmd.Parameters.AddWithValue("@CertificateProof", coach.CertificateProof);
        //            int rowsAffected = cmd.ExecuteNonQuery();
        //            return rowsAffected >= 1;
        //        }
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        
        //}

        /// <summary>
        /// Retrieves a list of coaches from the database.
        /// </summary>
        /// <returns>A list of coach objects.</returns>
        //public List<Coach> ViewCoach()
        //{
        //    List<Coach> Coachlist = new List<Coach>();
        //        try
        //        {
        //            OpenConnection();
        //            SqlCommand cmd = new SqlCommand("sp_viewCoach", SqlConnection);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@role", 2);
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Coachlist.Add(
        //                        new Coach()
        //                        {
        //                            Id = Convert.ToInt32(reader["id"]),
        //                            FullName = Convert.ToString(reader["fullName"]),
        //                            Email = Convert.ToString(reader["email"]),
        //                            DateOfBirth = (DateTime)reader["dateOfBirth"],
        //                            Age = Convert.ToInt32(reader["age"]),
        //                            Gender = Convert.ToString(reader["gender"]),
        //                            PhoneNumber = Convert.ToString(reader["phone"]),
        //                            Experience = Convert.ToInt32(reader["experience"]),
        //                            photo = (byte[])reader["photo"],
        //                            idproof = (byte[])reader["idproof"],
        //                            CertificateProof = (byte[])reader["CertificateProof"]
        //                        });
        //                }
        //            }
        //        }
        //        finally
        //        {
        //        CloseConnection();
        //    }
        //    return Coachlist;
        //}

        ///// <summary>
        ///// Encrypts a plaintext string using AES encryption with a predefined encryption key.
        ///// </summary>
        ///// <param name="plaintext">The plaintext string to be encrypted.</param>
        ///// <returns>The encrypted string in base64 format.</returns>
        //private string Encrypt(string plaintext)
        //{
        //    string encryptionKey = "MAKV2SPBNI99212";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(plaintext);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            plaintext = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    return plaintext;
        //}
    }

