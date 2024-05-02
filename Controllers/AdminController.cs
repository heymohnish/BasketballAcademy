 using BasketballAcademy.Controllers.Base;
using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace BasketballAcademy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : RepositoryApiControllerBase<AdminRepository>
    {
        private readonly AdminRepository _admin_repository;
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration, AdminRepository adminRepository) : base(adminRepository)
        {
            _admin_repository = adminRepository;
            _configuration = configuration;
        }

        [HttpGet("ViewAdmin")]
        public async Task<IActionResult> ViewAdmin()
        {
               return ApiOkResponse(await _admin_repository.ViewAdmin());
        }


        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin(Admin admin)
        {
            string message = await _admin_repository.AddAdmin(admin);
            var result = new { messgae=message};
            return ApiOkResponse(result);
        }
       
        
        [HttpDelete("DeleteAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            return ApiOkResponse(await _admin_repository.DeleteAdmin(id));
        }

        [HttpPost("Feedback")]
        [AllowAnonymous]
        public async Task<IActionResult> Contact(Contact contact)
            {
                string message = await _admin_repository.Message(contact);
                var result = new { messgae = message };
                return ApiOkResponse(result);
        }

        [HttpGet("ViewMessage")]
        public async Task<IActionResult> ViewMessage()
            {
              return ApiOkResponse(await _admin_repository.ViewMessage());
        }

        
        [HttpDelete("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage(int id)
            {
            return ApiOkResponse(await _admin_repository.DeleteMessage(id));
        }
    }
}

