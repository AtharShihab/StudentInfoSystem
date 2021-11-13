using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Models.Courses.Entities
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string CourseCode { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public double CreditHour { get; set; }
        [Required]
        public double ContactHour { get; set; }

    }
}
