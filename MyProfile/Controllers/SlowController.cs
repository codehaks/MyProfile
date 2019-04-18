using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyProfile.Controllers
{
    public class SlowController : Controller
    {
        [Route("/slow-async")]
        public async Task<IActionResult> SlowAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            return Ok("async done!");
        }

        [Route("/slow-sync")]
        public IActionResult SlowSync()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            return Ok("sync done!");
        }
    }
}