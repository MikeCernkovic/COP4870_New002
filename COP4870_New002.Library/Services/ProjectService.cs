using COP4870_New002.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                new Project{ Id = 1, Name = "Clean Room",       IsActive = true, ClientId = 1, OpenDate = new DateTime(2019,05,30,0,0,0), Notes = "Random Note"},
                new Project{ Id = 2, Name = "Work on Homework", IsActive = true, ClientId = 2, CloseDate = new DateTime(2012,06,20,0,0,0), Notes = "Two assignments due"},
                new Project{ Id = 3, Name = "Build Spaceship",  IsActive = true, ClientId = 3, CloseDate = new DateTime(2013,07,09,0,0,0), Notes = "Watch the terminator"},
                new Project{ Id = 4, Name = "Fix the black pearl", IsActive = true, ClientId = 1, OpenDate = new DateTime(2016,08,09,0,0,0), Notes = "This is another note"},
                new Project{ Id = 5, Name = "Feed the dog",     IsActive = true, ClientId = 3, OpenDate = new DateTime(2010,09,09,0,0,0), Notes = "One more note"},
                new Project{ Id = 6, Name = "Random Project",   IsActive = true, ClientId = 4, CloseDate = new DateTime(2029,02,10,0,0,0), Notes = "I am tired of writing notes"},
                new Project{ Id = 7, Name = "Workout",          IsActive = true, ClientId = 4, OpenDate = new DateTime(2040,01,04,0,0,0), Notes = "The new spiderman movie is out"},
                new Project{ Id = 8, Name = "COP4870 Proj",     IsActive = true, ClientId = 2, OpenDate = new DateTime(2013,01,03,0,0,0), Notes = "Fool me twice shame"}
            };
        }

        public List<Project> Projects
        {
            get { return projects; }
        }

        public List<Project> SearchProjects(string query)
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
                var Bills = new ObservableCollection<Bill>(BillService.Current.Bills.Where(b => b.ProjectId == projectToRemove.Id));
                foreach (Bill bill in Bills)
                {
                    BillService.Current.Delete(bill.Id);
                }
                projects.Remove(projectToRemove);
            }
        }

        public void Delete(Project s)
        {
            Delete(s.Id);
        }
    }
}

