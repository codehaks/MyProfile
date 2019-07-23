using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MyProfile.Data;
using MyProfile.Models;

namespace MyProfile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProfileDbContext _db;
        private IMemoryCache _cache;
        private static ILogger _logger;

        public HomeController(ProfileDbContext db, IMemoryCache cache, ILogger<HomeController> logger)
        {
            _cache = cache;
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IList<User> model;

            if (!_cache.TryGetValue("users", out model))
            {
                model = _db.Users.ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.NeverRemove)
                    //.SetAbsoluteExpiration(TimeSpan.FromDays(1))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(5))
                    .RegisterPostEvictionCallback(UsersCacheEvicted);
                                
                _cache.Set("users", model, cacheEntryOptions);
            }

            return View(model);
        }

        private static void UsersCacheEvicted(object key, object value, EvictionReason reason, object state)
        {
            _logger.LogWarning($"Users cache evicted :{reason} | state : {state}");

        }

        //[ResponseCache(Duration = 60)]
        [ResponseCache(CacheProfileName ="Default")]
        [Route("api/user")]
        public IActionResult Get()
        {
            IList<User> model;
            model = _db.Users.ToList();
            return Ok(model);
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
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
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