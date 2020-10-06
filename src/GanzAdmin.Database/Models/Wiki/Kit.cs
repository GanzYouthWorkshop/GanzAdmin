using GanzAdmin.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Kit : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<KitPart> Parts { get; set; } = new List<KitPart>();

        public bool IsAvailableForBuild
        {
            get
            {
                bool result = true;

                foreach(KitPart part in this.Parts)
                {
                    if(part.Part.SumInStock < part.Amount)
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }
        }

        public string DisplayValue
        {
            get
            {
                return this.Name;
            }
        }

        public static List<Kit> Search(IEnumerable<Kit> kits, List<SearchFragment> expression)
        {
            List<Kit> list = new List<Kit>();
            list.AddRange(kits);
            IEnumerable<Kit> result = list;

            foreach (SearchFragment fragment in expression)
            {
                string searchKey = fragment.Key.Trim().ToLower();
                string searchVal = fragment.Value?.Trim().ToLower();
                float searchNumeric = float.NaN;
                float.TryParse(searchVal, out searchNumeric);

                if (fragment.Type == SearchFragment.ExpressionType.Main)
                {
                    if (searchKey != "")
                    {
                        result = result.Where(t =>
                            (t.Name != null && t.Name.ToLower().Contains(searchKey)) ||
                            (t.Description != null && t.Description.ToLower().Contains(searchKey))
                            );
                    }
                }
            }
            return result.ToList();
        }
    }
}
