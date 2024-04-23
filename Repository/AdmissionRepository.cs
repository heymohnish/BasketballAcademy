using BasketballAcademy.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BasketballAcademy.Repository
{
    public class AdmissionRepository:Connection
    {
        protected readonly IConfiguration Configuration;
        public AdmissionRepository(IConfiguration configuration) : base(configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Processes the admission form for a player, checking for existing records, coach availability, and enrolling the player.
        /// </summary>
        /// <param name="admission">The Admission object containing player information.</param>
        /// <returns>
        /// -1 if the player with the same email already exists.
        /// -2 if the selected coach is not available.
        ///  0 if the enrollment fails for any other reason.
        ///  1 if the player is successfully enrolled.
        /// </returns>
        public int AdmissionForm(Admission admission)
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmdCheckUser = new SqlCommand("sp_CheckExisting", SqlConnection))
                        {
                        cmdCheckUser.CommandType = CommandType.StoredProcedure;
                        cmdCheckUser.Parameters.AddWithValue("@Email", admission.Email);
                        int userCount = (int)cmdCheckUser.ExecuteScalar();
                        if (userCount > 0)
                        {
                            return -1;
                        }
                    }
                    using (SqlCommand cmd1 = new SqlCommand("sp_CoachAvailability", SqlConnection))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@CoachID", admission.CoachID);
                        int count = (int)cmd1.ExecuteScalar();
                        if (count > 4)
                        {
                            return -2;
                        }
                        using (SqlCommand cmd = new SqlCommand("sp_EnrollPlayer", SqlConnection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", admission.Id);
                            cmd.Parameters.AddWithValue("@CoachID", admission.CoachID);
                            cmd.Parameters.AddWithValue("@Photo", admission.photo);
                            cmd.Parameters.AddWithValue("@FullName", admission.FullName);
                            cmd.Parameters.AddWithValue("@DateOfBirth", admission.DateOfBirth);
                            cmd.Parameters.AddWithValue("@Age", admission.Age);
                            cmd.Parameters.AddWithValue("@Gender", admission.Gender);
                            cmd.Parameters.AddWithValue("@Phone", admission.PhoneNumber);
                            cmd.Parameters.AddWithValue("@Email", admission.Email);
                            cmd.Parameters.AddWithValue("@ChooseMonth", admission.ChooseMonths);
                            cmd.Parameters.AddWithValue("@ChooseCoach", admission.Coach);
                            cmd.Parameters.AddWithValue("@ParentGuardianName", admission.ParentGuardianName);
                            cmd.Parameters.AddWithValue("@ParentGuardianPhone", admission.ParentGuardianPhone);
                            cmd.Parameters.AddWithValue("@Payment", admission.Payment);
                            cmd.Parameters.AddWithValue("@Status", 0);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }
                finally
                {
                CloseConnection();
            }
            
        }


        /// <summary>
        /// Updates the profile information of a player in the database.
        /// </summary>
        /// <param name="admission">The Admission object containing updated profile information.</param>
        /// <returns>True if the profile information was successfully updated; otherwise, false.</returns>
        public bool UpdateProfile(Player player)
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_updatePlayer", SqlConnection))
                        {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", player.Id);
                        cmd.Parameters.AddWithValue("@FullName", player.FullName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", player.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Age", player.Age);
                        cmd.Parameters.AddWithValue("@Gender", player.Gender);
                        cmd.Parameters.AddWithValue("@Phone", player.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Email", player.Email);
                        cmd.Parameters.AddWithValue("@ParentGuardianName", player.ParentGuardianName);
                        cmd.Parameters.AddWithValue("@ParentGuardianPhone", player.ParentGuardianPhone);
                        cmd.Parameters.AddWithValue("@Photo", player.photo);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected >= 1;
                    }
                }
                finally
                {
                CloseConnection();
            
            }
        }


        /// <summary>
        /// Retrieves a list of player admission records from the database.
        /// </summary>
        /// <returns>A list of Admission objects representing player admission records.</returns>
        public List<Admission> ViewPlayer()
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_viewPlayers", SqlConnection))
                        {
                        List<Admission> playerlist = new List<Admission>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);
                     
                        foreach (DataRow dr in dt.Rows)
                        {
                            playerlist.Add(
                                new Admission
                                {
                                    Id = Convert.ToInt32(dr["ID"]),
                                    FullName = Convert.ToString(dr["FullName"]),
                                    Email = Convert.ToString(dr["Email"]),
                                    DateOfBirth = (DateTime)dr["DateOfBirth"],
                                    Age = Convert.ToInt32(dr["Age"]),
                                    Gender = Convert.ToString(dr["Gender"]),
                                    PhoneNumber = Convert.ToString(dr["Phone"]),
                                    ChooseMonths = Convert.ToString(dr["ChooseMonth"]),
                                    Coach = Convert.ToString(dr["ChooseCoach"]),
                                    ParentGuardianName = Convert.ToString(dr["ParentGuardianName"]),
                                    ParentGuardianPhone = Convert.ToString(dr["ParentGuardianPhone"]),
                                    Payment = Convert.ToString(dr["Payment"]),
                                    status = Convert.ToInt32(dr["Status"])
                                });
                        }
                        return playerlist;
                    }
                }
                finally
                {
                CloseConnection();
            
            }
        }


        /// <summary>
        /// Retrieves a list of player profiles from the database.
        /// </summary>
        /// <returns>A list of Admission objects representing player profiles.</returns>
        public List<Admission> ViewEnrolledPlayer()
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_viewEnrolledPlayer", SqlConnection))
                       {
                        List<Admission> playerlist = new List<Admission>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Status", 1);
                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            playerlist.Add(
                                new Admission
                                {
                                    Id = Convert.ToInt32(dr["ID"]),
                                    FullName = Convert.ToString(dr["FullName"]),
                                    Email = Convert.ToString(dr["Email"]),
                                    DateOfBirth = (DateTime)dr["DateOfBirth"],
                                    Age = Convert.ToInt32(dr["Age"]),
                                    Gender = Convert.ToString(dr["Gender"]),
                                    PhoneNumber = Convert.ToString(dr["Phone"]),
                                    ChooseMonths = Convert.ToString(dr["ChooseMonth"]),
                                    Coach = Convert.ToString(dr["ChooseCoach"]),
                                    ParentGuardianName = Convert.ToString(dr["ParentGuardianName"]),
                                    ParentGuardianPhone = Convert.ToString(dr["ParentGuardianPhone"]),
                                    Payment = Convert.ToString(dr["Payment"]),
                                    status = Convert.ToInt32(dr["Status"]),
                                });
                        }
                        return playerlist;
                    }
                }
                finally
                {
                CloseConnection();
                }
        }

        /// <summary>
        /// Change the status of the admission player enrolled
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateState(int id, int status)
        {
                try
                {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_ChangeStatus", SqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                finally
                {
                CloseConnection();
            }
        }


        /// <summary>
        /// Deletes a player from the database by their ID.
        /// </summary>
        /// <param name="Id">The ID of the player to delete.</param>
        /// <returns>The number of rows affected by the deletion (usually 1 if successful).</returns>
        public int DeletePlayer(int Id)
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_removePlayer", SqlConnection))
                        {                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
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
        /// Retrieves a list of coaches from the database.
        /// </summary>
        /// <returns>A list of Coach objects representing coaches.</returns>
        public List<Coach> CoachList()
        {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_CoachList", SqlConnection))
                        {
                        List<Coach> coachlist = new List<Coach>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("role", 2);
                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            coachlist.Add(
                                new Coach
                                {
                                    Id = Convert.ToInt32(dr["id"]),
                                    FullName = Convert.ToString(dr["fullName"]),
                                    photo = dr.Field<byte[]>("photo"),
                                    Experience = Convert.ToInt32(dr["Experience"]),
                                    PrimarySkill = Convert.ToString(dr["PrimarySkill"]),

                                });
                        }
                        return coachlist;
                    }
                }
                finally
                {
                CloseConnection();
            }
        }



        /// <summary>
        /// Retrieves a list of players by coach name from the database.
        /// </summary>
        /// <param name="name">The name of the coach whose players are to be retrieved.</param>
        /// <returns>A list of Admission objects representing players.</returns>
        public List<Admission> PlayerList(string name)
        {
                try
                {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_ListPlayers", SqlConnection))
                    {
                        List<Admission> playerlist = new List<Admission>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ChooseCoach", name);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                playerlist.Add(
                                new Admission
                                {
                                    FullName = Convert.ToString(reader["FullName"]),
                                    photo = (byte[])reader["Photo"],
                                    Gender = Convert.ToString(reader["Gender"]),
                                    Age = Convert.ToInt32(reader["Age"]),

                                });
                            }
                        }
                        return playerlist;
                    }
                }
                finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Retrieves a list of events associated with a player from the database.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <returns>A list of Events objects representing events.</returns>
        public List<Events> GetEventsByPlayer(int playerId)
        {
            List<Events> eventList = new List<Events>();
                try
                 {
                    OpenConnection();
                    using (SqlCommand eventCmd = new SqlCommand("sp_GetEventsPlayerId", SqlConnection))
                        {
                        eventCmd.CommandType = CommandType.StoredProcedure;
                        eventCmd.Parameters.AddWithValue("@id", playerId);
                        using (SqlDataReader reader = eventCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Events newEvent = new Events
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
                                };

                                eventList.Add(newEvent);
                            }
                        }
                    }
                }
                finally
                {
                CloseConnection();
            }
                return eventList;
            }
    }
}
