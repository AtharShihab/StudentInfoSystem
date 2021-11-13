using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using StudentInfoSystem.Models.DataTable;
using StudentInfoSystem.Models.Configurations;
using StudentInfoSystem.Models.Responses;
using StudentInfoSystem.Services.Courses;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentInfoSystem.Models.Courses.ViewModels;

namespace StudentInfoSystem.Controllers.ApiControllers.Courses
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }








        /**
         * @name AddCourse
         * @role create new course
         * @param StudentInfoSystem.Models.Courses.ViewModels.CourseViewModel as model
         * @return statusCode as response
         */
        // POST: api/courses/addcourse

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CourseViewModel model)
        {
            // check modelstate
            if (!ModelState.IsValid)

            {
                return BadRequestModelState();
            }

            var response = await _courseService.CreateAsync(model);
            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);


        }




        




        /**
         * @name LoadCourseList
         * @role get Course list
         * @param StudentInfoSystem.Models.DataTable.DataTable as dataTableDto
         * @return anonymous object with CourseList as Data property
         */
        // POST: api/courses/loadcourselist
        [HttpPost]
        public async Task<object> LoadCourseList([FromBody] DataTable dataTableDto)
        {
            var response = await _courseService.GetDataTableAsync(dataTableDto);
            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
            // return new JsonResult(response);
        }





        /**
         * @name GetDetail
         * @role find Course Detail by id
         * @param int id
         * @return anonymous object with StudentInfoSystem.Models.Courses.Entities.Course as Data property
         */
        // POST: api/courses/getdetail/1

        [HttpGet("{id}")]
        [Route("getdetail/{id}")]
        public async Task<object> GetDetail(int id)
        {
            var response = await _courseService.FindByIdAsync(id);
            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }




        /**
         * @name UpdateCourse
         * @role update Course in database
         * @param StudentInfoSystem.Models.Courses.ViewModels.CourseViewModel as model
         * @return status code with message
         */
        // Post api/courses/updatecourse
        [HttpPost]
        public async Task<object> UpdateCourse([FromBody] CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var response = await _courseService.UpdateAsync(model);

            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }

        /**
         * @name GetCourses
         * @role get list of course
         * @param 
         * @return anonymous object with List<StudentInfoSystem.Models.Courses.ViewModels.CourseViewModel> as Data property
         */
        // Post api/courses/getcourses
        [HttpGet]
        public async Task<object> GetCourses()
        {
           

            var response = await _courseService.GetListAsync();

            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }




        /**
         * @name DeleteCourse
         * @role Delete a Course
         * @param int id
         * @return statuscode
         */
        // Post api/courses/deletecourse/1
        [HttpPost("{id}")]
        [Route("deletecourse/{id}")]
        public async Task<object> DeleteCourse(int id)
        {
            var response = await _courseService.DeleteAsync(id);

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
