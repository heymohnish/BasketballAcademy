using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventRepository Repository;

        public EventController(IConfiguration configuration)
        {
            Repository = new EventRepository(configuration);
        }

        string name;
        /// <summary>
        /// Adds a new event.
        /// </summary>
        /// <param name="events">Events object containing event information.</param>
        /// <returns>1 if event added successfully, 0 if an error occurs.</returns>
        [HttpPost]
        [Route("AddEvents")]
        public int AddEvents(Events events)
        {
            try
            {
                name = events.EventName;
                logger.LogInfo(name + " new event added", name);

                Repository.AddEvents(events);
                return 1;             
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return 0;
            }
        }

        /// <summary>
        /// Edits an existing event.
        /// </summary>
        /// <param name="events">Events object containing updated event information.</param>
        /// <param name="evenID">ID of the event to be edited.</param>
        /// <returns>1 if event edited successfully, 0 if an error occurs.</returns>
        [HttpPut]
        [Route("EditEvent/{evenID}")]
        public int EditEvents(Events events, int evenID)
        {
            try
            {
                Repository.EditEvents(events, evenID);
                return 1;              
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return 0; 
            }
        }

        /// <summary>
        /// Retrieves a list of all events.
        /// </summary>
        /// <returns>List of Events objects representing events.</returns>
        [HttpGet]
        [Route("ViewEvents")]
        public List<Events> ViewEvents()
        {
            try
            {
                List<Events> events = Repository.ViewEvents();
                return events;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Events>();
            }
        }

        /// <summary>
        /// Retrieves event information by event ID.
        /// </summary>
        /// <param name="eventID">ID of the event to retrieve.</param>
        /// <returns>List of Events objects representing the specified event.</returns>
        [HttpGet]
        [Route("EventByID/{eventID}")]
        public List<Events> GetEventByID(int eventID)
        {
            try
            {
                List<Events> events = Repository.EventByID(eventID);
                return events;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Events>();
            }
        }

        /// <summary>
        /// Retrieves a list of events for the home page.
        /// </summary>
        /// <returns>List of Events objects representing events for the home page.</returns>
        [HttpGet]
        [Route("HomeEvent")]
        public List<Events> HomeEvent()
        {
            try
            {
                List<Events> events = Repository.ViewEvents();
                return events;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Events>();
            }
        }

        /// <summary>
        /// Deletes an event by ID.
        /// </summary>
        /// <param name="Id">ID of the event to be deleted.</param>
        [HttpDelete]
        [Route("DeleteEvent/{id}")]
        public void Delete(int Id)
        {
            try
            {
                int result = Repository.DeleteEvent(Id);
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
            }
        }

        /// <summary>
        /// Registers a coach for a specific event.
        /// </summary>
        /// <param name="id">ID of the event.</param>
        /// <param name="coachid">ID of the coach.</param>
        /// <returns>1 if registration successful, 0 if an error occurs, -1 if an exception occurs.</returns>
        [HttpPost]
        [Route("RegisterEvent/{id}/{coachid}")]
        public int RegisterEvent(int id, int coachid)
        {
            try
            {
                int Eventid = id, CoachID = coachid;
                logger.LogInfo(id + " event registered by " + CoachID, name);
                bool eventRegistered = Repository.RegisterEvent(id, coachid);
                if (eventRegistered)
                {
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
                return -1; 
            }
        }

        /// <summary>
        /// Retrieves registration data for a specific event.
        /// </summary>
        /// <param name="id">ID of the event for which registration data is to be viewed.</param>
        /// <returns>List of Registeration objects representing registration data.</returns>
        [HttpGet]
        [Route("ViewRegistration/{id}")]
        public List<Registeration> ViewRegistration(int id)
        {
            try
            {
                List<Registeration> registerlist = Repository.GetRegistrationData(id);
                return registerlist;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Registeration>();
            }
        }

        /// <summary>
        /// Retrieves events associated with a coach.
        /// </summary>
        /// <param name="id">ID of the coach for whom events are to be viewed.</param>
        /// <returns>List of Events objects representing events associated with the coach.</returns>
        [HttpGet]
        [Route("ViewMyEvent/{id}")]
        public List<Events> ViewMyEvent(int id)
        {
            try
            {
                List<Events> events = Repository.GetEventsByCoachId(id);
                return events;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Events>();
            }
        }

        /// <summary>
        /// Retrieves a list of events for which a coach is in charge.
        /// </summary>
        /// <returns>List of Registeration objects representing events in charge.</returns>
        [HttpGet]
        [Route("Incharge")]
        public List<Registeration> EventIncharge()
        {
            try
            {
                List<Registeration> incharge = Repository.ListIncharge();
                return incharge;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Registeration>();
            }
        }
    }
}
