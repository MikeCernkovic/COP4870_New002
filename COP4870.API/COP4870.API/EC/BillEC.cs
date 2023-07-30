using System;
using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using COP4870.API.Database;

namespace COP4870.API.EC
{
	public class BillEC
	{
        public List<Bill> GetAll()
        {
            return Filebase.Current.Bills;
        }

        public BillDTO? Get(int id)
        {
            var bil = Filebase.Current.Bills
                .FirstOrDefault(c => c.Id == id)
                ?? new Bill();

            var times = Filebase.Current.Times
                .Where(b => b.BillId == bil.Id).ToList()
                ?? new List<Time>();

            return new BillDTO(bil, times);
        }

        public void AddOrUpdate(Bill b)
        {
            Filebase.Current.AddOrUpdate(b);
        }

        public void Delete(int id)
        {
            var ti = Filebase.Current.Times.Where(b => b.BillId == id).ToList();
            foreach (Time time in ti)
            {
                time.BillId = 0;
                Filebase.Current.AddOrUpdate(time);
            }
            Filebase.Current.DeleteBill(id.ToString());
        }
    }
}

