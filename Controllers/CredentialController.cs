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

namespace BasketballAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialController : RepositoryApiControllerBase<CredentialsRepository>
    {
        private readonly CredentialsRepository _credentialsRepository;
        private readonly IConfiguration _configuration;

        public CredentialController(IConfiguration configuration,CredentialsRepository credentialsRepository):base(credentialsRepository)
        {
            _credentialsRepository= credentialsRepository;
                _configuration= configuration;
        }

        /// <summary>
        /// Handles user sign-in.
        /// </summary>
        /// <param name="credentials">Credentials object containing user login information.</param>
        /// <returns>Action result with user role, name, and ID if sign-in is successful; otherwise, returns an error message.</returns>
        [HttpPost("signin")]
        public async Task<IActionResult> Signin(Credentials credentials)
        {
            var result = await _credentialsRepository.Signin(credentials); 
            if(result!=null)
            {
                return ApiOkResponse(result);
            }
            else
            { 
                return StatusCode(404, new { statusCode = 404, message = "User not found", data = new { }, error = "User not found" });
             }
    }


        /// <summary>
        /// Handles the password change request.
        /// </summary>
        /// <param name="forget">Forget object containing user information for password change.</param>
        /// <returns>1 if password changed successfully, 0 if an error occurs.</returns>
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
