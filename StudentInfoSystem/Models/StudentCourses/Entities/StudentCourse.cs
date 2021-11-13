using StudentInfoSystem.Models.Courses.Entities;
using StudentInfoSystem.Models.Students.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Models.StudentCourses.Entities
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Required]
        public string SemesterCode { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
