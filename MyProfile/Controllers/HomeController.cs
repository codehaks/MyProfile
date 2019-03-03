using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MyProfile.Data;

namespace MyProfile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProfileDbContext _db;
        private readonly IDistributedCache _cache;

        public HomeController(ProfileDbContext dbContext, IDistributedCache cache)
        {
            _db = dbContext;
            _cache = cache;
        }

        public IActionResult Index()
        {
            var model = _db.Users;
            return View(model);
        }
        [Route("api/user")]
        public IActionResult Get()
        {
            var cachedName = _cache.GetString("name");

            if (!string.IsNullOrEmpty(cachedName))
            {
                return Ok(cachedName);
            }
            else
            {
                var data = DateTime.Now.ToString();
                _cache.SetString("name", data, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
                });

                return Ok(data);
            }


        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Models.User model)
        {
            _db.Users.Add(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateList()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateList(IEnumerable<Models.User> model)
        {
            _db.Users.AddRange(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateDynamic()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = _db.Users.Find(id);
            return View(user);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _db.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(Models.User model)
        {
            _db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _db.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(Models.User model)
        {
            _db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}