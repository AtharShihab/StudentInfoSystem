using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Data;
using StudentInfoSystem.Models.DataTable;
using StudentInfoSystem.Models.StudentCourses.Entities;
using StudentInfoSystem.Models.StudentCourses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Services.StudentCourses
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public StudentCourseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> CreateAsync(StudentCourseViewModel model)
        {
            try
            {
                var studentCoursesToBeRemoved = _context.StudentCourse.Where(s => s.StudentId == model.StudentId && s.SemesterCode == model.SemesterCode);
                _context.StudentCourse.RemoveRange(studentCoursesToBeRemoved);
                await _context.SaveChangesAsync();


                

                for (int i = 0; i < model.Courses.Count; i++)
                {
                    var studentCourse = _mapper.Map<StudentCourse>(model);
                    studentCourse.EntryDate = DateTime.Now;
                    if (model.Courses[i].IsSelected)
                    {
                        studentCourse.CourseId = model.Courses[i].Id;
                        _context.StudentCourse.Add(studentCourse);
                        
                    }
                }

                await _context.SaveChangesAsync();
                return new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Assigned Successfully!"
                };
            }
            catch (Exception ex)
            {

                return new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    Message = ex.InnerException?.Message ?? ex.Message
                };
            }
        }

        public Task<object> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetAssignedCourse(AssignedCourseQueryViewModel model)
        {
            var courses = await _context.CourseList.ToListAsync();
            var assignedCourses = new List<AssignedCourse>();
            for (int i = 0; i < courses.Count; i++)
            {
                var assignedCourse = new AssignedCourse()
                {
                    Id = courses[i].Id,
                    CourseCode = courses[i].CourseCode,
                    CourseName = courses[i].CourseName
                };
                if(model.StudentId != 0) {
                    if(model.SemesterCode != "")
                    {
                        if (await _context.StudentCourse.AnyAsync(c => c.CourseId == courses[i].Id && c.StudentId == model.StudentId && c.SemesterCode == model.SemesterCode))
                        {
                            assignedCourse.IsSelected = true;
                        }
                    }
                    else
                    {
                        if (await _context.StudentCourse.AnyAsync(c => c.CourseId == courses[i].Id && c.StudentId == model.StudentId))
                        {
                            assignedCourse.IsSelected = true;
                        }
                    }
                    
                }
                else
                {
                    assignedCourse.IsSelected = false;
                }

                assignedCourses.Add(assignedCourse);
            }
            return new
            {
                StatusCode = StatusCodes.Status200OK,
                Success = true,
                Data = assignedCourses
            };
        }

        
    }
}
