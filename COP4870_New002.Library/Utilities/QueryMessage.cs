using System;
namespace COP4870_New002.Library.Utilities
{
    public class QueryMessage
    {
        private string? query;
        public string Query
        {
            get => query ?? string.Empty;
            set
            {
                query = value;
            }
        }
    }
}

