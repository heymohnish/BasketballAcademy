using BasketballAcademy.Model;
using BasketballAcademy.Repository.Base;
using Microsoft.AspNetCore.Mvc.Diagnostics; 
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using BasketballAcademy.Repository.Interface;
using System.Runtime.InteropServices;

namespace BasketballAcademy.Repository
{
    public class EventRepository:RepositoryBase
    {
        public EventRepository(string connectionStrings) : base(connectionStrings)
        {

        }

        /// <summary>
        /// Adds a new event to the database.
        /// </summary>
        /// <param name="events">The event object containing information about the event.</param>
        /// <returns>True if the event is added successfully, otherwise false.</returns>
        public async Task<string> AddEvents(Events events)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;


            await ExecuteSP("[dbo].[sp_addEvent]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@EventName", events.EventName);
                parameters.AddWithValue("@EventDate", events.EventDate);
                parameters.AddWithValue("@EventTime", events.EventTime);
                parameters.AddWithValue("@Venue", events.Venue);
                parameters.AddWithValue("@Details", events.Details);
                parameters.AddWithValue("@Incharge", events.Incharge);
                parameters.AddWithValue("@AgeGroup", events.AgeGroup);
                parameters.AddWithValue("@Contact", events.Contact);
                parameters.AddWithValue("@EventImage", events.EventImage);
                parameters.AddWithValue("@PrizeDetails", events.PrizeDetails);
                parameters.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;

        }


        /// <summary>
        /// Edits an existing event in the database.
        /// </summary>
        /// <param name="events">The event object containing updated information.</param>
        /// <param name="evenID">The ID of the event to be edited.</param>
        /// <returns>True if the event is edited successfully, otherwise false.</returns>
        public async Task<string> EditEvents(Events events)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;


            await ExecuteSP("[dbo].[sp_editEvent]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@EventID", events.EventID);
                parameters.AddWithValue("@EventName", events.EventName);
                parameters.AddWithValue("@EventDate", events.EventDate);
                parameters.AddWithValue("@EventTime", events.EventTime);
                parameters.AddWithValue("@Venue", events.Venue);
                parameters.AddWithValue("@Details", events.Details);
                parameters.AddWithValue("@Incharge", events.Incharge);
                parameters.AddWithValue("@AgeGroup", events.AgeGroup);
                parameters.AddWithValue("@Contact", events.Contact);
                parameters.AddWithValue("@EventImage", events.EventImage);
                parameters.AddWithValue("@PrizeDetails", events.PrizeDetails);
                parameters.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }


        /// <summary>
        /// Deletes an event from the database based on its ID.
        /// </summary>
        /// <param name="Id">The ID of the event to be deleted.</param>
        /// <returns>The number of rows affected (should be 1 if deletion is successful).</returns>
        public async Task<string> DeleteEvent(int Id)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;


            await ExecuteSP("[dbo].[sp_removeEvent]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@EventID",Id);
                parameters.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }
           

            /// <summary>
            /// Retrieves a list of events from the database.
            /// </summary>
            /// <returns>A list of event objects.</returns>
            public async Task<IEnumerable<Events>> ViewEvents()
        {
            var dataMapper = new CollectionDataMapper<Events>();
            await ExecuteSP("sp_viewEvent", (SqlParameterCollection parameter) =>
            {
            },dataMapper);
            var events = dataMapper.Data;
            return events;
        }

        /// <summary>
        /// Retrieves information about a specific event based on its ID.
        /// </summary>
        /// <param name="eventID">The ID of the event to retrieve.</param>
        /// <returns>A list containing information about the specified event.</returns>
        public async Task<Events> EventByID(int eventID)
        {
            var dataMapper=new IDataMapper<Events>();
            await ExecuteSP("sp_viewEventbyId",async parameter =>
            {
                parameter.AddWithValue("@EventID", eventID);
            },dataMapper);
            return dataMapper.Data;
        }


        /// <summary>
        /// Retrieves registration data for a specific event based on its ID.
        /// </summary>
        /// <param name="id">The ID of the event for which registration data is requested.</param>
        /// <returns>A list containing registration information for the specified event.</returns>
        public async Task<List<Coach>> GetRegistrationData(int id)
        {
            var dataMapper=new CollectionDataMapper<Coach>();
            await ExecuteSP("sp_ViewRegistration", async parameter =>
            {
                parameter.AddWithValue("@EventID", id);
            }, dataMapper);
            return dataMapper.Data;
        }
 

            /// <summary>
            /// Registers a coach for a specific event.
            /// </summary>
            /// <param name="eventId">The ID of the event to register for.</param>
            /// <param name="coachId">The ID of the coach to register.</param>
            /// <returns>True if registration is successful, otherwise false.</returns>
            public async Task<string> RegisterEvent(RegisterEvent registerEvent)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;

            await ExecuteSP("[dbo].[sp_EventReg]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@EventID", registerEvent.EventID);
                parameters.AddWithValue("@CoachID", registerEvent.CoachID);
                parameters.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }


        /// <summary>
        /// Retrieves a list of events associated with a specific coach based on their ID.
        /// </summary>
        /// <param name="coachId">The ID of the coach.</param>
        /// <returns>A list of event objects associated with the specified coach.</returns>
        public async Task<List<Events>> GetEventsByCoachId(int coachId)
        {
            var dataMapper= new CollectionDataMapper<Events>();
            await ExecuteSP("[sp_GetEventIdByCoachID]", async parameter =>
            {
                parameter.AddWithValue("CoachID", coachId);
            }, dataMapper);
            return dataMapper.Data; 
        }


        /// <summary>
        /// Retrieves a list of coaches from the database.
        /// </summary>
        /// <returns>A list of Registeration objects containing coach names.</returns>
        public async Task<List<Registeration>> ListIncharge()
        {
            var dataMapper=new CollectionDataMapper<Registeration>();
            await ExecuteSP("sp_ChooseCoach", async p =>
            {
            }, dataMapper);
            return dataMapper.Data;
        }
        }
}
