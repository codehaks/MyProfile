using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProfile.Data;

namespace MyProfile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProfileDbContext _db;
        public HomeController(ProfileDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Index()
        {
            var model = _db.Users;
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}