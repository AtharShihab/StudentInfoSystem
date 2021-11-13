using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using StudentInfoSystem.Models.DataTable;
using StudentInfoSystem.Models.Configurations;
using StudentInfoSystem.Models.Responses;
using StudentInfoSystem.Services.Students;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentInfoSystem.Models.Students.ViewModels;

namespace StudentInfoSystem.Controllers.ApiControllers.Students
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }








        /**
         * @name AddStudent
         * @role create new student
         * @param StudentInfoSystem.Models.Students.ViewModels.StudentViewModel as model
         * @return statusCode as response
         */
        // POST: api/students/addstudent

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentViewModel model)
        {
            // check modelstate
            if (!ModelState.IsValid)

            {
                return BadRequestModelState();
            }

            var response = await _studentService.CreateAsync(model);
            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);


        }




        




        /**
         * @name LoadUserList
         * @role get Student list
         * @param StudentInfoSystem.Models.DataTable.DataTable as dataTableDto
         * @return anonymous object with StudentList as Data property
         */
        // POST: api/students/loadstudentlist
        [HttpPost]
        public async Task<object> LoadStudentList([FromBody] DataTable dataTableDto)
        {
            var response = await _studentService.GetDataTableAsync(dataTableDto);
            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
            // return new JsonResult(response);
        }





        /**
         * @name GetDetail
         * @role find Student Detail by id
         * @param int id
         * @return anonymous object with StudentInfoSystem.Models.Students.Entities.Student as Data property
         */
        // POST: api/students/getdetail/1

        [HttpGet("{id}")]
        [Route("getdetail/{id}")]
        public async Task<object> GetDetail(int id)
        {
            var response = await _studentService.FindByIdAsync(id);
            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }




        /**
         * @name UpdateStudent
         * @role update Student in database
         * @param StudentInfoSystem.Models.Students.ViewModels.StudentModel as model
         * @return status code with message
         */
        // Post api/students/updatestudent
        [HttpPost]
        public async Task<object> UpdateStudent([FromBody] StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var response = await _studentService.UpdateAsync(model);

            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }

        /**
         * @name GetStudents
         * @role get list of Students
         * @param 
         * @return anonymous object with List<StudentInfoSystem.Models.Students.Entities.Student> as Data property
         */
        // Post api/students/getstudents
        [HttpGet]
        public async Task<object> GetStudents()
        {
           

            var response = await _studentService.GetListAsync();

            var statusCodeValue = (int)response.GetType().GetProperty("StatusCode").GetValue(response);
            return StatusCode(statusCodeValue, response);
        }




        /**
         * @name DeleteStudent
         * @role Delete a Student
         * @param int id
         * @return statuscode
         */
        // Post api/students/deletestudent/1
        [HttpPost("{id}")]
        [Route("deletestudent/{id}")]
        public async Task<object> DeleteStudent(int id)
        {
            var response = await _studentService.DeleteAsync(id);

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
