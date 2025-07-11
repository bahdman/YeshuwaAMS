using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User HOD { get; set; }
        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}
