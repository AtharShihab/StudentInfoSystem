using StudentInfoSystem.Models.DataTable;
using StudentInfoSystem.Models.StudentCourses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Services.StudentCourses
{
    public interface IStudentCourseService
    {
        Task<object> CreateAsync(StudentCourseViewModel model);

        Task<object> GetAssignedCourse(AssignedCourseQueryViewModel model);
        // bool IsExists(int id);

        Task<object> DeleteAsync(int id);
    }
}
