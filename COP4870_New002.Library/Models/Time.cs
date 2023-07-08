using System;
namespace COP4870_New002.Library.Models
{
	public class Time : ICloneable
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Narrative { get; set; }
		public int Hours { get; set; }
		public int BillId { get; set; }
		public int EmployeeId { get; set; }

		public Time()
		{
		}

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

