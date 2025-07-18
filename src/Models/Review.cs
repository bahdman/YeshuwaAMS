using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string LecturerName  { get; set; } = string.Empty;
        public string CourseTaught { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Stars { get; set; } 
        public string CreatedBy { get; set; } = string.Empty;   
    }
}
