using CRM.Config;
using CRM.Dto.Requests;
using CRM.Dto.Responses;
using CRM.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, Sales")]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<NotesResponseDto>>>> GetAll()
        {
            var notes = await _notesService.GetAllAsync();
            return ApiResponse<IEnumerable<NotesResponseDto>>.Success(notes, HttpStatusCode.OK, "Notes retrieved successfully");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<NotesResponseDto>>> GetById(int id)
        {
            var note = await _notesService.GetByIdAsync(id);
            return ApiResponse<NotesResponseDto>.Success(note, HttpStatusCode.OK, "Note retrieved successfully");
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<NotesResponseDto>>> Create([FromBody] NotesRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errorDict = new Dictionary<string, string[]>();
                foreach (var kvp in ModelState)
                {
                    errorDict[kvp.Key] = kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray();
                }

                return BadRequest(new ApiResponse<NotesResponseDto>
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Validation Failed",
                    Errors = errorDict
                });
            }

            var created = await _notesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.NotesId },
                ApiResponse<NotesResponseDto>.Success(created, HttpStatusCode.Created, "Note created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<NotesResponseDto>>> Update(int id, [FromBody] NotesRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errorDict = new Dictionary<string, string[]>();
                foreach (var kvp in ModelState)
                {
                    errorDict[kvp.Key] = kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray();
                }

                return BadRequest(new ApiResponse<NotesResponseDto>
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Validation Failed",
                    Errors = errorDict
                });
            }

            var updated = await _notesService.UpdateAsync(id, dto);
            return ApiResponse<NotesResponseDto>.Success(updated, HttpStatusCode.OK, "Note updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            await _notesService.DeleteAsync(id);
            return ApiResponse<string>.Success("Note deleted successfully", HttpStatusCode.OK);
        }
    }
}
