using BasketballAcademy.Controllers.Base;
using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace BasketballAcademy.Controllers
{
    //[Authorize]
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

        /// <summary>
        /// Retrieves a list of all administrators.
        /// </summary>
        /// <returns>List of Admin objects representing administrators.</returns>
        [HttpGet("ViewAdmin")]
        public async Task<IActionResult> ViewAdmin()
        {
               return ApiOkResponse(await _admin_repository.ViewAdmin());
              
        }

        /// <summary>
        /// Adds a new administrator.
        /// </summary>
        /// <param name="admin">Admin object containing information about the new administrator.</param>
        /// <returns>1 if the administrator is added successfully, 0 if the administrator already exists, -1 if an error occurs.</returns>
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin(Admin admin)
        {
            string message = await _admin_repository.AddAdmin(admin);
            var result = new { messgae=message};
            return ApiOkResponse(result);
                }

        /// <summary>
        /// Deletes an administrator by ID.
        /// </summary>
        /// <param name="id">ID of the administrator to be deleted.</param>
        /// <returns>Success message if the administrator is deleted, or an error message if an exception occurs.</returns>
        [HttpDelete("DeleteAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            return ApiOkResponse(await _admin_repository.DeleteAdmin(id));
        }

        ///// <summary>
        ///// Receives feedback from a user.
        ///// </summary>
        ///// <param name="contact">Contact object containing feedback information.</param>
        ///// <returns>Success message if the feedback is sent successfully, or an error message if an exception occurs.</returns>
        [HttpPost("Feedback")]
        [AllowAnonymous]
        public async Task<IActionResult> Contact(Contact contact)
            {
                string message = await _admin_repository.Message(contact);
                var result = new { messgae = message };
                return ApiOkResponse(result);
        }

        /// <summary>
        /// Retrieves a list of feedback messages.
        /// </summary>
        /// <returns>List of Contact objects representing feedback messages.</returns>
        [HttpGet]
        [Route("ViewMessage")]
        public async Task<IActionResult> ViewMessage()
            {
              return ApiOkResponse(await _admin_repository.ViewMessage());
        }

        /// <summary>
        /// Deletes a feedback message by ID.
        /// </summary>
        /// <param name="id">ID of the feedback message to be deleted.</param>
        /// <returns>Success message if the feedback message is deleted, or an error message if an exception occurs.</returns>

        [HttpDelete("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage(int id)
            {
            return ApiOkResponse(await _admin_repository.DeleteMessage(id));
        }
    }
}

