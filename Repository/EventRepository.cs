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
    public class EventRepository : RepositoryBase
    {
        public EventRepository(string connectionStrings) : base(connectionStrings)
        {

        }

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

        public async Task<string> DeleteEvent(int Id)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;


            await ExecuteSP("[dbo].[sp_removeEvent]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@EventID", Id);
                parameters.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }

        public async Task<IEnumerable<Events>> ViewEvents()
        {
            var dataMapper = new CollectionDataMapper<Events>();
            await ExecuteSP("sp_viewEvent", (SqlParameterCollection parameter) =>
            {
            }, dataMapper);
            var events = dataMapper.Data;
            return events;
        }

        public async Task<Events> EventByID(int eventID)
        {
            var dataMapper = new IDataMapper<Events>();
            await ExecuteSP("sp_viewEventbyId", async parameter =>
            {
                parameter.AddWithValue("@EventID", eventID);
            }, dataMapper);
            return dataMapper.Data;
        }

        public async Task<List<Coach>> GetRegistrationData(int id)
        {
            var dataMapper = new CollectionDataMapper<Coach>();
            await ExecuteSP("sp_ViewRegistration", async parameter =>
            {
                parameter.AddWithValue("@EventID", id);
            }, dataMapper);
            return dataMapper.Data;
        }

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

        public async Task<List<Events>> GetEventsByCoachId(int coachId)
        {
            var dataMapper = new CollectionDataMapper<Events>();
            await ExecuteSP("[sp_GetEventIdByCoachID]", async parameter =>
            {
                parameter.AddWithValue("CoachID", coachId);
            }, dataMapper);
            return dataMapper.Data;
        }

        public async Task<List<Registeration>> ListIncharge()
        {
            var dataMapper = new CollectionDataMapper<Registeration>();
            await ExecuteSP("sp_ChooseCoach", async p =>
            {
            }, dataMapper);
            return dataMapper.Data;
        }
    }
}
