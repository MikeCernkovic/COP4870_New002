using System;
using COP4870_New002.Library.Models;

namespace COP4870_New002.Library.DTO
{
	public class ProjectDTO
	{
		public ProjectDTO()
		{
			ClientName = string.Empty;
			project = new Project();
			Bills = new List<Bill>();
		}

		public ProjectDTO(Project? p, List<Bill>? bs, string? cn)
		{
			this.ClientName = cn;
			this.project = p;
			this.Bills = bs;
		}

        public string? ClientName { get; set; }
        public Project? project { get; set; }
        public List<Bill>? Bills { get; set; }
    }
}

