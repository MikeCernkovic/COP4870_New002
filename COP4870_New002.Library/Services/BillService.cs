using System;
using COP4870_New002.Library.Models;

namespace COP4870_New002.Library.Services
{
	public class BillService
	{
        private static BillService? instance;
        private static object _lock = new object();

        public static BillService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new BillService();
                    }
                }

                return instance;
            }
        }

        private List<Bill> bills;
        private BillService()
        {
            bills = new List<Bill>
            {
                new Bill{ Id = 2, IsActive = true, ClientId = 1, ProjectId = 1, DueDate = new DateTime(2012,06,20,0,0,0)},
                new Bill{ Id = 3, IsActive = true, ClientId = 1, ProjectId = 1, DueDate = new DateTime(2013,07,09,0,0,0)},
                new Bill{ Id = 4, IsActive = true, ClientId = 1, ProjectId = 1, DueDate = new DateTime(2016,08,09,0,0,0)},
                new Bill{ Id = 5, IsActive = true, ClientId = 2, ProjectId = 1, DueDate = new DateTime(2010,09,09,0,0,0)},
                new Bill{ Id = 6, IsActive = true, ClientId = 2, ProjectId = 1, DueDate = new DateTime(2029,02,10,0,0,0)}
            };
        }

        public List<Bill> Bills
        {
            get { return bills; }
        }

        public Bill? Get(int id)
        {
            return Bills.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Bill bill)
        {

        }
    }
}

