using CRM.Dto.Requests;
using CRM.Dto.Responses;

namespace CRM.Services.IServices
{
    public interface IAccountService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest dto);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
