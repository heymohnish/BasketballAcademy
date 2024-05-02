using BasketballAcademy.Model;
using Microsoft.AspNetCore.Mvc;


namespace BasketballAcademy.Controllers
{
    public interface ICredentialController
    {
        Task<IActionResult> Signin(Credentials credentials);
        Task<IActionResult> ForgotPassword(Forget forget);
    }
}
