using BasketballAcademy.Controllers.Base;
using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;


namespace BasketballAcademy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : RepositoryApiControllerBase<UserRepository>
    {
        private readonly UserRepository _user_repository;
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration, UserRepository userRepository) : base(userRepository)
        {
            _user_repository = userRepository;
            _configuration = configuration;
        }

         [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser(User user)
        {
            var result = await _user_repository.RegisterUser(user);
            return ApiOkResponse(result);
        }

        [HttpGet("ViewAllUser")]
        public async Task<IActionResult> ViewAll()
        {
            return ApiOkResponse(await _user_repository.ViewUser());
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _user_repository.DeleteUser(id);
            return ApiOkResponse(result);
        }
    }
}