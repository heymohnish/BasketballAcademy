using BasketballAcademy.Model;
using Microsoft.AspNetCore.Mvc.Diagnostics; 
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace BasketballAcademy.Repository
{
    public class EventRepository:Connection
    {
        protected readonly IConfiguration Configuration;
        public EventRepository(IConfiguration configuration) : base(configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Adds a new event to the database.
        /// </summary>
        /// <param name="events">The event object containing information about the event.</param>
        /// <returns>True if the event is added successfully, otherwise false.</returns>
        public bool AddEvents(Events events)
        {
                try
                {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_addEvent", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventName", events.EventName);
                        cmd.Parameters.AddWithValue("@EventDate", events.EventDate);
                        cmd.Parameters.AddWithValue("@EventTime", events.EventTime);
                        cmd.Parameters.AddWithValue("@Venue", events.Venue);
                        cmd.Parameters.AddWithValue("@Details", events.Details);
                        cmd.Parameters.AddWithValue("@Incharge", events.Incharge);
                        cmd.Parameters.AddWithValue("@AgeGroup", events.AgeGroup);
                        cmd.Parameters.AddWithValue("@Contact", events.Contact);
                        cmd.Parameters.AddWithValue("@EventImage", events.EventImage);
                        cmd.Parameters.AddWithValue("@PrizeDetails", events.PrizeDetails);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                finally
                {
                CloseConnection();
            }
        }

        /// <summary>
        /// Edits an existing event in the database.
        /// </summary>
        /// <param name="events">The event object containing updated information.</param>
        /// <param name="evenID">The ID of the event to be edited.</param>
        /// <returns>True if the event is edited successfully, otherwise false.</returns>
        public bool EditEvents(Events events, int evenID)
        {
                try
                {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_editEvent", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventID", evenID);
                        cmd.Parameters.AddWithValue("@EventName", events.EventName);
                        cmd.Parameters.AddWithValue("@EventDate", events.EventDate);
                        cmd.Parameters.AddWithValue("@EventTime", events.EventTime);
                        cmd.Parameters.AddWithValue("@Venue", events.Venue);
                        cmd.Parameters.AddWithValue("@Details", events.Details);
                        cmd.Parameters.AddWithValue("@Incharge", events.Incharge);
                        cmd.Parameters.AddWithValue("@AgeGroup", events.AgeGroup);
                        cmd.Parameters.AddWithValue("@Contact", events.Contact);
                        cmd.Parameters.AddWithValue("@EventImage", events.EventImage);
                        cmd.Parameters.AddWithValue("@PrizeDetails", events.PrizeDetails);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                finally
                {
                CloseConnection();
            }
        }

        /// <summary>
        /// Deletes an event from the database based on its ID.
        /// </summary>
        /// <param name="Id">The ID of the event to be deleted.</param>
        /// <returns>The number of rows affected (should be 1 if deletion is successful).</returns>
        public int DeleteEvent(int Id)
        {
                try
                {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_removeEvent", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventID", Id);
                        int i = cmd.ExecuteNonQuery();
                        return i;
                    }
                }
                finally
                {
                CloseConnection();
            }
        }

        /// <summary>
        /// Retrieves a list of events from the database.
        /// </summary>
        /// <returns>A list of event objects.</returns>
        public List<Events> ViewEvents()
        {
                try
                {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_viewEvent", SqlConnection))
                    {
                        List<Events> eventlist = new List<Events>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventlist.Add(
                                    new Events
                                    {
                                        EventID = Convert.ToInt32(reader["EventID"]),
                                        EventName = Convert.ToString(reader["EventName"]),
                                        EventDate = (DateTime)reader["EventDate"],
                                        EventTime = Convert.ToString(reader["EventTime"]),
                                        Incharge = Convert.ToString(reader["Incharge"]),
                                        Venue = Convert.ToString(reader["Venue"]),
                                        Details = Convert.ToString(reader["Details"]),
                                        AgeGroup = Convert.ToString(reader["AgeGroup"]),
                                        EventImage = (byte[])reader["EventImage"],
                                        PrizeDetails = Convert.ToString(reader["PrizeDetails"]),
                                        Contact = Convert.ToString(reader["Contact"]),
                                    });
                            }
                        }
                        return eventlist;
                    }
                }
                finally
                {
                CloseConnection();
            }
        }

        /// <summary>
        /// Retrieves information about a specific event based on its ID.
        /// </summary>
        /// <param name="eventID">The ID of the event to retrieve.</param>
        /// <returns>A list containing information about the specified event.</returns>
        public List<Events> EventByID(int eventID)
        {
            List<Events> eventlist = new List<Events>();
                try
                {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_viewEventbyId", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventID", eventID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventlist.Add(
                                    new Events
                                    {
                                        EventID = Convert.ToInt32(reader["EventID"]),
                                        EventName = Convert.ToString(reader["EventName"]),
                                        EventDate = (DateTime)reader["EventDate"],
                                        EventTime = Convert.ToString(reader["EventTime"]),
                                        Incharge = Convert.ToString(reader["Incharge"]),
                                        Venue = Convert.ToString(reader["Venue"]),
                                        Details = Convert.ToString(reader["Details"]),
                                        AgeGroup = Convert.ToString(reader["AgeGroup"]),
                                        EventImage = (byte[])reader["EventImage"],
                                        PrizeDetails = Convert.ToString(reader["PrizeDetails"]),
                                        Contact = Convert.ToString(reader["Contact"]),
                                    });
                            }
                        }
                        return eventlist;
                    }
                }
                finally
                {
                CloseConnection();
            }
        }

        /// <summary>
        /// Retrieves registration data for a specific event based on its ID.
        /// </summary>
        /// <param name="id">The ID of the event for which registration data is requested.</param>
        /// <returns>A list containing registration information for the specified event.</returns>
        public List<Registeration> GetRegistrationData(int id)
        {
            List<Registeration> registerlist = new List<Registeration>();
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_ViewRegistration", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventID", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                registerlist.Add(new Registeration
                                {
                                    Name = Convert.ToString(reader["fullName"])
                                });
                            }
                        }
                    }
                }
                finally
                {
                    CloseConnection();
                }
            return registerlist;
        }

        /// <summary>
        /// Registers a coach for a specific event.
        /// </summary>
        /// <param name="eventId">The ID of the event to register for.</param>
        /// <param name="coachId">The ID of the coach to register.</param>
        /// <returns>True if registration is successful, otherwise false.</returns>
        public bool RegisterEvent(int eventId, int coachId)
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_EventCheckRegistration", SqlConnection))
                        {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventID", eventId);
                        cmd.Parameters.AddWithValue("@CoachID", coachId);
                        int existingRegistrations = (int)cmd.ExecuteScalar();
                        if (existingRegistrations > 0)
                        {
                            return false;
                        }
                        cmd.CommandText = "sp_EventReg";
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                finally
                {
                CloseConnection();
            }
        }

        /// <summary>
        /// Retrieves a list of events associated with a specific coach based on their ID.
        /// </summary>
        /// <param name="coachId">The ID of the coach.</param>
        /// <returns>A list of event objects associated with the specified coach.</returns>
        public List<Events> GetEventsByCoachId(int coachId)
        {
            List<Events> eventlist = new List<Events>();
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_GetEventIdByCoachID", SqlConnection))
                        {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CoachID", coachId);
                        List<int> eventIds = new List<int>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int eventId = Convert.ToInt32(reader["EventID"]);
                                eventIds.Add(eventId);
                            }
                        }
                        using (SqlCommand eventCmd = new SqlCommand("sp_GetEventByEventID", SqlConnection))
                        {
                            eventCmd.CommandType = CommandType.StoredProcedure;
                            foreach (int eventId in eventIds)
                            {
                                eventCmd.Parameters.Clear();
                                eventCmd.Parameters.AddWithValue("@EventID", eventId);
                                using (SqlDataReader reader = eventCmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        eventlist.Add(new Events
                                        {
                                            EventID = Convert.ToInt32(reader["EventID"]),
                                            EventName = Convert.ToString(reader["EventName"]),
                                            EventDate = (DateTime)reader["EventDate"],
                                            EventTime = Convert.ToString(reader["EventTime"]),
                                            Incharge = Convert.ToString(reader["Incharge"]),
                                            Venue = Convert.ToString(reader["Venue"]),
                                            Details = Convert.ToString(reader["Details"]),
                                            AgeGroup = Convert.ToString(reader["AgeGroup"]),
                                            EventImage = (byte[])reader["EventImage"],
                                            PrizeDetails = Convert.ToString(reader["PrizeDetails"]),
                                            Contact = Convert.ToString(reader["Contact"]),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                finally
                {
                CloseConnection();
            }
                return eventlist;
        }

        /// <summary>
        /// Retrieves a list of coaches from the database.
        /// </summary>
        /// <returns>A list of Registeration objects containing coach names.</returns>
        public List<Registeration> ListIncharge()
        {
            List<Registeration> registerlist = new List<Registeration>();
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_ChooseCoach", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                registerlist.Add(new Registeration
                                {
                                    Name = Convert.ToString(reader["fullName"])
                                });
                            }
                        }
                    }
                }
                finally
                {
                    CloseConnection();
                }
            
            return registerlist;
        }
    }
}
