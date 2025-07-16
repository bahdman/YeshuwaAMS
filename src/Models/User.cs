using Microsoft.AspNetCore.Identity;
using src.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class User : IdentityUser
    {
        // [Key]
        // public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        // public string Email { get; set; } = string.Empty;
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateOnly DOB { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string StateOfOrigin { get; set; } = string.Empty;
        public string LGA { get; set; } = string.Empty;
        public string Program { get; set; }
        public Level Level { get; set; }
        public string EntryMode { get; set; }
    }
}
