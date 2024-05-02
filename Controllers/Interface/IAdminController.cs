using BasketballAcademy.Model;
using Microsoft.AspNetCore.Mvc;


namespace BasketballAcademy.Controllers
{
    public interface IAdminController
    {
        Task<IActionResult> ViewAdmin();
        Task<IActionResult> AddAdmin(Admin admin);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Contact(Contact contact);
        Task<IActionResult> ViewMessage();
        Task<IActionResult> DeleteMessage(int id);
    }
}
