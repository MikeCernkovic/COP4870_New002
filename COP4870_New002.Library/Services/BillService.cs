using System;
using System.Collections.ObjectModel;
using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;

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
            UpdateBills();
        }

        public List<Bill> Bills
        {
            get { return bills ?? new List<Bill>(); }
        }

        public void UpdateBills()
        {
            var response = new WebRequestHandler()
                    .Get("/Bill")
                    .Result;

            bills = JsonConvert
                .DeserializeObject<List<Bill>>(response)
                ?? new List<Bill>();
        }

        public BillDTO? Get(int id)
        {
            var response = new WebRequestHandler()
                    .Get($"/Bill/{id}")
                    .Result;

            return JsonConvert.DeserializeObject
                <BillDTO>(response) ?? new BillDTO();
        }

        public void Add(Bill? p)
        {
            if (p != null)
            {
                var response = new WebRequestHandler()
                    .Post("/Bill/Add", p).Result;

                UpdateBills();
            }
        }

        public void Delete(Bill s)
        {
            var response = new WebRequestHandler()
                .Delete($"/Bill/Delete/{s.Id}").Result;

            UpdateBills();
        }
    }
}

