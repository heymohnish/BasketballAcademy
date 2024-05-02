using BasketballAcademy.Controllers.Base;
using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using BasketballAcademy.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    [Authorize]
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

       [HttpPost("AddCoach")]
        public async Task<IActionResult> AddCoach(Admin admin)
        {
            var result = await _coachRepository.RegisterCoach(admin);
            return ApiOkResponse(result);

        }


        [HttpGet("ViewCoach")]
        public async Task<IActionResult> ViewCoach()
        {
            return ApiOkResponse(await _coachRepository.ViewCoach());
        }

        [HttpPut("UpdateCoach")]
        public async Task<IActionResult> UpdateCoach(Coach coach)
        {
            var result = await _coachRepository.UpdateCoach(coach);
            return ApiOkResponse(result);
        }

        [HttpDelete("DeleteCoach")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _coachRepository.DeleteCoach(Id);
            return ApiOkResponse(result);
        }
         
    }
}