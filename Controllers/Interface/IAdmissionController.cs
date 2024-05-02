using BasketballAcademy.Model;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    public interface IAdmissionController
    {
        Task<IActionResult> EnrollPlayer(Admission admission);
        Task<IActionResult> UpdatePlayer(Player player);
        Task<IActionResult> ViewPlayer();
        Task<IActionResult> ViewEnrolledPlayer();
        Task<IActionResult> UpdateStatus(AdmissionStatus admission);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> CoachList();
        Task<IActionResult> PlayerList(string name);
        Task<IActionResult> ViewPlayerEvent(int id);
    }
}
