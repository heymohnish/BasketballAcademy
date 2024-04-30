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
    public class EventController : RepositoryApiControllerBase<EventRepository>
    {
        private readonly EventRepository _eventRepository;
        private readonly IConfiguration _configuration;

        public EventController(IConfiguration configuration,EventRepository eventRepository):base(eventRepository)
        {
            _configuration = configuration;
            _eventRepository = eventRepository;
    }

        /// <summary>
        /// Adds a new event.
        /// </summary>
        /// <param name="events">Events object containing event information.</param>
        /// <returns>1 if event added successfully, 0 if an error occurs.</returns>
        [HttpPost("AddEvents")]
        public async Task<IActionResult> AddEvents(Events events)
        {
            var result=await _eventRepository.AddEvents(events);
            return ApiOkResponse(result);
        }


        /// <summary>
        /// Edits an existing event.
        /// </summary>
        /// <param name="events">Events object containing updated event information.</param>
        /// <param name="evenID">ID of the event to be edited.</param>
        /// <returns>1 if event edited successfully, 0 if an error occurs.</returns>
        [HttpPut("EditEvent")]
        public async Task<IActionResult> EditEvents(Events events)
        {
            var result = await _eventRepository.EditEvents(events);
            return ApiOkResponse(result);
        }


        /// <summary>
        /// Retrieves a list of all events.
        /// </summary>
        /// <returns>List of Events objects representing events.</returns>
        [HttpGet("ViewEvents")]
        public async Task<IActionResult> ViewEvents()
        {
            return ApiOkResponse(_eventRepository.ViewEvents());
        }

        /// <summary>
        /// Retrieves event information by event ID.
        /// </summary>
        /// <param name="eventID">ID of the event to retrieve.</param>
        /// <returns>List of Events objects representing the specified event.</returns>
        [HttpGet("ViewEventByID")]
        public async Task<IActionResult> GetEventByID(int EventID)
        {
            var result = await _eventRepository.EventByID(EventID);
            if (result != null)
                return ApiOkResponse(result);
            else
                return StatusCode(404, new { statusCode = 404, message = "Event not found", data = new { }, error = "Event not found" });
        }


        /// <summary>
        /// Retrieves a list of events for the home page.
        /// </summary>
        /// <returns>List of Events objects representing events for the home page.</returns>
        //[HttpGet("HomeEvent")]
        //public async Task<IActionResult> HomeEvent()
        //{
        //    return ApiOkResponse(await _eventRepository)
        //    try
        //    {
        //        List<Events> events = Repository.ViewEvents();
        //        return events;
        //    }
        //    catch (Exception exception)
        //    {
        //        logger.LogError(exception);
        //        return new List<Events>();
        //    }
        //}

        /// <summary>
        /// Deletes an event by ID.
        /// </summary>
        /// <param name="Id">ID of the event to be deleted.</param>
        [HttpDelete("DeleteEvent")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _eventRepository.DeleteEvent(Id);
            return ApiOkResponse(result);
        }

        /// <summary>
        /// Registers a coach for a specific event.
        /// </summary>
        /// <param name="id">ID of the event.</param>
        /// <param name="coachid">ID of the coach.</param>
        /// <returns>1 if registration successful, 0 if an error occurs, -1 if an exception occurs.</returns>
        [HttpPost("RegisterEvent")]
        public async Task<IActionResult> RegisterEvent(RegisterEvent registerEvent)
        {
            var result = await _eventRepository.RegisterEvent(registerEvent);
            return ApiOkResponse(result);
        }

        /// <summary>
        /// Retrieves registration data for a specific event.
        /// </summary>
        /// <param name="id">ID of the event for which registration data is to be viewed.</param>
        /// <returns>List of Registeration objects representing registration data.</returns>
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

        /// <summary>
        /// Retrieves events associated with a coach.
        /// </summary>
        /// <param name="id">ID of the coach for whom events are to be viewed.</param>
        /// <returns>List of Events objects representing events associated with the coach.</returns>
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


        /// <summary>
        /// Retrieves a list of events for which a coach is in charge.
        /// </summary>
        /// <returns>List of Registeration objects representing events in charge.</returns>
        [HttpGet("Incharge")]
        public async Task<IActionResult> EventIncharge()
        {
            return ApiOkResponse(await _eventRepository.ListIncharge());
        }
        }
}
