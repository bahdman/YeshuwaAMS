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
        public string Level { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        [ForeignKey("Faculty")]
        public int? FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public CourseCategory CourseCategory { get; set; }
        [ForeignKey("Lecturer")]
        public string LecturerId { get; set; } = string.Empty;
        public User Lecturer { get; set; }
    }
}
