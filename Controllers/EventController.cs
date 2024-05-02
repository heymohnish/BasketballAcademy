using BasketballAcademy.Controllers.Base;
using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : RepositoryApiControllerBase<EventRepository>,IEventController
    {
        private readonly EventRepository _eventRepository;
        private readonly IConfiguration _configuration;

        public EventController(IConfiguration configuration,EventRepository eventRepository):base(eventRepository)
        {
            _configuration = configuration;
            _eventRepository = eventRepository;
    }


        [HttpPost("AddEvents")]
        public async Task<IActionResult> AddEvents(Events events)
        {
            var result=await _eventRepository.AddEvents(events);
            return ApiOkResponse(result);
        }


        [HttpPut("EditEvent")]
        public async Task<IActionResult> EditEvents(Events events)
        {
            var result = await _eventRepository.EditEvents(events);
            return ApiOkResponse(result);
        }

        [HttpGet("ViewEvents")]
        public async Task<IActionResult> ViewEvents()
        {
            return ApiOkResponse(_eventRepository.ViewEvents());
        }


        [HttpGet("ViewEventByID")]
        public async Task<IActionResult> GetEventByID(int EventID)
        {
            var result = await _eventRepository.EventByID(EventID);
            if (result != null)
                return ApiOkResponse(result);
            else
                return StatusCode(404, new { statusCode = 404, message = "Event not found", data = new { }, error = "Event not found" });
        }

        [HttpDelete("DeleteEvent")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _eventRepository.DeleteEvent(Id);
            return ApiOkResponse(result);
        }

         [HttpPost("RegisterEvent")]
        public async Task<IActionResult> RegisterEvent(RegisterEvent registerEvent)
        {
            var result = await _eventRepository.RegisterEvent(registerEvent);
            return ApiOkResponse(result);
        }

        [HttpGet("ViewRegistration")]
        public async Task<IActionResult> ViewRegistration(int EventID)
        {
            var result = await _eventRepository.GetRegistrationData(EventID);
            if (result.Count != 0)
            {
                return ApiOkResponse(result);
            }
            else
                return StatusCode(404, new { statusCode = 404, message = "Event not found", data = new { }, error = "Event not found" });
        }


        [HttpGet("getEventsByCoachID")]
        public async Task<IActionResult> ViewMyEvent(int id)
        {
            var result=await _eventRepository.GetEventsByCoachId(id);
            if(result.Count!=0)
            {
                return ApiOkResponse(result);
            }
            else
                return StatusCode(404, new { statusCode = 404, message = "Coach ID not found", data = new { }, error = "Coach ID not found" });
        }

        [HttpGet("Incharge")]
        public async Task<IActionResult> EventIncharge()
        {
            return ApiOkResponse(await _eventRepository.ListIncharge());
        }
        }
}
