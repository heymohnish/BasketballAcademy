using BasketballAcademy.Model;
using BasketballAcademy.Repository.Base;
using System.Data.SqlClient;
using System.Data;
using BasketballAcademy.Repository.Interface;
using System.Numerics;
using System.Security.Cryptography.Xml;

namespace BasketballAcademy.Repository
{
    public class AdmissionRepository:RepositoryBase
    {

        public AdmissionRepository(string connectionStrings) : base(connectionStrings)
        {

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
        public async Task<string> AdmissionForm(Admission admission)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;


            await ExecuteSP("[dbo].[sp_EnrollPlayer]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@ID", admission.Id);
                parameters.AddWithValue("@CoachID", admission.CoachID);
                parameters.AddWithValue("@Photo", admission.photo);
                parameters.AddWithValue("@FullName", admission.FullName);
                parameters.AddWithValue("@DateOfBirth", admission.DateOfBirth);
                parameters.AddWithValue("@Age", admission.Age);
                parameters.AddWithValue("@Gender", admission.Gender);
                parameters.AddWithValue("@Phone", admission.PhoneNumber);
                parameters.AddWithValue("@Email", admission.Email);
                parameters.AddWithValue("@ChooseMonth", admission.ChooseMonths);
                parameters.AddWithValue("@ChooseCoach", admission.Coach);
                parameters.AddWithValue("@ParentGuardianName", admission.ParentGuardianName);
                parameters.AddWithValue("@ParentGuardianPhone", admission.ParentGuardianPhone);
                parameters.AddWithValue("@Payment", admission.Payment);
                parameters.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }





        /// <summary>
        /// Updates the profile information of a player in the database.
        /// </summary>
        /// <param name="admission">The Admission object containing updated profile information.</param>
        /// <returns>True if the profile information was successfully updated; otherwise, false.</returns>
        public async Task<string> UpdateProfile(Player player)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;


            await ExecuteSP("[dbo].[sp_updatePlayer]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@ID", player.Id);
                parameters.AddWithValue("@Photo", player.photo);
                parameters.AddWithValue("@FullName", player.FullName);
                parameters.AddWithValue("@DateOfBirth", player.DateOfBirth);
                parameters.AddWithValue("@Age", player.Age);
                parameters.AddWithValue("@Gender", player.Gender);
                parameters.AddWithValue("@Phone", player.PhoneNumber);
                parameters.AddWithValue("@Email", player.Email);
                parameters.AddWithValue("@ParentGuardianName", player.ParentGuardianName);
                parameters.AddWithValue("@ParentGuardianPhone", player.ParentGuardianPhone);
                parameters.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }


        /// <summary>
        /// Retrieves a list of player admission records from the database.
        /// </summary>
        /// <returns>A list of Admission objects representing player admission records.</returns>
        public async Task<List<Admission>> ViewPlayer()
        {
            var dataMapper = new CollectionDataMapper<Admission>();
            await ExecuteSP("sp_viewPlayers", (SqlParameterCollection parameter) =>
            {

            }, dataMapper);
            var players = dataMapper.Data;
            return players;
        }



        /// <summary>
        /// Retrieves a list of player profiles from the database.
        /// </summary>
        /// <returns>A list of Admission objects representing player profiles.</returns>
        public async Task<List<Admission>> ViewEnrolledPlayer()
        {
            var dataMapper = new CollectionDataMapper<Admission>();
            await ExecuteSP("sp_viewEnrolledPlayer", (SqlParameterCollection parameter) =>
            {

            }, dataMapper);
            var enrolledplayers = dataMapper.Data;
            return enrolledplayers;
        }


        /// <summary>
        /// Change the status of the admission player enrolled
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<string> UpdateState(AdmissionStatus admissionStatus)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;

            await ExecuteSP("[dbo].[sp_ChangeStatus]", (SqlParameterCollection parameters) =>
            {
                parameters.AddWithValue("@ID", admissionStatus.Id);
                parameters.AddWithValue("@Status", admissionStatus.status);
                parameters.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }


        /// <summary>
        /// Deletes a player from the database by their ID.
        /// </summary>
        /// <param name="Id">The ID of the player to delete.</param>
        /// <returns>The number of rows affected by the deletion (usually 1 if successful).</returns>
        public async Task<string> DeletePlayer(int Id)
        {
            string message = null;
            SqlParameter outputParameter = new SqlParameter("@message", SqlDbType.NVarChar, 1000);
            outputParameter.Direction = ParameterDirection.Output;

            await ExecuteSP("sp_removePlayer", (SqlParameterCollection parameter) =>
            {
                parameter.AddWithValue("Id", Id);
                parameter.Add(outputParameter);
            });
            message = outputParameter.Value.ToString();
            return message;
        }



        /// <summary>
        /// Retrieves a list of coaches from the database.
        /// </summary>
        /// <returns>A list of Coach objects representing coaches.</returns>
        public async Task<List<CoachList>> CoachList()
        {
            var dataMapper = new CollectionDataMapper<CoachList>();
            await ExecuteSP("sp_CoachList", (SqlParameterCollection parameter) =>
            {

            }, dataMapper);
            var coachs = dataMapper.Data;
            return coachs;
        }



        /// <summary>
        /// Retrieves a list of players by coach name from the database.
        /// </summary>
        /// <param name="name">The name of the coach whose players are to be retrieved.</param>
        /// <returns>A list of Admission objects representing players.</returns>
        public async Task<List<PlayerList>> PlayerList(string name)
        {
            var dataMapper = new CollectionDataMapper<PlayerList>();
            await ExecuteSP("sp_ListPlayers", (SqlParameterCollection parameter) =>
            {
                parameter.AddWithValue("ChooseCoach", name);
            },dataMapper);
            var playerList = dataMapper.Data;
            return playerList;
        }


        /// <summary>
        /// Retrieves a list of events associated with a player from the database.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <returns>A list of Events objects representing events.</returns>
        public async Task<List<Events>> GetEventsByPlayer(int playerId)
        {
            var dataMapper = new CollectionDataMapper<Events>();
            await ExecuteSP("sp_GetEventsPlayerId", (SqlParameterCollection parameter) =>
            {
                parameter.AddWithValue("@id", playerId);
            }, dataMapper);
            var playerEvent = dataMapper.Data;
            return playerEvent;
        }
           
        }
}
