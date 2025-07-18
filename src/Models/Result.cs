﻿using src.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Result
    {
        public int Id { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int CaScore { get; set; }
        public int ExamScore { get; set; }
        public string Grade { get; set; }
        public Status Status { get; set; }
    }
}
