using GanzAdmin.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class MemberProject : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }
        public DateTime StartTime { get; set; }
        public string Logo { get; set; }

        [BsonRef]
        public List<Member> Members { get; set; } = new List<Member>();

        public List<LogEntry> ProjectLog { get; set; } = new List<LogEntry>();

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

        public static List<MemberProject> Search(IEnumerable<MemberProject> projects, List<SearchFragment> expression)
        {
            List<MemberProject> list = new List<MemberProject>();
            list.AddRange(projects);
            IEnumerable<MemberProject> result = list;

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
                        result = result.Where(t => t.Name.ToLower().Contains(searchKey) || t.Description.ToLower().Contains(searchKey));
                    }
                }
            }
            return result.ToList();
        }
    }
}
