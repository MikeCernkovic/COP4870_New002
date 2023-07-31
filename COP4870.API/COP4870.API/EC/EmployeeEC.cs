using System;
using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using COP4870.API.Database;

namespace COP4870.API.EC
{
	public class EmployeeEC
	{
        public List<Employee> Search(string query = "")
        {
            if (query == "")
            {
                return Filebase.Current.Employees;
            }
            return Filebase.Current.Employees
                .Where(c => c.Name.ToUpper()
                .Contains(query.ToUpper())).ToList();
        }

        public Employee? Get(int id)
        {
            return Filebase.Current.Employees
                 .FirstOrDefault(c => c.Id == id)
                 ?? new Employee();
        }

        public void AddOrUpdate(Employee c)
        {
            Filebase.Current.AddOrUpdate(c);
        }

        public void Delete(int id)
        {
            Filebase.Current.DeleteEmployee(id.ToString());
        }
    }
}

