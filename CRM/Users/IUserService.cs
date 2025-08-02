
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Models;

namespace CRM.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAllAsync();
        Task<UserResponseDTO> GetByIdAsync(int id);
        Task<UserResponseDTO> CreateAsync(UserRequestDTO dto);
        Task<UserResponseDTO> UpdateAsync(int id, UserRequestDTO dto);
        Task DeleteAsync(int id);
    }
}
