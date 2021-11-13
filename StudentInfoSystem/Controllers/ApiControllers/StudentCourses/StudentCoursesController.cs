using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentInfoSystem.Models.DataTable;
using StudentInfoSystem.Models.Responses;
using StudentInfoSystem.Models.StudentCourses.ViewModels;
using StudentInfoSystem.Services.Semesters;
using StudentInfoSystem.Services.StudentCourses;

namespace StudentInfoSystem.Controllers.ApiControllers.StudentCourses
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {

        private readonly IStudentCourseService _studentCourseService;
        private readonly ISemesterRepo _semesterRepo;

        public StudentCoursesController(IStudentCourseService studentCourseService, ISemesterRepo semesterRepo)
        {
            _studentCourseService = studentCourseService;
            _semesterRepo = semesterRepo;
        }

        // POST: api/StudentCourses/AddStudentCourse

        [HttpPost]
        public async Task<IActionResult> AssigneCourses([FromBody] StudentCourseViewModel model)
        {
            // check modelstate
            if (!ModelState.IsValid)

            {
                return BadRequestModelState();
            }

            var response = await _studentCourseService.CreateAsync(model);
            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);


        }




        [HttpGet]
        public object GetSemesters()
        {


            var response = _semesterRepo.GetSemesterListAsync();

            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }



        // GET api/StudentCourses/getStudentCourses
        [HttpPost]
        public async Task<object> GetStudentCourses([FromBody] AssignedCourseQueryViewModel model)
        {


            var response = await _studentCourseService.GetAssignedCourse(model);

            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }




        // Post api/StudentCourses/deleteStudentCourse/1
        [HttpPost("{id}")]
        [Route("deletestudentcourse/{id}")]
        public async Task<object> DeleteStudentCourse(int id)
        {
            var response = await _studentCourseService.DeleteAsync(id);

            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }






        private IActionResult BadRequestModelState()
        {
            IEnumerable<string> errorMessages =
                ModelState.Values
                .SelectMany
                (
                    v => v.Errors.
                    Select(e => e.ErrorMessage)
                );

            return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse(errorMessages));
        }
    }
}
