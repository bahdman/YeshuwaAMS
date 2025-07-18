using src.Enums;

namespace src.ViewModels
{
    public class CreateCourseViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public int Unit { get; set; }
        public CourseCategory CourseCategory { get; set; }
        //public User Lecturer { get; set; }
    }
}
