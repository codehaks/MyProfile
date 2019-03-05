using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MyProfile.Data;
using MyProfile.Models;

namespace MyProfile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProfileDbContext _db;
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;
        public HomeController(ProfileDbContext dbContext, IDistributedCache cache, ILogger<HomeController> logger)
        {
            _db = dbContext;
            _cache = cache;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IList<User> model;

            var cachedUsers = _cache.Get("users");

            if (cachedUsers == null)
            {
                model = _db.Users.ToList();
                var bytes = ObjectToByteArray(model);
                _cache.Set("users", bytes, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1000)
                });
                _logger.LogWarning("Users cached to redis");
            }
            else
            {
                model = ByteArrayToObject(cachedUsers) as IList<User>;
                _logger.LogWarning("Users read from redis");
            }

            return View(model);
        }

        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
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