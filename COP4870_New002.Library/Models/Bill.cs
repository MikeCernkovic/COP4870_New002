using System;
namespace COP4870_New002.Library
{
	public class Bill : ICloneable
	{
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public bool IsActive { get; set; }

        public Bill()
		{
		}

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

