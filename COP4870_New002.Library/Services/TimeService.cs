using System;
using COP4870_New002.Library.DTO;
using COP4870_New002.Library.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;

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
            //UpdateTimes();
        }

        //public List<Time> Times
        //{
        //    get { return times; }
        //}

        //public Time? Get(int id)
        //{
        //    var response = new WebRequestHandler()
        //            .Get($"/Time/{id}")
        //            .Result;

        //    return JsonConvert.DeserializeObject
        //        <Time>(response) ?? new Time();
        //}

        public void Add(Time? t)
        {
            if (t != null)
            {
                //update bill amount
                var response = new WebRequestHandler()
                    .Post("/Time/Add", t).Result;
            }
        }

        public void Delete(Time t)
        {
            var response = new WebRequestHandler()
                .Delete($"/Time/Delete/{t.Id}").Result;
        }
    }
}

