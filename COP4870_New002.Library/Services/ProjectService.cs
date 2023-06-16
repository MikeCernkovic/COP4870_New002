using COP4870_New002.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace COP4870_New002.Library.Services
{
	public class ProjectService
	{
        private static ProjectService? instance;
        private static object _lock = new object();

        public static ProjectService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ProjectService();
                    }
                }

                return instance;
            }
        }

        private List<Project> projects;
        private ProjectService()
        {
            projects = new List<Project>
            {
                new Project{ Id = 1, Name = "Proj 1", IsActive = true, ClientId = 1},
                new Project{ Id = 2, Name = "Proj 2", IsActive = true, ClientId = 1},
                new Project{ Id = 3, Name = "Proj 3", IsActive = true, ClientId = 1},
                new Project{ Id = 4, Name = "Proj 4", IsActive = true, ClientId = 1},
                new Project{ Id = 5, Name = "Proj 5", IsActive = true, ClientId = 2},
                new Project{ Id = 6, Name = "Proj 6", IsActive = true, ClientId = 2},
                new Project{ Id = 7, Name = "Proj 7", IsActive = true, ClientId = 2},
                new Project{ Id = 8, Name = "Proj 8", IsActive = true, ClientId = 2},
                new Project{ Id = 9, Name = "Proj 9", IsActive = true, ClientId = 3},
            };
        }

        public List<Project> Projects
        {
            get { return projects; }
        }

        public List<Project> SearchClients(string query)
        {
            return Projects.Where(s => s.Name.ToUpper()
                            .Contains(query.ToUpper()))
                            .ToList();
        }

        public Project? Get(int id)
        {
            return Projects.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Project? project)
        {
            if (project != null)
            {
                project.Id = projects.Last().Id + 1;
                projects.Add(project);
            }
        }

        public void Delete(int id)
        {
            var projectToRemove = Get(id);
            if (projectToRemove != null)
            {
                projects.Remove(projectToRemove);
            }
        }

        public void Delete(Project s)
        {
            Delete(s.Id);
        }
    }
}

