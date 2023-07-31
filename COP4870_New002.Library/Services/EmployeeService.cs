using System;
using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;

namespace COP4870_New002.Library.Services
{
	public class EmployeeService
	{
        private static EmployeeService? instance;
        private static object _lock = new object();

        public static EmployeeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeService();
                    }
                }

                return instance;
            }
        }

        private List<Employee> employees;
        private EmployeeService()
        {
            UpdateEmployees();
        }

        public List<Employee> Employees
        {
            get { return employees ?? new List<Employee>(); }
        }

        public void UpdateEmployees()
        {
            var response = new WebRequestHandler()
                    .Get("/Employee")
                    .Result;

            employees = JsonConvert
                .DeserializeObject<List<Employee>>(response)
                ?? new List<Employee>();
        }

        public List<Employee> SearchEmployees(string query)
        {
            return Employees.Where(s => s.Name.ToUpper()
                            .Contains(query.ToUpper()))
                            .ToList();
        }

        public Employee? Get(int id)
        {
            var response = new WebRequestHandler()
                    .Get($"/Employee/{id}")
                    .Result;

            return JsonConvert.DeserializeObject
                <Employee>(response) ?? new Employee();
        }

        public void Add(Employee? e)
        {
            if (e != null)
            {
                var response = new WebRequestHandler()
                    .Post("/Employee/Add", e).Result;
                UpdateEmployees();
            }
        }

        public void Delete(Employee e)
        {
            var response = new WebRequestHandler()
                .Delete($"/Employee/Delete/{e.Id}").Result;

        }
    }
}

