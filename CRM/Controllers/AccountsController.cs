using CRM.Config;
using CRM.Dto.Requests;
using CRM.Dto.Responses;
using CRM.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
            => _accountService = accountService;

        [HttpPost("register")]
        public async Task<ApiResponse<RegisterResponse>> Register([FromBody] RegisterRequest dto)
        {
            var result = await _accountService.RegisterAsync(dto);
            return ApiResponse<RegisterResponse>.Success(result, HttpStatusCode.OK, "Registration successful");
        }

        [HttpPost("login")]
        public async Task<ApiResponse<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var result = await _accountService.LoginAsync(request);
            return ApiResponse<LoginResponse>.Success(result, HttpStatusCode.OK, "Login successful");
        }
    }
}
