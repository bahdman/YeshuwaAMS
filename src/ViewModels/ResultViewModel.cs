using src.Enums;
using src.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.ViewModels
{
    public class ResultViewModel
    {
        //public int Id { get; set; }
        //public int CourseId { get; set; }
        //public Course Course { get; set; }
        public int CaScore { get; set; }
        public int ExamScore { get; set; }
        public string Grade { get; set; }
        public Status Status { get; set; }
    }
}
