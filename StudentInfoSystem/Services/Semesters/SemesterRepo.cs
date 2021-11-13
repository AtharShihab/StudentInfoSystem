using Microsoft.AspNetCore.Http;
using StudentInfoSystem.Models.Semesters.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Services.Semesters
{
    public class SemesterRepo : ISemesterRepo
    {
        public static List<SemesterViewModel> Semesters = new List<SemesterViewModel>()
        {
            new SemesterViewModel{ Name = "Spring", Code = "SP"},
            new SemesterViewModel{ Name = "Summer", Code = "SU"},
            new SemesterViewModel{ Name = "Fall", Code = "FA"},
        };

        public object GetSemesterListAsync()
        {
            return new
            {
                StatusCode = StatusCodes.Status200OK,
                Success = true,
                Data = Semesters
            };
        }
    }
}
