using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly CoachRepository Repository;

        public CoachController(IConfiguration configuration)
        {
            Repository = new CoachRepository(configuration);
        }

        string name;

        /// <summary>
        /// Adds a new coach.
        /// </summary>
        /// <param name="admin">Admin object containing coach information.</param>
        /// <returns>"ok" if coach added successfully, "error" if an error occurs, "Something went wrong" if an exception occurs.</returns>
        [HttpPost]
        [Route("AddCoach")]
        public string AddCoach(Admin admin)
        {
            try
            {
                name = admin.fullName;
                logger.LogInfo(name + " new coach added", name);
                bool result = Repository.RegisterCoach(admin);
                if (result)
                {
                    return "ok"; // Coach added successfully.
                }
                else
                {
                    return "error"; // Error adding coach.
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return "Something went wrong"; 
            }
        }

        /// <summary>
        /// Retrieves a list of all coaches.
        /// </summary>
        /// <returns>List of Coach objects representing coaches.</returns>
        [HttpGet]
        [Route("ViewCoach")]
        public List<Coach> ViewCoach()
        {
            try
            {
                List<Coach> Coach = Repository.ViewCoach();
                return Coach;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Coach>();
            }
        }

        /// <summary>
        /// Updates coach information.
        /// </summary>
        /// <param name="coach">Coach object containing updated information.</param>
        /// <param name="id">ID of the coach.</param>
        /// <returns>1 if updated successfully, 0 if an error occurs, -1 if an exception occurs.</returns>
        [HttpPut]
        [Route("UpdateCoach/{id}")]
        public int UpdateCoach(Coach coach, int id)
        {
            try
            {
                name = coach.FullName;
                logger.LogInfo(name + " updated their profile", name);
                bool result = Repository.UpdateCoach(coach, id);
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
                return -1;
            }
        }

        /// <summary>
        /// Deletes a coach by ID.
        /// </summary>
        /// <param name="Id">ID of the coach to be deleted.</param>
        /// <returns>True if coach deleted, false if an error occurs.</returns>
        [HttpDelete]
        [Route("DeleteCoach/id")]
        public bool Delete(int Id)
        {
            try
            {
                int result = Repository.DeleteCoach(Id);
                if (result == 1)
                {
                    return true; // Coach deleted successfully.
                }
                else
                {
                    return false; // Error deleting coach.
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return false; 
            }
        }
    }
}
