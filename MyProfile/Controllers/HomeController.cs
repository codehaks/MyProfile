using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyProfile.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["message"] = "Hello from Controller!";
            return View();
        }
    }
}