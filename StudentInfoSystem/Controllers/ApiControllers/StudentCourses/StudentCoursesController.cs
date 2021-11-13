using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentInfoSystem.Models.DataTable;
using StudentInfoSystem.Models.Responses;
using StudentInfoSystem.Models.StudentCourses.ViewModels;
using StudentInfoSystem.Services.StudentCourses;

namespace StudentInfoSystem.Controllers.ApiControllers.StudentCourses
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {

        private readonly IStudentCourseService _studentCourseService;

        public StudentCoursesController(IStudentCourseService studentCourseService)
        {
            _studentCourseService = studentCourseService;
        }

        /**
         * @name AddStudentCourse
         * @role create new StudentCourse
         * @param StudentInfoSystem.Models.StudentCourses.ViewModels.StudentCourseViewModel as model
         * @return statusCode as response
         */
        // POST: api/StudentCourses/AddStudentCourse

        [HttpPost]
        public async Task<IActionResult> AddStudentCourse([FromBody] StudentCourseViewModel model)
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









        /**
         * @name LoadStudentCourseList
         * @role get StudentCourse list
         * @param StudentInfoSystem.Models.DataTable.DataTable as dataTableDto
         * @return anonymous object with StudentCourseList as Data property
         */
        // POST: api/StudentCourses/LoadStudentCourseList
        [HttpPost]
        public async Task<object> LoadStudentCourseList([FromBody] DataTable dataTableDto)
        {
            var response = await _studentCourseService.GetDataTableAsync(dataTableDto);
            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
            // return new JsonResult(response);
        }





        /**
         * @name GetDetail
         * @role find StudentCourse Detail by id
         * @param int id
         * @return anonymous object with StudentInfoSystem.Models.StudentCourses.Entities.StudentCourse as Data property
         */
        // POST: api/StudentCourses/getdetail/1

        [HttpGet("{id}")]
        [Route("getdetail/{id}")]
        public async Task<object> GetDetail(int id)
        {
            var response = await _studentCourseService.FindByIdAsync(id);
            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }




        /**
         * @name UpdateStudentCourse
         * @role update StudentCourse in database
         * @param StudentInfoSystem.Models.StudentCourses.ViewModels.StudentCourseViewModel as model
         * @return status code with message
         */
        // Post api/StudentCourses/updateStudentCourse
        [HttpPost]
        public async Task<object> UpdateStudentCourse([FromBody] StudentCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var response = await _studentCourseService.UpdateAsync(model);

            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }

        /**
         * @name GetStudentCourses
         * @role get list of StudentCourse
         * @param 
         * @return anonymous object with List<StudentInfoSystem.Models.StudentCourses.ViewModels.StudentCourseViewModel> as Data property
         */
        // Post api/StudentCourses/getStudentCourses
        [HttpGet]
        public async Task<object> GetStudentCourses()
        {


            var response = await _studentCourseService.GetListAsync();

            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }




        /**
         * @name DeleteStudentCourse
         * @role Delete a StudentCourse
         * @param int id
         * @return statuscode
         */
        // Post api/StudentCourses/deleteStudentCourse/1
        [HttpPost("{id}")]
        [Route("deletestudentcourse/{id}")]
        public async Task<object> DeleteStudentCourse(int id)
        {
            var response = await _studentCourseService.DeleteAsync(id);

            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }






        /**
         * @name BadRequestModelState
         * @role return modelstate errors
         * @param 
         * @return CheckmateErpApp.Models.Response.ErrorResponse as errorMessages
         */
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
