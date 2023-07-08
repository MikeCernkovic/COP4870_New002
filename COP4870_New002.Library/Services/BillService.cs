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
            };
        }




    }
}

