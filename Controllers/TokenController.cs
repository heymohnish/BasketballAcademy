using BasketballAcademy.DTOs;
using BasketballAcademy.Model;
using BasketballAcademy.Model.Core;
using BasketballAcademy.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JWTSettings _jWTSettings;
        private readonly ITokenService _tokenService;

        public TokenController(JWTSettings jWTSettings, ITokenService tokenService)
        {
            _jWTSettings = jWTSettings;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("InstantKeyGenerate")]
        public IActionResult ShareKey()
        {
            try
            {
                Guid uuid = Guid.NewGuid();
                string shortUuid = uuid.ToString().Substring(0, 2);
                // Get Unix timestamp
                long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                // Combine UUID and Unix timestamp
                string combinedId = $"{uuid}##{unixTimestamp}";

                // Encrypt the combinedId
                //string encryptedId = EncryptToCiberText(combinedId);

                return Ok("Key = " + combinedId + ", Encrypted Key = " + _tokenService.EncryptToCiberText(combinedId));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("GetToken")]
        public IActionResult GenerateToken([FromBody] AuthUser userModel)
        {
            var token = _tokenService.CreateToken(userModel);
           
            if (token == null)
                return BadRequest("User not found");

            return Ok(new TokenResponseDto
            {
                AccessToken = token.AccessToken,
                Expiration = token.Expiration,
            });
        }
    }
}
