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
                    case nameof(WikiPage): result = "Wiki"; break;
                    case nameof(Member): result = "Tag"; break;
                    case nameof(Kit): result = "Kit"; break;
                    case nameof(MemberProject): result = "Projekt"; break;
                }

                return result;
            }
        }
    }
}
