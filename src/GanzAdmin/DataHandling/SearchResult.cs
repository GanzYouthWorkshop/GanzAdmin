using GanzAdmin.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.DataHandling
{
    public class SearchResult
    {
        public string Display { get; set; }
        public Type DataType { get; set; }
        public string Url { get; set; }
        public string Information { get; set; }

        public string TypeDisplay
        {
            get
            {
                string result = "";

                switch(this.DataType.Name)
                {
                    case nameof(Part): result = "Alkatrész";  break;
                    case nameof(Category): result = "Kategória"; break;
                }

                return result;
            }
        }
    }
}
