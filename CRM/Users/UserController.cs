using CRM.Config;
using CRM.Models;
using CRM.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserResponseDTO>>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return ApiResponse<IEnumerable<UserResponseDTO>>.Success(users, HttpStatusCode.OK, "Users retrieved successfully");
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponseDTO>>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return ApiResponse<UserResponseDTO>.Success(user, HttpStatusCode.OK, "User retrieved successfully");
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserResponseDTO>>> Create(UserRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errorDict = new Dictionary<string, string[]>();
                foreach (var kvp in ModelState)
                {
                    errorDict[kvp.Key] = kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray();
                }

                return BadRequest(new ApiResponse<UserResponseDTO>
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Validation Failed",
                    Errors = errorDict
                });
            }

            var created = await _userService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.UserId },
                ApiResponse<UserResponseDTO>.Success(created, HttpStatusCode.Created, "User created successfully"));
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponseDTO>>> Update(int id, UserRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errorDict = new Dictionary<string, string[]>();
                foreach (var kvp in ModelState)
                {
                    errorDict[kvp.Key] = kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray();
                }

                return BadRequest(new ApiResponse<UserResponseDTO>
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Validation Failed",
                    Errors = errorDict
                });
            }

            var updated = await _userService.UpdateAsync(id, dto);
            return ApiResponse<UserResponseDTO>.Success(updated, HttpStatusCode.OK, "User updated successfully");
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return ApiResponse<string>.Success("User deleted successfully", HttpStatusCode.OK);
        }
    }
}
