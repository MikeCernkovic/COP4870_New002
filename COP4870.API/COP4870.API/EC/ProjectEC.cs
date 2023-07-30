using System;
using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using COP4870.API.Database;
using COP4870_New002.Library.Services;
using System.Reflection;

namespace COP4870.API.EC
{
	public class ProjectEC
	{
        public List<Project> Search(string query = "")
        {
            if (query == "")
            {
                return Filebase.Current.Projects;
            }
            return Filebase.Current.Projects
                .Where(p => p.Name.ToUpper()
                .Contains(query.ToUpper())).ToList();
        }

        public ProjectDTO? Get(int id)
        {
            var pro = Filebase.Current.Projects
                .FirstOrDefault(p => p.Id == id)
                ?? new Project();

            var bills = Filebase.Current.Bills
                .Where(b => b.ProjectId == pro.Id).ToList()
                ?? new List<Bill>();


            var cn = Filebase.Current.Clients
                .FirstOrDefault(c => c.Id == pro.ClientId);

            if(cn != null)
            {
                return new ProjectDTO(pro, bills, cn.Name);
            }
            return new ProjectDTO(pro, bills, string.Empty);
        }

        public void AddOrUpdate(Project p)
        {
            Filebase.Current.AddOrUpdate(p);
        }

        public void Delete(int id)
        {
            var bl = Filebase.Current.Bills.Where(b => b.ProjectId == id).ToList();
            foreach (Bill bill in bl)
            {
                bill.ProjectId = 0;
                Filebase.Current.AddOrUpdate(bill);
            }
            Filebase.Current.DeleteProject(id.ToString());
        }
    }
}

