using System;
using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using COP4870.API.EC;
using Microsoft.AspNetCore.Mvc;

namespace COP4870.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController : ControllerBase
    {
        private readonly ILogger<BillController> _logger;

        public BillController(ILogger<BillController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Bill> Get()
        {
            return new BillEC().GetAll();
        }

        [HttpGet("{id}")]
        public BillDTO? GetId(int id)
        {
            return new BillEC().Get(id);
        }

        ////[HttpPost]
        ////public IEnumerable<Client> Search([FromBody] QueryMessage query)
        ////{
        ////    return new ClientEC().Search(query.Query);
        ////}

        [HttpDelete("Delete/{id}")]
        public void Delete(int id)
        {
            new BillEC().Delete(id);
        }

        [HttpPost("Add")]
        public void AddOrUpdate([FromBody] Bill b)
        {
            new BillEC().AddOrUpdate(b);
        }
    }
}

