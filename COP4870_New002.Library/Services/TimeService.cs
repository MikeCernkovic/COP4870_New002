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
                new Time{ProjectId = 1, EmployeeId=2, Narrative="inlwejfnlc"},
                new Time{ProjectId = 1, EmployeeId=2, Narrative="inlwejfnlc"},
                new Time{ProjectId = 1, EmployeeId=2, Narrative="inlwejfnlc"},
                new Time{ProjectId = 2, EmployeeId=1, Narrative="inlwejfnlc"}
            };
        }

        public List<Time> Times
        {
            get { return times; }
        }
    }
}

