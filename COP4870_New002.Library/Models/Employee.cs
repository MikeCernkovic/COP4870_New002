﻿using System;
namespace COP4870_New002.Library.Models
{
	public class Employee : ICloneable
	{
        public int Id { get; set; } //converted from field to property because of get&set
		public double Rate { get; set; }
		public string? Name { get; set; }

        public Employee()
		{

		}

        public override string ToString()
        {
            return $"{Id}\t\t{Name}\t\t{Rate}";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

