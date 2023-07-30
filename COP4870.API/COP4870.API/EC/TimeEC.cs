using System;
using COP4870_New002.Library.Models;
using COP4870.API.Database;

namespace COP4870.API.EC
{
	public class TimeEC
	{
        public void AddOrUpdate(Time t)
        {
            Filebase.Current.AddOrUpdate(t);
        }

        public void Delete(int id)
        {
            Filebase.Current.DeleteTime(id.ToString());
        }
    }
}

