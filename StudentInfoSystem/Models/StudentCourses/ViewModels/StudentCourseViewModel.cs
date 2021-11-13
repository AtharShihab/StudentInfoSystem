using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Models.StudentCourses.ViewModels
{
    public class StudentCourseViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        [Required]
        public string SemesterCode { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
