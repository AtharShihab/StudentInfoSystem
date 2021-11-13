using StudentInfoSystem.Data;
using StudentInfoSystem.Models.DataTable;
using StudentInfoSystem.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Models.Students.ViewModels;
using AutoMapper;
using StudentInfoSystem.Models.Students.Entities;

namespace StudentInfoSystem.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public StudentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /**
         * @name CreateAsync
         * @role Create new Student
         * @param StudentInfoSystem.Models.Students.ViewModels.StudentViewModel model
         * @return status code with message
         */
        public async Task<object> CreateAsync(StudentViewModel model)
        {
            try
            {
                if (await IsEmailExistsAsync(model.MailingAddress, model.Id))
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status409Conflict,
                        Success = false,
                        Message = "Email already exists!"
                    };
                }


                if (await IsContactNoExistsAsync(model.ContactNo, model.Id))
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status409Conflict,
                        Success = false,
                        Message = "Contact No. already exists!"
                    };
                }


                var student = _mapper.Map<Student>(model);

                student.EntryDate = DateTime.Now;
                student.UpdateDate = DateTime.Now;
                _context.Entry(student).State = EntityState.Added;
                await _context.SaveChangesAsync();


                return new
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Student Created Successfully!"

                };
            }
            catch (Exception ex)
            {

                return new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message

                };
            }
        }




        /**
         * @name DeleteAsync
         * @role Delete a Student
         * @param int id
         * @return status code with message
         */
        public async Task<object> DeleteAsync(int id)
        {
            try
            {
                var student = await _context.StudentInformation.FindAsync(id);
                if (student == null)
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Student does not exists!"
                    };
                }


                _context.StudentInformation.Remove(student);
                await _context.SaveChangesAsync();

                return new
                {
                    StatusCode = StatusCodes.Status204NoContent,
                    Success = true,
                    Message = "Student deleted successfully!"

                };
            }
            catch (Exception ex)
            {
                return new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message

                };
            }
        }









        /**
         * @name FindByIdAsync
         * @role find an student by his id
         * @param int id
         * @return status code with StudentInfoSystem.Models.Students.ViewModels.StudentViewModel as data
         */
        public async Task<object> FindByIdAsync(int id)
        {
            try
            {
                var student = await _context.StudentInformation.SingleOrDefaultAsync(s => s.Id == id);
                if (student == null)
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Student does not exists!"
                    };
                }


                var studentModel = _mapper.Map<StudentViewModel>(student);


                return new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    Data = studentModel
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message

                };
            }
        }




        /**
         * @name GetDataTableAsync
         * @role Get a list of students
         * @param StudentInfoSystem.Models.DataTable.DataTable dataTableDto
         * @return status code with List<StudentInfoSystem.Models.Students.ViewModels.StudentViewModel> studentModels
         */
        public async Task<object> GetDataTableAsync(DataTable dataTableDto)
        {
            try
            {
                if (dataTableDto == null)
                {
                    throw new ArgumentNullException(nameof(dataTableDto));
                }

                int draw = dataTableDto.Draw;
                int start = dataTableDto.Start;
                int length = dataTableDto.Length;

                // Sorting Column and order
                string sortColumnName = dataTableDto.Columns[dataTableDto.Order[0].Column].Name;
                string sortColumnDir = dataTableDto.Order[0].Dir;

                // Search value
                string searchValue = dataTableDto.Search.Value;

                IQueryable<Student> studentAsQueryable = _context.StudentInformation;


                int recordsTotal = studentAsQueryable.Count();


                if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    studentAsQueryable = studentAsQueryable.Where(m => m.Name.Contains(searchValue)
                                                                       || m.DOB.Year.Equals(searchValue)
                                                                       || m.DOB.Month.Equals(searchValue)
                                                                       || m.DOB.Day.Equals(searchValue)
                                                                       || m.ContactNo.Contains(searchValue)
                                                                       || m.MailingAddress.Contains(searchValue)
                                                                       || m.Father.Contains(searchValue)
                                                                       || m.Mother.Contains(searchValue));
                }

                int recordsFiltered = studentAsQueryable.Count();

                var students = await studentAsQueryable.Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.DOB,
                    m.ContactNo,
                    m.MailingAddress,
                    m.Father,
                    m.Mother
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                // }).Skip(start).Take(length).ToListAsync();

                return new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = students
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message

                };
            }
        }




        /**
         * @name GetListAsync
         * @role Get a simple list of students
         * @param 
         * @return status code with StudentInfoSystem.Models.Students.Entities.Student as data
         */
        public async Task<object> GetListAsync()
        {

            //return _userManager.Users.ToListAsync();
            try
            {
                var data = await _context.StudentInformation
                    .AsNoTracking()
                    .ToListAsync();

                return new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message

                };
            }

        }




        /**
         * @name UpdateAsync
         * @role Update students information
         * @param StudentInfoSystem.Models.Students.ViewModels.Student model
         * @return status code with message
         */
        public async Task<object> UpdateAsync(StudentViewModel model)
        {
            try
            {
                if (await IsEmailExistsAsync(model.MailingAddress, model.Id))
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Email already exists!"
                    };
                }


                if (await IsContactNoExistsAsync(model.ContactNo, model.Id))
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Contact No. already exists!"
                    };
                }

                var student = _mapper.Map<Student>(model);
                student.UpdateDate = DateTime.Now;
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                return new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Student updated successfully!"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message

                };
            }

        }














        /**
         * @name IsEmailExistsAsync
         * @role Check if student with same email already exists in database
         * @param string email, string Id
         * @return bool
         */
        private async Task<bool> IsEmailExistsAsync(string email, int Id)
        {
            return await _context.StudentInformation.AnyAsync(e => e.MailingAddress == email && e.Id != Id);
        }




        /**
         * @name IsContactNoExistsAsync
         * @role Check if student with same contact already exists in database
         * @param string contactNo, string Id
         * @return bool
         */
        private async Task<bool> IsContactNoExistsAsync(string contactNo, int id)
        {
            return await _context.StudentInformation.AnyAsync(s => s.ContactNo == contactNo && s.Id != id);
        }

    }
}
