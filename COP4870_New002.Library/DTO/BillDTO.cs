using System;
using COP4870_New002.Library.Models;

namespace COP4870_New002.Library.DTO
{
	public class BillDTO
	{
		public BillDTO()
		{
			bill = new Bill();
			Times = new List<Time>();
		}

		public BillDTO(Bill b, List<Time> ts)
		{
			this.bill = b;
			this.Times = ts;
		}

		public Bill bill { get; set; }
		public List<Time> Times { get; set; }
	}
}

