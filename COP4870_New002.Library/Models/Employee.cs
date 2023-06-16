using System;
namespace COP4870_New002.Library.Models
{
	public class Employee
	{
        public int Id { get; set; } //converted from field to property because of get&set
		public double Rate { get; set; }
		public string? Name { get; set; }

        public Employee()
		{

		}
	}
}

