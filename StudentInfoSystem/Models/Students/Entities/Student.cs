using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Models.Students.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string ContactNo { get; set; }
        [EmailAddress]
        [Required]
        public string MailingAddress { get; set; }
        [Required]
        public string Father { get; set; }
        [Required]
        public string FatherOcupation { get; set; }
        [Required]
        public string FatherContactNo { get; set; }
        [Required]
        public string Mother { get; set; }
        [Required]
        public string MotherOcupation { get; set; }
        public string MotherContactNo { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
