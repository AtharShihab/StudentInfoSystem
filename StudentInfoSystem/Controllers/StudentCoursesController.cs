﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentInfoSystem.Controllers
{
    public class StudentCoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}