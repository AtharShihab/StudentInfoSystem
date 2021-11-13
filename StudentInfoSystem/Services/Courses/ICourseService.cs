using StudentInfoSystem.Models.Courses.ViewModels;
using StudentInfoSystem.Models.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Services.Courses
{
    public interface ICourseService
    {
        Task<object> CreateAsync(CourseViewModel model);

        Task<object> FindByIdAsync(int id);

        Task<object> GetListAsync();
        // bool IsExists(int id);

        Task<object> GetDataTableAsync(DataTable dataTableDto);
        Task<object> UpdateAsync(CourseViewModel model);
        Task<object> DeleteAsync(int id);
    }
}
