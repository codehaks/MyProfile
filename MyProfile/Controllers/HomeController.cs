using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyProfile.Data;
using MyProfile.Models;
using MyProfile.ViewModels;

namespace MyProfile.Controllers
{
    public class HomeController : Controller
    {
        const int PageSize = 5;

        private readonly ProfileDbContext _db;
        private readonly IMapper _mapper;
        public HomeController(ProfileDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        [Route("api/list")]
        public IActionResult List()
        {
            var model = _db.Users.Select(_mapper.Map<UserViewModel>);
            return Ok(model);
        }

        public IActionResult Index(string term=""
            ,OrderType orderType=OrderType.Name
            ,SortType sortType=SortType.Asc
            ,GenderType genderType=GenderType.None,
            int pageNumber=1)
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
                sortType = SortType.Asc;
            }
            else
            {
                sortType = SortType.Dsc;
            }

            if (genderType!=GenderType.None)
            {
                model = model.Where(u => u.Gender == genderType);
            }
            var count = model.Count();
            model = model.Skip((pageNumber - 1) * PageSize).Take(PageSize);

            var vm = new UserIndexModel
            {
                Users = model,
                GenderType=genderType,
                Term=term,
                SortType=sortType,
                PageNumber=pageNumber,
                PageSize=PageSize,
                OrderType=orderType,
                PageCount= (count / PageSize)+1
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserAddModel
            {
                Gender = GenderType.Male
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(UserAddModel model)
        {
            var user = _mapper.Map<UserAddModel, User>(model);
            _db.Users.Add(user);

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