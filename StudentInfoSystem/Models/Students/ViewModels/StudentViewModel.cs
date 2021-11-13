using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentInfoSystem.Models.Students.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        [Required]
        [Display(Name = "Contact No.")]
        public string ContactNo { get; set; }
        [EmailAddress]
        [Required]
        [Display(Name = "Email")]
        public string MailingAddress { get; set; }
        [Required]
        [Display(Name = "Father's Name")]
        public string Father { get; set; }
        [Required]
        [Display(Name = "Father's Occupation")]
        public string FatherOcupation { get; set; }
        [Required]
        [Display(Name = "Father's Contact No.")]
        public string FatherContactNo { get; set; }
        [Required]
        [Display(Name = "Mother's Name")]
        public string Mother { get; set; }
        [Required]
        [Display(Name = "Mother's Occupation")]
        public string MotherOcupation { get; set; }
        [Display(Name = "Mother' Contact No.")]
        public string MotherContactNo { get; set; }
    }
}
