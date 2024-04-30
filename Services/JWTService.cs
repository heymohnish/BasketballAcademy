using BasketballAcademy.DTOs;
using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using BasketballAcademy.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BasketballAcademy.Services
{
    public class JWTService : Connection,ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
      

        public JWTService(IConfiguration configuration):base(configuration)
        {
            _configuration = configuration;
        }

        public string EncryptToCiberText(string plainText)
        {
            string passPhrase = Convert.ToString(_configuration["Key:PassPhrase"]);
            string saltValue = Convert.ToString(_configuration["Key:SaltValue"]);
            string hashAlgorithm = "SHA1";
            int passwordIterations = 3;
            string initVector = "@2B3c4D4e6F6g8H9";
            int keySize = 256;

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);


            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);


            PasswordDeriveBytes password = new PasswordDeriveBytes
            (
                passPhrase,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations
            );
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor
            (
                keyBytes,
                initVectorBytes
            );

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream
            (
                memoryStream,
                encryptor,
                CryptoStreamMode.Write
            );

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            return cipherText;
        }

        public TokenResponseDto CreateToken(AuthUser user)
        {
            if (isValidUser(user))
            {
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                //var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("cid", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.username),
                        new Claim(JwtRegisteredClaimNames.Email, user.username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     }),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinute"])),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);

                return (new TokenResponseDto
                {
                    AccessToken = stringToken,
                    Expiration = token.ValidTo,

                });
            }
            return null;
        }

        public bool isValidUser(AuthUser user)
        {
            bool isValid = false;
            long strkey;
            try
            {
                string key = DecryptToPlainText(user.Key);
                if (key.Split("##").Length > 0)
                    strkey = Convert.ToInt64(key.Split("##")[1]);
                else
                    throw new SecurityTokenException("Invalid token");
                // Calculate the difference in milliseconds between the current time and the stored timestamp
                long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                long storedUnixTimestamp = strkey; // Convert milliseconds to seconds
                long differenceInSeconds = (currentTimestamp - storedUnixTimestamp) / 1000;

                int maxAllowedDifference = GetMaxAllowedDifferenceFromDatabase();

                if (differenceInSeconds >= 0 && differenceInSeconds <= maxAllowedDifference)
                {
                    // User is considered valid within the specified time range
                    // Check if the user exists in the database
                    AuthUser validUser = GetUsersDetails(user.username);
                    if (validUser != null)
                        isValid = true;
                    else
                        throw new Exception("User not found");
                }
                else
                    throw new Exception("The provided key has expired");
            }
            catch (Exception ex)
            {
                // Handle exceptions here
            }
            return isValid;
        }

        public string DecryptToPlainText(string cipherText)
        {
            string passPhrase = Convert.ToString(_configuration["Key:PassPhrase"]);
            string saltValue = Convert.ToString(_configuration["Key:SaltValue"]);

            try
            {

                string hashAlgorithm = "SHA1";
                int passwordIterations = 3;
                string initVector = "@2B3c4D4e6F6g8H9";
                int keySize = 256;

                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(
                    passPhrase,
                    saltValueBytes,
                    hashAlgorithm,
                    passwordIterations
                );

                byte[] keyBytes = password.GetBytes(keySize / 8);

                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                        keyBytes,
                        initVectorBytes
                    ))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(
                                memoryStream,
                                decryptor,
                                CryptoStreamMode.Read
                            ))
                            {
                                using (StreamReader streamReader = new StreamReader(cryptoStream))
                                {
                                    return streamReader.ReadToEnd();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return cipherText;
            }
        }

        private int GetMaxAllowedDifferenceFromDatabase()
        {
            try
            {
                OpenConnection();
                int value = 0;
                using (SqlCommand checkCmd = new SqlCommand("[dbo].[sp_APIProperties]", SqlConnection))
                {
                    checkCmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Attempt to parse the retrieved value as an integer
                            if (int.TryParse(reader["Value"].ToString(), out value))
                            {
                                // Parsing successful
                                // The 'value' variable now holds the integer value retrieved from the database
                            }
                            else
                            {
                                // Parsing failed, handle the error or log a message
                            }
                        }
                    }
                }
                return value;
            }

            finally
            {
                CloseConnection();
            }

        }

        AuthUser GetUsersDetails(string userName)
        {
            try
            {
                AuthUser User = null;
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[dbo].[GetUserDetailByName]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", userName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User = new AuthUser
                            {
                                username = reader.GetString(0)
                            };
                        }
                    }
                }
                return User;
            }
            finally
            {
                CloseConnection();
            }
        }

        public TokenResponseDto GetToken(IEnumerable<Claim> claim)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinute"])),
                claims: claim,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResponseDto { AccessToken = tokenString, Expiration = token.ValidTo };
        }

        public void InsertRefreshToken(UserRefreshTokenRequest userRefreshTokenRequest)
        {
            try
            {
                String SqlconString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(SqlconString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("CreateUpdateRefreshToken", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", userRefreshTokenRequest.UserName);
                        command.Parameters.AddWithValue("@RefreshToken", userRefreshTokenRequest.RefreshToken);
                        command.Parameters.AddWithValue("@RefreshTokenExpiry", userRefreshTokenRequest.RefreshTokenExpiry);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
