using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
       
        public Role Role { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; }
        [ForeignKey("User_Status")]
        public int User_Status_ID { get; set; }
        public  User_Status User_Status { get; set; }

        public  ICollection<Notes> Notes { get; set; }
        public  ICollection<Contact> Contacts { get; set; }
    }
}
