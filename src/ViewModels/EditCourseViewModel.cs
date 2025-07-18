using src.Enums;
using src.Models;

namespace src.ViewModels
{
    public class EditCourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public int Unit { get; set; }
        public CourseCategory CourseCategory { get; set; }
        public int LecturerId { get; set; }
        public User Lecturer { get; set; }
    }
}
