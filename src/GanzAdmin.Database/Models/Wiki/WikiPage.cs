using GanzAdmin.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class WikiPage : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string LogoPath { get; set; }
        public bool IsCoded { get; set; }
        public DateTime LastModified { get; set; }
        public WikiPage ParentPage { get; set; }

        public string Content { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

        public override bool Equals(object obj)
        {
            return this.IsEqual(obj);
        }

        public static List<WikiPage> Search(IEnumerable<WikiPage> wikis, List<SearchFragment> expression)
        {
            List<WikiPage> list = new List<WikiPage>();
            list.AddRange(wikis);
            IEnumerable<WikiPage> result = list;

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
                            (t.Content != null && t.Content.Contains(searchKey))
                            );
                    }
                }
            }
            return result.ToList();
        }
    }
}
