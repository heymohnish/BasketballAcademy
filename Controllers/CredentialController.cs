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

namespace BasketballAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialController : ControllerBase
    {
        private readonly CredentialsRepository Repository;

        public CredentialController(IConfiguration configuration)
        {
            Repository = new CredentialsRepository(configuration);
        }

        /// <summary>
        /// Handles user sign-in.
        /// </summary>
        /// <param name="credentials">Credentials object containing user login information.</param>
        /// <returns>Action result with user role, name, and ID if sign-in is successful; otherwise, returns an error message.</returns>
        [HttpPost]
        [Route("signin")]
        public ActionResult Signin(Credentials credentials)
        {
            try
            {
                bool temp = Repository.Signin(credentials, out int result, out int id, out string name, out string email);

                if (temp)
                {
                    if (result == 0)
                    {
                        logger.LogInfo("Signed in: {Username}", name);
                        return Ok(new { Role = "Admin", name, id });
                    }
                    else if (result == 2)
                    {
                        logger.LogInfo("Signed in: {Username}", name);
                        return Ok(new { Role = "Coach", name, id, email });
                    }
                    else if (result == 3)
                    {
                        logger.LogInfo("Signed in: {Username}", name);
                        return Ok(new { Role = "Player", name, id, email });
                    }
                    else
                    {
                        logger.LogInfo("Signed in: {Username}", name);
                        return Ok(new { Role = "Unknown", name, id });
                    }
                }
                else
                {
                    return Ok(new { Role = "Unknown", name, id });
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return BadRequest("An error occurred during signin");
            }
        }

        /// <summary>
        /// Handles the password change request.
        /// </summary>
        /// <param name="forget">Forget object containing user information for password change.</param>
        /// <returns>1 if password changed successfully, 0 if an error occurs.</returns>
        [HttpPost]
        [Route("ChangePassword")]
        public int ForgotPassword(Forget forget)
        {
            try
            {
                if (Repository.Forgot(forget))
                {
                    string name = forget.Username;
                    logger.LogInfo("Password changed for user: " + name, name);
                    return 1; 
                }
                else
                {
                    return 0; 
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return 0; 
            }
        }
    }
}
