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
    public class AdmissionController : ControllerBase
    {
        private readonly AdmissionRepository Repository;

        public AdmissionController(IConfiguration configuration)
        {
            Repository = new AdmissionRepository(configuration);
        }

        string name;

        /// <summary>
        /// Enrolls a player in the academy.
        /// </summary>
        /// <param name="admission">Admission object containing player information.</param>
        /// <returns>1 if enrolled successfully, 2 if already enrolled, 3 if coach not available, 0 if an error occurs.</returns>
        [HttpPost]
        [Route("EnrollPlayer")]
        public int EnrollPlayer(Admission admission)
        {
            try
            {
                int result = Repository.AdmissionForm(admission);
                if (result == 1)
                {
                    name = admission.FullName;
                    string course = admission.ChooseMonths;
                    logger.LogInfo(course + " course enrolled by " + name, name);
                    return 1; // Enrolled successfully.
                }
                else if (result == -1)
                {
                    return 2; // Already enrolled in the academy.
                }
                else if (result == -2)
                {
                    return 3; // Coach not available.
                }
                return 0; // Something went wrong.
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return 0; 
            }
        }

        /// <summary>
        /// Updates player information.
        /// </summary>
        /// <param name="player">Player object containing updated information.</param>
        /// <returns>1 if updated successfully, 0 if an error occurs, -1 if an exception occurs.</returns>
        [HttpPut]
        [Route("UpdatePlayer/{id}")]
        public int UpdatePlayer(Player player)
        {
            try
            {
                name = player.FullName;
                logger.LogInfo(name + " updated profile", name);

                bool result = Repository.UpdateProfile(player);
                if (result)
                {
                    return 1; // Update successful.
                }
                else
                {
                    return 0; // Update error.
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return -1; // Exception occurred.
            }
        }

        /// <summary>
        /// Retrieves a list of all players.
        /// </summary>
        /// <returns>List of Admission objects representing players.</returns>
        [HttpGet]
        [Route("ViewPlayer")]
        public List<Admission> ViewPlayer()
        {
            try
            {
                List<Admission> admissions = Repository.ViewPlayer();
                return admissions;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Admission>();
            }
        }

        /// <summary>
        /// Retrieves a list of enrolled players.
        /// </summary>
        /// <returns>List of Admission objects representing enrolled players.</returns>
        [HttpGet]
        [Route("ViewEnrolledPlayer")]
        public List<Admission> ViewEnrolledPlayer()
        {
            try
            {
                List<Admission> admissions = Repository.ViewEnrolledPlayer();
                return admissions;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Admission>();
            }
        }

        /// <summary>
        /// Updates the enrollment status of a player.
        /// </summary>
        /// <param name="itemId">ID of the player.</param>
        /// <param name="status">New status code.</param>
        /// <returns>1 if successful, 0 if an error occurs.</returns>
        [HttpPost]
        [Route("/UpdateStatus/{itemId}/{status}")]
        public int UpdateStatus(int itemId, int status)
        {
            try
            {
                bool success = Repository.UpdateState(itemId, status);
                return 1; 
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return 0; 
            }
        }

        /// <summary>
        /// Deletes a player by ID.
        /// </summary>
        /// <param name="Id">ID of the player to be deleted.</param>
        /// <returns>1 if player deleted, 0 if an error occurs.</returns>
        [Route("DeletePlayer/{Id}")]
        [HttpDelete]
        public int Delete(int Id)
        {
            try
            {
                if (Repository.DeletePlayer(Id) == 1)
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
                return 0; 
            }
        }

        /// <summary>
        /// Retrieves a list of all coaches.
        /// </summary>
        /// <returns>List of Coach objects representing coaches.</returns>
        [HttpGet]
        [Route("CoachList")]
        public List<Coach> CoachList()
        {
            try
            {
                List<Coach> coaches = Repository.CoachList();
                return coaches;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Coach>();
            }
        }

        /// <summary>
        /// Retrieves a list of players associated with a coach.
        /// </summary>
        /// <param name="name">Name of the coach.</param>
        /// <returns>List of Admission objects representing players.</returns>
        [HttpGet]
        [Route("PlayerList/{name}")]
        public List<Admission> PlayerList(string name)
        {
            try
            {
                logger.LogInfo(name + " viewed their players", name);

               
                List<Admission> players = Repository.PlayerList(name);
                return players;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Admission>();
            }
        }

        /// <summary>
        /// Retrieves a list of events associated with a player.
        /// </summary>
        /// <param name="id">ID of the player.</param>
        /// <returns>List of Events objects representing events.</returns>
        [HttpGet]
        [Route("ViewPlayerEvent/{id}")]
        public List<Events> ViewPlayerEvent(int id)
        {
            try
            {
                List<Events> events = Repository.GetEventsByPlayer(id);
                return events;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Events>();
            }
        }
    }
}
