using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeManager.Domain.DataObjects.Documents.Responses;
using OfficeManager.Domain.DataObjects.Paginations.Requests;
using OfficeManager.Domain.DataObjects.Paginations.Responses;
using OfficeManager.Domain.DataObjects.Users.Requests;
using OfficeManager.Domain.DataObjects.Users.Responses;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Services;
using OfficeManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeManager.Api.Controllers.v1
{
    [ApiController]
    [Route("/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Process to get all users by office id
        /// </summary>
        /// <returns> Users with the given office id</returns>
        [ProducesResponseType(typeof(List<DocumentResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/users/office/{officeId:guid}")]
        public async Task<IActionResult> GetAllByCaseId([FromRoute] Guid officeId)
        {
            var result = await _userService.GetAllByOfficeId(officeId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get the users paginated
        /// </summary>
        /// <returns> List of users paged </returns>
        [ProducesResponseType(typeof(PaginationResponse<UserResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/users:paginated")]
        public async Task<IActionResult> GetPaginated([FromBody] UserPaginationRequest request)
        {
            var result = await _userService.GetPaginated(request);
            return Ok(result);
        }

        /// <summary>
        /// Process to check if the user email is already in use
        /// </summary>
        /// <returns> A boolean, if true means the email is already in use by another user</returns>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpPost("/v1/users:check-email")]
        public async Task<ActionResult<User>> CheckEmail([FromBody] string email)
        {
            try
            {
                var emailIsInUse = await _userService.CheckIfEmailIsAlreadyInUse(email);
                return Ok(emailIsInUse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Process to do the user login
        /// </summary>
        /// <returns> User logged</returns>
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [HttpPost("/v1/users:login")]
        public async Task<ActionResult<User>> Login([FromBody] UserRequest request)
        {
            try
            {
                var user = await _userService.LoginAsync(request);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Process to reset the user password
        /// </summary>
        /// <returns> None</returns>
        [HttpPost("/v1/users:reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] UserRequest request)
        {
            try
            {
                await _userService.ResetPasswordAsync(request);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Process to get the user by id
        /// </summary>
        /// <returns> User with the given id</returns>
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [HttpGet("/v1/users/{userId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid userId)
        {
            var result = await _userService.GetById(userId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get all the users
        /// </summary>
        /// <returns> All the client meetings</returns>
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/users")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _userService.GetAll();
            return Ok(entities);
        }

        /// <summary>
        /// Process to create a new user
        /// </summary>
        /// <returns> None </returns>
        [HttpPost("/v1/users")]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            try
            {
                await _userService.Create(request);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Process to update an user
        /// </summary>
        /// <returns> None </returns>
        [HttpPut("/v1/users")]
        public async Task<IActionResult> Update([FromBody] UserRequest request)
        {
            try
            {
                await _userService.Update(request);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Process to delete an user
        /// </summary>
        /// <returns> None </returns>
        [HttpDelete("/v1/users")]
        public async Task<IActionResult> Delete([FromRoute] Guid userId)
        {
            try
            {
                await _userService.Remove(userId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
