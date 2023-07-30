using System;
using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using COP4870.API.EC;
using Microsoft.AspNetCore.Mvc;

namespace COP4870.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
	{
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ILogger<ProjectController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Project> Get()
        {
            return new ProjectEC().Search();
        }

        [HttpGet("{id}")]
        public ProjectDTO? GetId(int id)
        {
            return new ProjectEC().Get(id);
        }

        ////[HttpPost]
        ////public IEnumerable<Client> Search([FromBody] QueryMessage query)
        ////{
        ////    return new ClientEC().Search(query.Query);
        ////}

        [HttpDelete("Delete/{id}")]
        public void Delete(int id)
        {
            new ProjectEC().Delete(id);
        }

        [HttpPost("Add")]
        public void AddOrUpdate([FromBody] Project p)
        {
            new ProjectEC().AddOrUpdate(p);
        }
    }
}

