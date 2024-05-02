using BasketballAcademy.Model;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    public interface ICoachController
    {
        Task<IActionResult> AddCoach(Admin admin);
        Task<IActionResult> ViewCoach();
        Task<IActionResult> UpdateCoach(Coach coach);
        Task<IActionResult> Delete(int id);
    }
}
