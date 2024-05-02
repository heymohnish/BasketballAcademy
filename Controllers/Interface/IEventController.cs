using BasketballAcademy.Model;
using Microsoft.AspNetCore.Mvc;


namespace BasketballAcademy.Controllers
{
    public interface IEventController
    {
        Task<IActionResult> AddEvents(Events events);
        Task<IActionResult> EditEvents(Events events);
        Task<IActionResult> ViewEvents();
        Task<IActionResult> GetEventByID(int eventID);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> RegisterEvent(RegisterEvent registerEvent);
        Task<IActionResult> ViewRegistration(int eventID);
        Task<IActionResult> ViewMyEvent(int id);
        Task<IActionResult> EventIncharge();
    }
}
