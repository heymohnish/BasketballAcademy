using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Security.Claims;
using System.Text.Json;
using Serilog.Core;
using BasketballAcademy.Controllers.Base;
using BasketballAcademy.Model.Core;
using BasketballAcademy.Services.Interfaces;

namespace BasketballAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialController : RepositoryApiControllerBase<CredentialsRepository>
    {
        private readonly CredentialsRepository _credentialsRepository;
        private readonly IConfiguration _configuration;
        private readonly JWTSettings _jWTSettings;
        private readonly ITokenService _tokenService;

        public CredentialController(JWTSettings jWTSettings, ITokenService tokenService,IConfiguration configuration,CredentialsRepository credentialsRepository):base(credentialsRepository)
        {
            _credentialsRepository= credentialsRepository;
                _configuration= configuration;
            _jWTSettings = jWTSettings;
            _tokenService = tokenService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin(Credentials credentials)
        {
            var result = await _credentialsRepository.Signin(credentials);
            if (result != null)
            {
                var token = _tokenService.CreateToken();
                var response = new { Result = result, Token = token }; 
                return ApiOkResponse(response);
            }

            else
            { 
                return StatusCode(404, new { statusCode = 404, message = "User not found", data = new { }, error = "User not found" });
             }
    }


        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ForgotPassword(Forget forget)
        {
            var result = await _credentialsRepository.Forgot(forget);
            if (result != null)
            {
                return ApiOkResponse(result);
            }
            else
            {
                return StatusCode(404, new { statusCode = 404, message = "Invalid user", data = new { }, error = "Invalid user" });
            }
        }
        }
}
    