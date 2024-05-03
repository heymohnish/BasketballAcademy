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

        public TokenResponseDto CreateToken()
        {
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                return (new TokenResponseDto
                {
                    AccessToken = stringToken
                });
        }
    }
}
