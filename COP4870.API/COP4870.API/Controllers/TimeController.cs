using System;
using COP4870_New002.Library.Models;
using COP4870.API.EC;
using Microsoft.AspNetCore.Mvc;

namespace COP4870.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController
	{
        private readonly ILogger<TimeController> _logger;

        public TimeController(ILogger<TimeController> logger)
        {
            _logger = logger;
        }

        [HttpDelete("Delete/{id}")]
        public void Delete(int id)
        {
            new TimeEC().Delete(id);
        }

        [HttpPost("Add")]
        public void AddOrUpdate([FromBody] Time t)
        {
            new TimeEC().AddOrUpdate(t);
        }
    }
}

