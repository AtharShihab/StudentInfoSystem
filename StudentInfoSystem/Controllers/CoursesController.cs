using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentInfoSystem.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View("Courses");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
