using BasketballAcademy.Controllers.Base;
using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using BasketballAcademy.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : RepositoryApiControllerBase<CoachRepository>
    {


        private readonly CoachRepository _coachRepository;
        private readonly IConfiguration _configuration;

        public CoachController(IConfiguration configuration, CoachRepository coachRepository) : base(coachRepository)
        {
            _coachRepository = coachRepository;
            _configuration = configuration;
        }


        /// <summary>
        /// Adds a new coach.
        /// </summary>
        /// <param name="admin">Admin object containing coach information.</param>
        /// <returns>"ok" if coach added successfully, "error" if an error occurs, "Something went wrong" if an exception occurs.</returns>
        [HttpPost("AddCoach")]
        public async Task<IActionResult> AddCoach(Admin admin)
        {
            var result = await _coachRepository.RegisterCoach(admin);
            return ApiOkResponse(result);

        }

        ///// <summary>
        ///// Retrieves a list of all coaches.
        ///// </summary>
        ///// <returns>List of Coach objects representing coaches.</returns>
        [HttpGet("ViewCoach")]
        public async Task<IActionResult> ViewCoach()
        {
            return ApiOkResponse(await _coachRepository.ViewCoach());
        }

        /// <summary>
        /// Updates coach information.
        /// </summary>
        /// <param name="coach">Coach object containing updated information.</param>
        /// <param name="id">ID of the coach.</param>
        /// <returns>1 if updated successfully, 0 if an error occurs, -1 if an exception occurs.</returns>
        [HttpPut("UpdateCoach")]
        public async Task<IActionResult> UpdateCoach(Coach coach)
        {
            var result = await _coachRepository.UpdateCoach(coach);
            return ApiOkResponse(result);
        }

        /// <summary>
        /// Deletes a coach by ID.
        /// </summary>
        /// <param name="Id">ID of the coach to be deleted.</param>
        /// <returns>True if coach deleted, false if an error occurs.</returns>
        [HttpDelete("DeleteCoach")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _coachRepository.DeleteCoach(Id);
            return ApiOkResponse(result);
        }
         
    }
}