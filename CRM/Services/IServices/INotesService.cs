using CRM.Dto.Requests;
using CRM.Dto.Responses;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM.Services.IServices
{
    public interface INotesService
    {
        Task<IEnumerable<NotesResponseDto>> GetAllAsync();
        Task<NotesResponseDto> GetByIdAsync(int id);
        Task<NotesResponseDto> CreateAsync(NotesRequestDto dto);
        Task<NotesResponseDto> UpdateAsync(int id, NotesRequestDto dto);
        Task DeleteAsync(int id);
    }
}