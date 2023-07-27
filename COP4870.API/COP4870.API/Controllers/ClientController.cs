using System;
using COP4870_New002.Library.Models;
using COP4870_New002.Library.DTO;
using Microsoft.AspNetCore.Mvc;
using COP4870.API.EC;
using COP4870_New002.Library.Utilities;

namespace COP4870.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Client> Get()
        {
            return new ClientEC().Search();
        }

        [HttpGet("/{id}")]
        public ClientDTO? GetId(int id)
        {
            return new ClientEC().Get(id);
        }

        //[HttpPost]
        //public IEnumerable<Client> Search([FromBody] QueryMessage query)
        //{
        //    return new ClientEC().Search(query.Query);
        //}

        //[HttpDelete("Delete/{id}")]
        //public ClientDTO? Delete(int id)
        //{
        //    return new ClientEC().Delete(id);
        //}

        //[HttpPost]
        //public ClientDTO AddOrUpdate([FromBody] ClientDTO client)
        //{
        //    return new ClientEC().AddOrUpdate(client);
        //}
    }
}

