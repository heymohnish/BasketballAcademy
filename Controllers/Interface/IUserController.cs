using BasketballAcademy.Model;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    public interface IUserController
    {
        Task<IActionResult> AddUser(User user);
        Task<IActionResult> ViewAll();
        Task<IActionResult> Delete(int id);
    }
}
