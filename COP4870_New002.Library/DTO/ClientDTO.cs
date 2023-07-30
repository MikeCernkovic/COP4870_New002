using System;
using COP4870_New002.Library.Models;
using System.Xml.Linq;
using COP4870_New002.Library.Services;

namespace COP4870_New002.Library.DTO
{
	public class ClientDTO
	{
        public ClientDTO()
        {
            client = new Client();
            Projects = new List<Project>();
            Bills = new List<Bill>();
        }

        public ClientDTO(Client c, List<Project> ps, List<Bill> bs)
        {
            this.client = c;
            this.Projects = ps;
            this.Bills = bs;
        }

        public Client client { get; set; }

        public List<Project> Projects { get; set; }
        public List<Bill> Bills { get; set; }
    }
}

