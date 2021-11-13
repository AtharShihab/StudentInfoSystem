using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Models.Courses.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Course Code")]
        public string CourseCode { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Required]
        [Display(Name = "Credit Hours")]
        public double CreditHour { get; set; }
        [Required]
        [Display(Name = "Contact Hours")]
        public double ContactHour { get; set; }
    }
}
