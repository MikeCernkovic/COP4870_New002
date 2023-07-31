using System;
using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using COP4870.API.EC;
using Microsoft.AspNetCore.Mvc;

namespace COP4870.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
	{
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Employee> Get()
        {
            return new EmployeeEC().Search();
        }

        [HttpGet("{id}")]
        public Employee? GetId(int id)
        {
            return new EmployeeEC().Get(id);
        }

        ////[HttpPost]
        ////public IEnumerable<Client> Search([FromBody] QueryMessage query)
        ////{
        ////    return new ClientEC().Search(query.Query);
        ////}

        [HttpDelete("Delete/{id}")]
        public void Delete(int id)
        {
            new EmployeeEC().Delete(id);
        }

        [HttpPost("Add")]
        public void AddOrUpdate([FromBody] Employee e)
        {
            new EmployeeEC().AddOrUpdate(e);
        }
    }
}

