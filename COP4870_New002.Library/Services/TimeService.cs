using System;
using COP4870_New002.Library.Models;

namespace COP4870_New002.Library.Services
{
	public class TimeService
	{
        private static TimeService? instance;
        private static object _lock = new object();

        public static TimeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new TimeService();
                    }
                }

                return instance;
            }
        }

        private List<Time> times;
        private TimeService()
        {
            times = new List<Time>
            {
                new Time{Id=1, BillId = 2, Hours=0, EmployeeId=2, Narrative="Brushed teeth"},
                new Time{Id=2, BillId = 2, Hours=0, EmployeeId=5, Narrative="Finished Homework"},
                new Time{Id=3, BillId = 2, Hours=0, EmployeeId=2, Narrative="Walked the dog"},
                new Time{Id=4, BillId = 1, Hours=0, EmployeeId=3, Narrative="Got Milk"}
            };
        }

        public List<Time> Times
        {
            get { return times; }
        }

        public Time? Get(int id)
        {
            return Times.FirstOrDefault(t => t.Id == id);
        }

        public void Add(Time? time)
        {
            if (time != null)
            {
                time.Id = times.Last().Id + 1;
                times.Add(time);
            }
        }

        public void Delete(int id)
        {
            var timeToRemove = Get(id);
            if (timeToRemove != null)
            {
                times.Remove(timeToRemove);
            }
        }

        public void Delete(Time s)
        {
            Delete(s.Id);
        }
    }
}

