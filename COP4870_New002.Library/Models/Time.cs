using System;
namespace COP4870_New002.Library.Models
{
	public class Time
	{
		public DateTime Date { get; set; }
		public string Narrative { get; set; }
		public int Hours { get; set; }
		public int ProjectId { get; set; }
		public int EmployeeId { get; set; }

		public Time()
		{
		}
	}
}

