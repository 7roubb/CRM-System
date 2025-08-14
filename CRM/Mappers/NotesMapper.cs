using CRM.Dto.Requests;
using CRM.Dto.Responses;
using CRM.Model;
using System;

namespace CRM.Mappers
{
    public static class NotesMapper
    {
        public static Notes ToNotes(this NotesRequestDto dto)
        {
            return new Notes
            {
                Date = dto.Date,
                Note = dto.Note,
                IsNew_Todo = dto.IsNew_Todo,
                Todo_Type_ID = dto.Todo_Type_ID,
                Todo_Desc_ID = dto.Todo_Desc_ID,
                Task_Status_ID = dto.Task_Status_ID,
                Sales_Rep_ID = dto.Sales_Rep_ID
            };
        }

        public static NotesResponseDto ToNotesResponseDto(this Notes notes)
        {
            return new NotesResponseDto
            {
                NotesId = notes.Id,
                Date = notes.Date,
                Note = notes.Note,
                IsNew_Todo = notes.IsNew_Todo,
                Todo_Type_ID = notes.Todo_Type_ID,
                Todo_Desc_ID = notes.Todo_Desc_ID,
                Task_Status_ID = notes.Task_Status_ID,
                Sales_Rep_ID = notes.Sales_Rep_ID,
                CreatedAt = DateTime.UtcNow // Auto
            };
        }

        public static void UpdateFromDto(this Notes notes, NotesRequestDto dto)
        {
            notes.Date = dto.Date;
            notes.Note = dto.Note;
            notes.IsNew_Todo = dto.IsNew_Todo;
            notes.Todo_Type_ID = dto.Todo_Type_ID;
            notes.Todo_Desc_ID = dto.Todo_Desc_ID;
            notes.Task_Status_ID = dto.Task_Status_ID;
            notes.Sales_Rep_ID = dto.Sales_Rep_ID;
        }
    }
}