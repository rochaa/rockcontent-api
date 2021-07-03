using System;
using Microsoft.AspNetCore.Mvc;

namespace RockContent.WebAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        public HomeController() { }

        [HttpGet]
        public string Get()
        {
            return new
            {
                service = "RockContent",
                success = true,
                version = "1.0",
                host = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            }
            .ToString();
        }
    }
}
