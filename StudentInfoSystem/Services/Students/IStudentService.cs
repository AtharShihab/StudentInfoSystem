using StudentInfoSystem.Models.DataTable;
using StudentInfoSystem.Models.Students.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Services.Students
{
    public interface IStudentService
    {
        Task<object> CreateAsync(StudentViewModel model);

        Task<object> FindByIdAsync(int id);

        Task<object> GetListAsync();
        // bool IsExists(int id);

        Task<object> GetDataTableAsync(DataTable dataTableDto);
        Task<object> UpdateAsync(StudentViewModel model);
        Task<object> DeleteAsync(int id);
    }
}
