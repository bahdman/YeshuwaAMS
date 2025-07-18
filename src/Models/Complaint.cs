using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Complaint
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
