namespace src.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string LecturerName { get; set; } = string.Empty;
        public string CourseTaught { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Stars { get; set; }
    }
}
