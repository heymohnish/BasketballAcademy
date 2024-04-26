﻿using BasketballAcademy.Controllers.Base;
using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;


namespace BasketballAcademy.Controllers
{
    //[Authorize]
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

        /// <summary>
        /// Adds a new user to the system through the signup endpoint.
        /// </summary>
        /// <param name="user">The user object containing user information.</param>
        /// <returns>Returns "1" on successful user registration, "0" if registration fails, "-1" in case of an exception.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("signup")]
        public async Task<IActionResult> AddUser(User user)
        {
            var result = await _user_repository.RegisterUser(user);
            return ApiOkResponse(result);
        }

        /// <summary>
        /// Retrieves a list of all users from the system through the ViewAllUser endpoint.
        /// </summary>
        /// <returns>Returns a list of User objects representing all users in the system.</returns>
        [HttpGet("ViewAllUser")]
        public async Task<IActionResult> ViewAll()
        {
            return ApiOkResponse(await _user_repository.ViewUser());
        }

        ///// <summary>
        ///// Deletes a user from the system based on the provided user ID.
        ///// </summary>
        ///// <param name="id">The ID of the user to be deleted.</param>
        ///// <returns>Returns a success message if user deletion is successful, otherwise returns an error message.</returns>
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _user_repository.DeleteUser(id);
            return ApiOkResponse(result);
        }
    }
}