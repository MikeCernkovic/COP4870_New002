using System;
namespace COP4870_New002.Library.Models
{
    public class Client : ICloneable
    {
        public int Id { get; set; } //converted from field to property because of get&set
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public bool IsActive { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

