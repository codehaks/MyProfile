using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyProfile.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }
        [Route("task/all")]
        public async Task<IActionResult> Index()
        {
            var task1 = CheckUsers();
            var task2 = CheckEmails();

            _logger.LogInformation("WhenAll");
            var result = await Task.WhenAll(task1, task2);

            _logger.LogInformation("WhenAll -> result");
            return Ok();
        }

        private async Task<bool> CheckUsers()
        {
            _logger.LogInformation("Check Users -> started");
            await Task.Delay(10000);
            _logger.LogInformation("Check Users -> finished");
            return true;
        }

        private async Task<bool> CheckEmails()
        {
            _logger.LogInformation("Check Emails -> started");
            await Task.Delay(7000);
            _logger.LogInformation("Check Emails -> finished");
            return true;
        }
    }
}