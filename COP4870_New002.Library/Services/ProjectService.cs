using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;
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
            UpdateProjects();
        }

        public List<Project> Projects
        {
            get { return projects ?? new List<Project>(); }
        }

        public void UpdateProjects()
        {
            var response = new WebRequestHandler()
                    .Get("/Project")
                    .Result;

            projects = JsonConvert
                .DeserializeObject<List<Project>>(response)
                ?? new List<Project>();
        }

        public List<Project> SearchProjects(string query)
        {
            return Projects.Where(s => s.Name.ToUpper()
                            .Contains(query.ToUpper()))
                            .ToList();
        }

        public ProjectDTO? Get(int id)
        {
            var response = new WebRequestHandler()
                    .Get($"/Project/{id}")
                    .Result;

            return JsonConvert.DeserializeObject
                <ProjectDTO>(response) ?? new ProjectDTO();
        }

        public void Add(Project? p)
        {
            if (p != null)
            {
                var response = new WebRequestHandler()
                    .Post("/Project/Add", p).Result;
                UpdateProjects();
            }
        }

        public void Delete(Project s)
        {
            var response = new WebRequestHandler()
                .Delete($"/Project/Delete/{s.Id}").Result;

            UpdateProjects();
        }
    }
}

