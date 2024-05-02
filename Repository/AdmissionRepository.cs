using BasketballAcademy.Model;
using BasketballAcademy.Repository.Base;
using System.Data.SqlClient;
using System.Data;
using BasketballAcademy.Repository.Interface;
using System.Numerics;
using System.Security.Cryptography.Xml;

namespace BasketballAcademy.Repository
{
    public class AdmissionRepository : RepositoryBase
    {

        public AdmissionRepository(string connectionStrings) : base(connectionStrings)
        {

        }


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


        public async Task<List<Admission>> ViewPlayer()
        {
            var dataMapper = new CollectionDataMapper<Admission>();
            await ExecuteSP("sp_viewPlayers", (SqlParameterCollection parameter) =>
            {

            }, dataMapper);
            var players = dataMapper.Data;
            return players;
        }



        public async Task<List<Admission>> ViewEnrolledPlayer()
        {
            var dataMapper = new CollectionDataMapper<Admission>();
            await ExecuteSP("sp_viewEnrolledPlayer", (SqlParameterCollection parameter) =>
            {

            }, dataMapper);
            var enrolledplayers = dataMapper.Data;
            return enrolledplayers;
        }


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



        public async Task<List<CoachList>> CoachList()
        {
            var dataMapper = new CollectionDataMapper<CoachList>();
            await ExecuteSP("sp_CoachList", (SqlParameterCollection parameter) =>
            {

            }, dataMapper);
            var coachs = dataMapper.Data;
            return coachs;
        }



        public async Task<List<PlayerList>> PlayerList(string name)
        {
            var dataMapper = new CollectionDataMapper<PlayerList>();
            await ExecuteSP("sp_ListPlayers", (SqlParameterCollection parameter) =>
            {
                parameter.AddWithValue("ChooseCoach", name);
            }, dataMapper);
            var playerList = dataMapper.Data;
            return playerList;
        }


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
