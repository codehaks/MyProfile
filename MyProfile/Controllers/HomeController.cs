using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProfile.Data;
using MyProfile.Models;

namespace MyProfile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProfileDbContext _db;
        public HomeController(ProfileDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Index(string term=""
            ,OrderType orderType=OrderType.Name
            ,SortType sortType=SortType.Asc)
        {
            IEnumerable<User> model;
            if (string.IsNullOrEmpty(term))
            {
                model = _db.Users;
            }
            else
            {
                model = _db.Users.Where(u => u.Name
                .ToLower()
                .StartsWith(term.ToLower()));
            }

            switch (orderType)
            {
                case OrderType.Name:
                    model = model.OrderBy(u => u.Name);
                    break;
                case OrderType.Age:
                    model = model.OrderBy(u => u.Age);
                    break;
                default:
                    break;
            }

            if (sortType==SortType.Dsc)
            {
                model = model.Reverse();
            }

            ViewData["term"] = term;
            return View(model);
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
        public IActionResult Details(int id)
        {
            var user=_db.Users.Find(id);
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