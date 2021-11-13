using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Models.StudentCourses.ViewModels
{
    public class AssignedCourseQueryViewModel
    {
        public int StudentId { get; set; }
        public string SemesterCode { get; set; }
    }
}
