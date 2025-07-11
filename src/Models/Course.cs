using src.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public int Unit { get; set; }
        public CourseCategory CourseCategory { get; set; }
        [ForeignKey("Lecturer")]
        public int LecturerId { get; set; }
        public User Lecturer { get; set; }
    }
}
