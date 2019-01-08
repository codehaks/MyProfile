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
            //ViewData["message"] = "Hello from Controller!";
            ViewData["now"] = DateTime.Now.TimeOfDay;

            var model = new Message();
            model.Body = "Hello from Model!";

            return View(model);
        }
    }
}