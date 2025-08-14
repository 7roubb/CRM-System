using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Dto.Requests
{
    public class ContactRequest
    {
        [Required]
        [MaxLength(10)]
        public string Contact_Title { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z\-']+$", ErrorMessage = "First name can only contain letters, hyphens, and apostrophes.")]
        public string Contact_First { get; set; }

        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z\-']*$", ErrorMessage = "Middle name can only contain letters, hyphens, and apostrophes.")]
        public string Contact_Middle { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z\-']+$", ErrorMessage = "Last name can only contain letters, hyphens, and apostrophes.")]
        public string Contact_Last { get; set; }

        [Required]
        [MaxLength(50)]
        public string Contact_Type { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date_of_Initial_Contact { get; set; }

        [MaxLength(100)]
        public string Company { get; set; }

        [MaxLength(100)]
        public string Address_Street_1 { get; set; }

        [MaxLength(100)]
        public string Address_Street_2 { get; set; }

        [MaxLength(50)]
        public string Address_City { get; set; }

        [MaxLength(50)]
        public string Address_State { get; set; }

        [MaxLength(20)]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Zip must be in 12345 or 12345-6789 format.")]
        public string Address_Zip { get; set; }

        [MaxLength(50)]
        public string Address_Country { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Url(ErrorMessage = "Invalid website URL format.")]
        public string Website { get; set; }

        [Url(ErrorMessage = "Invalid LinkedIn profile URL format.")]
        public string Linkedin_Profile { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Sales_Rep_ID must be a positive integer.")]
        public int Sales_Rep_ID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Contact_Status_ID must be a positive integer.")]
        public int Contact_Status_ID { get; set; }

        [MaxLength(50)]
        public string Project_Type { get; set; }

        [MaxLength(500)]
        public string Proposal_Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Proposal_Date { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Deliverables must be a non-negative number.")]
        public float Deliverables { get; set; }
    }
}
