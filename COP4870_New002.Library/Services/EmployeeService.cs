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
                new Employee{Id = 1, Name = "Milan", Rate = 0.90},
                new Employee{Id = 2, Name = "Mike", Rate = 0.40},
                new Employee{Id = 3, Name = "Alex", Rate = 0.70},
                new Employee{Id = 4, Name = "Katie", Rate = 0.30},
                new Employee{Id = 5, Name = "Christina", Rate = 0.70},
                new Employee{Id = 6, Name = "Noah", Rate = 0.80}
            };
        }

        public List<Employee> Employees
        {
            get { return employees; }
        }
    }
}

