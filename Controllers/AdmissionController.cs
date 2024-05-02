using BasketballAcademy.Controllers.Base;
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
    public class AdmissionController : RepositoryApiControllerBase<AdmissionRepository>
    {
        private readonly AdmissionRepository _admissionRepository;
        private readonly IConfiguration _configuration;

        public AdmissionController(IConfiguration configuration,AdmissionRepository admissionRepository):base(admissionRepository)
        {
            _admissionRepository = admissionRepository;
            _configuration = configuration;
        }


        [HttpPost("EnrollPlayer")]
        public async Task<IActionResult> EnrollPlayer(Admission admission)
        {
            var result = await _admissionRepository.AdmissionForm(admission);
            return ApiOkResponse(result);
        }



        [HttpPut("UpdatePlayer")]
        public async Task<IActionResult> UpdatePlayer(Player player)
        {
            var result = await _admissionRepository.UpdateProfile(player);
            return ApiOkResponse(result);
        }

         [HttpGet("ViewPlayer")]
        public async Task<IActionResult> ViewPlayer()
        {
            return ApiOkResponse(await _admissionRepository.ViewPlayer());
        }

        [HttpGet("ViewEnrolledPlayer")]
        public async Task<IActionResult> ViewEnrolledPlayer()
        {
            return ApiOkResponse(await _admissionRepository.ViewEnrolledPlayer());
        }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(AdmissionStatus admission)
        {
            var response = await _admissionRepository.UpdateState(admission);
            var result =new { status = response };
            return ApiOkResponse(result);
        }

        [HttpDelete("DeletePlayer")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _admissionRepository.DeletePlayer(Id);
            return ApiOkResponse(response);
        }


        [HttpGet("CoachList")]
        public async Task<IActionResult> CoachList()
        {
            return ApiOkResponse(await _admissionRepository.CoachList());
        }

        [HttpGet("PlayerList")]
        public async Task<IActionResult> PlayerList(string name)
        {
            return ApiOkResponse(await _admissionRepository.PlayerList(name));
        }

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
