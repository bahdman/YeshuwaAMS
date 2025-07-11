using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Departments { get; set; }
    }
}
