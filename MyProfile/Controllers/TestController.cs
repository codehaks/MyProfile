using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyProfile.Controllers
{
    public class TestController : Controller
    {
        #region Test - Async Void

        [HttpGet("test/case1")]
        public async Task<IActionResult> Case1()
        {
            try
            {
                ThrowExceptionAsync();
            }
            catch (Exception)
            {
                // The exception is never caught here!
                throw;
            }

            return Ok();
        }

        private async void ThrowExceptionAsync()
        {
            await Task.Delay(1000);
            throw new InvalidOperationException();
        }

        #endregion

        #region Test - Async Task
        [HttpGet("test/case2")]
        public async Task<IActionResult> Case2()
        {
            try
            {
               await ThrowAsyncException();
            }
            catch (Exception)
            {
                // The exception is never caught here!
                throw;
            }

            return Ok();
        }
        private async Task ThrowAsyncException()
        {
            await Task.Delay(1000);
            throw new InvalidOperationException();
        }
        #endregion

        [HttpGet("test/await")]
        public async Task<IActionResult> Get()
        {
            var client = new HttpClient();
            var users = await client.GetStringAsync("https://reqres.in/api/users");
            var books = await client.GetStringAsync("https://fakerestapi.azurewebsites.net/api/Books");
            return Ok("");
        }

        [HttpGet("test/all")]
        public async Task<IActionResult> GetAll()
        {
            var client = new HttpClient();
            var usersTask =  client.GetStringAsync("https://reqres.in/api/users");
            var booksTask =  client.GetStringAsync("https://fakerestapi.azurewebsites.net/api/Books");
            var result=await Task.WhenAll(usersTask, booksTask);
            return Ok(result[0]);
        }

        [HttpGet("test/wait")]
        public async Task<IActionResult> GetWait()
        {
            var client = new HttpClient();
            var users =  await client.GetStringAsync("https://reqres.in/api/users");
            var books = await client.GetStringAsync("https://fakerestapi.azurewebsites.net/api/Books");

            //var result = await Task.WhenAll(usersTask, booksTask)
            return Ok(users);
        }

        public IActionResult GetUsers()
        {
            var client = new HttpClient();
            client.GetStringAsync("https://reqres.in/api/users").Wait();
            return Ok();
        }

    }
}