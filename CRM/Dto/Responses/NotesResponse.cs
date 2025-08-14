using System;

namespace CRM.Dto.Responses
{
    public class NotesResponseDto
    {
        public int NotesId { get; set; }
        public string Date { get; set; }
        public string Note { get; set; }
        public bool IsNew_Todo { get; set; }
        public int Todo_Type_ID { get; set; }
        public int Todo_Desc_ID { get; set; }
        public int Task_Status_ID { get; set; }
        public int Sales_Rep_ID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}