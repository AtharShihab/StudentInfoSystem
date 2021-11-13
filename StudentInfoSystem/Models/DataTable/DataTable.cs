using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Models.DataTable
{
    public class DataTable
    {
        public int Draw { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public ColumnRequestItem[] Columns { get; set; }

        public OrderRequestItem[] Order { get; set; }

        public SearchRequestItem Search { get; set; }
    }
}
