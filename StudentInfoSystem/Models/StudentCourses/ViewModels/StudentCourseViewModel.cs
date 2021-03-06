using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Models.StudentCourses.ViewModels
{
    public class StudentCourseViewModel
    {
        [Required]
        [Display(Name = "Student")]
        public int StudentId { get; set; }
        [Required]
        [Display(Name = "Semester")]
        public string SemesterCode { get; set; }
        public List<AssignedCourse> Courses { get; set; }
    }
}
