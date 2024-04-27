using BasketballAcademy.Controllers.Base;
using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : RepositoryApiControllerBase<AdmissionRepository>
    {
        private readonly AdmissionRepository _admissionRepository;
        private readonly IConfiguration _configuration;

        public AdmissionController(IConfiguration configuration,AdmissionRepository admissionRepository):base(admissionRepository)
        {
            _admissionRepository = admissionRepository;
            _configuration = configuration;
        }

        string name;

        /// <summary>
        /// Enrolls a player in the academy.
        /// </summary>
        /// <param name="admission">Admission object containing player information.</param>
        /// <returns>1 if enrolled successfully, 2 if already enrolled, 3 if coach not available, 0 if an error occurs.</returns>
        [HttpPost("EnrollPlayer")]
        public async Task<IActionResult> EnrollPlayer(Admission admission)
        {
            var result = await _admissionRepository.AdmissionForm(admission);
            return ApiOkResponse(result);
        }



        /// <summary>
        /// Updates player information.
        /// </summary>
        /// <param name="player">Player object containing updated information.</param>
        /// <returns>1 if updated successfully, 0 if an error occurs, -1 if an exception occurs.</returns>
        [HttpPut("UpdatePlayer")]
        public async Task<IActionResult> UpdatePlayer(Player player)
        {
            var result = await _admissionRepository.UpdateProfile(player);
            return ApiOkResponse(result);
        }


        ///// <summary>
        ///// Retrieves a list of all players.
        ///// </summary>
        ///// <returns>List of Admission objects representing players.</returns>
        [HttpGet("ViewPlayer")]
        public async Task<IActionResult> ViewPlayer()
        {
            return ApiOkResponse(await _admissionRepository.ViewPlayer());
        }

        /// <summary>
        /// Retrieves a list of enrolled players.
        /// </summary>
        /// <returns>List of Admission objects representing enrolled players.</returns>
        [HttpGet("ViewEnrolledPlayer")]
        public async Task<IActionResult> ViewEnrolledPlayer()
        {
            return ApiOkResponse(await _admissionRepository.ViewEnrolledPlayer());
        }

        /// <summary>
        /// Updates the enrollment status of a player.
        /// </summary>
        /// <param name="itemId">ID of the player.</param>
        /// <param name="status">New status code.</param>
        /// <returns>1 if successful, 0 if an error occurs.</returns>
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(AdmissionStatus admission)
        {
            var response = await _admissionRepository.UpdateState(admission);
            var result =new { status = response };
            return ApiOkResponse(result);
        }


        /// <summary>
        /// Deletes a player by ID.
        /// </summary>
        /// <param name="Id">ID of the player to be deleted.</param>
        /// <returns>1 if player deleted, 0 if an error occurs.</returns>
        [HttpDelete("DeletePlayer")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _admissionRepository.DeletePlayer(Id);
            return ApiOkResponse(response);
        }

        /// <summary>
        /// Retrieves a list of all coaches.
        /// </summary>
        /// <returns>List of Coach objects representing coaches.</returns>
        [HttpGet("CoachList")]
        public async Task<IActionResult> CoachList()
        {
            return ApiOkResponse(await _admissionRepository.CoachList());
        }


        /// <summary>
        /// Retrieves a list of players associated with a coach.
        /// </summary>
        /// <param name="name">Name of the coach.</param>
        /// <returns>List of Admission objects representing players.</returns>
        [HttpGet]
        [Route("PlayerList")]
        public async Task<IActionResult> PlayerList(string name)
        {
            return ApiOkResponse(await _admissionRepository.PlayerList(name));
        }


        /// <summary>
        /// Retrieves a list of events associated with a player.
        /// </summary>
        /// <param name="id">ID of the player.</param>
        /// <returns>List of Events objects representing events.</returns>
        [HttpGet("ViewPlayerEvent")]
        public async Task<IActionResult> ViewPlayerEvent(int id)
        {
            var result = await _admissionRepository.GetEventsByPlayer(id);
            if (result.Count != 0)
            {
                return ApiOkResponse(result);
            }
            else
                return StatusCode(404, new { statusCode = 404, message = "Player not found", data = new { }, error = "Player not found" });

        }
        }
}
