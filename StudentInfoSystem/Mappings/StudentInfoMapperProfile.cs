using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentInfoSystem.Models.Courses.Entities;
using StudentInfoSystem.Models.Courses.ViewModels;
using StudentInfoSystem.Models.StudentCourses.Entities;
using StudentInfoSystem.Models.StudentCourses.ViewModels;
using StudentInfoSystem.Models.Students.Entities;
using StudentInfoSystem.Models.Students.ViewModels;

namespace StudentInfoSystem.Mappings
{
    public class StudentInfoMapperProfile : Profile
    {
        public StudentInfoMapperProfile()
        {
            CreateMap<Student, StudentViewModel>().ReverseMap();
            CreateMap<Course, CourseViewModel>().ReverseMap();
            CreateMap<StudentCourse, StudentCourseViewModel>().ReverseMap();

        }
    }
}
