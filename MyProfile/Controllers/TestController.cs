using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyProfile.Controllers
{
    public class TestController : Controller
    {
        #region Test - Async Void

        
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

    }
}