using CRM.Data;
using CRM.Dto.Requests;
using CRM.Dto.Responses;
using CRM.Exceptions;
using CRM.Mappers;
using CRM.Model;
using CRM.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CRM.Services
{
    public class NotesService : INotesService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NotesService> _logger;

        public NotesService(ApplicationDbContext context, ILogger<NotesService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<NotesResponseDto>> GetAllAsync()
        {
            _logger.LogInformation("Attempting to retrieve all notes.");
            var notes = await _context.Notes.AsNoTracking().ToListAsync();
            _logger.LogInformation($"Found {notes.Count} notes.");
            return notes.Select(n => n.ToNotesResponseDto());
        }

        public async Task<NotesResponseDto> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Attempting to retrieve note with ID: {id}.");
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                _logger.LogWarning($"Note with ID {id} not found.");
                throw new NoteNotFoundException($"Note with ID {id} not found.");
            }

            _logger.LogInformation($"Successfully retrieved note with ID: {id}.");
            return note.ToNotesResponseDto();
        }

        public async Task<NotesResponseDto> CreateAsync(NotesRequestDto dto)
        {
            _logger.LogInformation("Attempting to create a new note.");

            var note = dto.ToNotes();

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Successfully created note with ID: {note.Id}.");

            return note.ToNotesResponseDto();
        }

        public async Task<NotesResponseDto> UpdateAsync(int id, NotesRequestDto dto)
        {
            _logger.LogInformation($"Attempting to update note with ID: {id}.");
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                _logger.LogWarning($"Note with ID {id} not found for update.");
                throw new NoteNotFoundException($"Note with ID {id} not found.");
            }

            // تحديث الخصائص من الـ DTO
            note.UpdateFromDto(dto);

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Successfully updated note with ID: {id}.");

            return note.ToNotesResponseDto();
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Attempting to delete note with ID: {id}.");
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                _logger.LogWarning($"Note with ID {id} not found for deletion.");
                throw new NoteNotFoundException($"Note with ID {id} not found.");
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Successfully deleted note with ID: {id}.");
        }
    }
}
