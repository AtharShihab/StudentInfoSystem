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
using AutoMapper;
using StudentInfoSystem.Models.Courses.ViewModels;
using StudentInfoSystem.Models.Courses.Entities;

namespace StudentInfoSystem.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public CourseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /**
         * @name CreateAsync
         * @role Create new Course
         * @param StudentInfoSystem.Models.Courses.ViewModels.CourseViewModel model
         * @return status code with message
         */
        public async Task<object> CreateAsync(CourseViewModel model)
        {
            try
            {
                if (await IsCourseCodeExistsAsync(model.CourseCode, model.Id))
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status409Conflict,
                        Success = false,
                        Message = "Course Code already exists!"
                    };
                }


                if (await IsCourseNameExistsAsync(model.CourseName, model.Id))
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status409Conflict,
                        Success = false,
                        Message = "Course name already exists!"
                    };
                }


                var course = _mapper.Map<Course>(model);


                _context.Entry(course).State = EntityState.Added;
                await _context.SaveChangesAsync();


                return new
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Course Created Successfully!"

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
         * @role Delete a Course
         * @param int id
         * @return status code with message
         */
        public async Task<object> DeleteAsync(int id)
        {
            try
            {
                var course = await _context.CourseList.FindAsync(id);
                if (course == null)
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Student does not exists!"
                    };
                }


                _context.CourseList.Remove(course);
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
         * @role find an course by his id
         * @param int id
         * @return status code with StudentInfoSystem.Models.Courses.ViewModels.CourseViewModel as data
         */
        public async Task<object> FindByIdAsync(int id)
        {
            try
            {
                var course = await _context.CourseList.SingleOrDefaultAsync(s => s.Id == id);
                if (course == null)
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Student does not exists!"
                    };
                }


                var courseModel = _mapper.Map<CourseViewModel>(course);


                return new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    Data = courseModel
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
         * @role Get a list of courses
         * @param StudentInfoSystem.Models.DataTable.DataTable dataTableDto
         * @return status code with List<StudentInfoSystem.Models.Courses.ViewModels.CourseViewModel> courseModels
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

                IQueryable<Course> courseAsQueryable = _context.CourseList;


                int recordsTotal = courseAsQueryable.Count();


                if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    courseAsQueryable = courseAsQueryable.Where(m => m.CourseName.Contains(searchValue)
                                                                       || m.CourseCode.Contains(searchValue)
                                                                       || m.CreditHour.Equals(searchValue)
                                                                       || m.ContactHour.Equals(searchValue));
                }

                int recordsFiltered = courseAsQueryable.Count();

                var courses = await courseAsQueryable.Select(m => new
                {
                    m.Id,
                    m.CourseCode,
                    m.CourseName,
                    m.CreditHour,
                    m.ContactHour
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                // }).Skip(start).Take(length).ToListAsync();

                return new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = courses
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
         * @role Get a simple list of courses
         * @param 
         * @return status code with StudentInfoSystem.Models.Courses.Entities.Course as data
         */
        public async Task<object> GetListAsync()
        {

            //return _userManager.Users.ToListAsync();
            try
            {
                var data = await _context.CourseList
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
         * @role Update courses information
         * @param StudentInfoSystem.Models.Courses.ViewModels.CourseViewModel model
         * @return status code with message
         */
        public async Task<object> UpdateAsync(CourseViewModel model)
        {
            try
            {
                if (await IsCourseNameExistsAsync(model.CourseName, model.Id))
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Course name already exists!"
                    };
                }


                if (await IsCourseCodeExistsAsync(model.CourseCode, model.Id))
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Course code already exists!"
                    };
                }

                var course = _mapper.Map<Course>(model);

                _context.Entry(course).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                return new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Course updated successfully!"
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
         * @role Check if course with same email already exists in database
         * @param string email, string Id
         * @return bool
         */
        private async Task<bool> IsCourseCodeExistsAsync(string code, int Id)
        {
            return await _context.CourseList.AnyAsync(e => e.CourseCode == code && e.Id != Id);
        }




        /**
         * @name IsContactNoExistsAsync
         * @role Check if course with same contact already exists in database
         * @param string contactNo, string Id
         * @return bool
         */
        private async Task<bool> IsCourseNameExistsAsync(string name, int id)
        {
            return await _context.CourseList.AnyAsync(s => s.CourseName == name && s.Id != id);
        }


    }
}
