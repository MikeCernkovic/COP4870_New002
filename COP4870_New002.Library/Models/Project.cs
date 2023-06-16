using System;
namespace COP4870_New002.Library.Models
{
	public class Project : ICloneable
	{
        public int Id { get; set; } //converted from field to property because of get&set
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public bool IsActive { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public int ClientId { get; set; }
        public string Status
        {
            get
            {
                if (IsActive)
                {
                    return "Active";
                }
                else
                {
                    return "Not Active";
                }
            }
        }

        public override string ToString()
        {
            return $"{Id}\t\t{Name}";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

