using System;
using COP4870_New002.Library.Models;

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
            employees = new List<Employee>
            {
                new Employee{Id = 1, Name = "Milan", PayRate = 0.90},
                new Employee{Id = 2, Name = "Mike", PayRate = 0.40},
                new Employee{Id = 3, Name = "Alex", PayRate = 0.70},
                new Employee{Id = 4, Name = "Katie", PayRate = 0.30},
                new Employee{Id = 5, Name = "Christina", PayRate = 0.80}
            };
        }

        public List<Employee> Employees
        {
            get { return employees; }
        }

        public List<Employee> SearchEmployees(string query)
        {
            return Employees.Where(s => s.Name.ToUpper()
                            .Contains(query.ToUpper()))
                            .ToList();
        }

        public Employee? Get(int id)
        {
            return Employees.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Employee? employee)
        {
            if (employee != null)
            {
                employee.Id = employees.Last().Id + 1;
                employees.Add(employee);
            }
        }

        public void Delete(int id)
        {
            var employeeToRemove = Get(id);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
            }
        }

        public void Delete(Employee s)
        {
            Delete(s.Id);
        }
    }
}

