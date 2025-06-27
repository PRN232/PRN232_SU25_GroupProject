using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Users;
using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <returns>The created user.</returns>
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var created = await _userService.CreateUserAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a user by id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Get users by role.
        /// </summary>
        [HttpGet("roles/{role}")]
        public async Task<ActionResult<List<UserDto>>> GetByRole(string role)
        {
            if (!Enum.TryParse<UserRole>(role, true, out var userRole))
                return BadRequest("Invalid role.");

            var users = await _userService.GetUsersByRoleAsync(userRole);
            return Ok(users);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
        {
            if (id != request.Id) return BadRequest("Id mismatch.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _userService.UpdateUserAsync(request);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deactivate (soft delete) a user.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _userService.DeactivateUserAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
