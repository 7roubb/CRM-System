using System.ComponentModel.DataAnnotations;

namespace CRM.Dto.Requests
{
    public class NotesRequestDto
    {
        [Required(ErrorMessage = "Date is required")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Note content is required")]
        [StringLength(500, ErrorMessage = "Note cannot exceed 500 characters")]
        public string Note { get; set; }

        public bool IsNew_Todo { get; set; }

        [Required(ErrorMessage = "Todo Type ID is required")]
        public int Todo_Type_ID { get; set; }

        [Required(ErrorMessage = "Todo Description ID is required")]
        public int Todo_Desc_ID { get; set; }

        [Required(ErrorMessage = "Task Status ID is required")]
        public int Task_Status_ID { get; set; }

        [Required(ErrorMessage = "Sales Rep ID is required")]
        public int Sales_Rep_ID { get; set; }
    }
}