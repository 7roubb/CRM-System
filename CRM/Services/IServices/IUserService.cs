using CRM.Dto.Requests;
using CRM.Dto.Responses;

namespace CRM.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAllAsync();
        Task<UserResponseDTO> GetByIdAsync(int id);
        Task<UserResponseDTO> CreateAsync(UserRequestDTO dto);
        Task<UserResponseDTO> UpdateAsync(int id, UserRequestDTO dto);
        Task DeleteAsync(int id);
        Task<bool> ChangeRole(int UserId, string RoleName);
    }
}
