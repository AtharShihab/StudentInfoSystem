using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Services.Semesters
{
    public interface ISemesterRepo
    {
        object GetSemesterListAsync();

    }
}
