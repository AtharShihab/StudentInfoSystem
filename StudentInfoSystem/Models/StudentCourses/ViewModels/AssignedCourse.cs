using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Models.StudentCourses.ViewModels
{
    public class AssignedCourse
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public bool IsSelected { get; set; }
    }
}
