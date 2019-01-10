using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProfile.Models;

namespace MyProfile.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["now"] = DateTime.Now.TimeOfDay;
         
            return View();
        }
    }
}