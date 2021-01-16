using GanzAdmin.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Member : IEntity
    {
        public enum Activities
        {
            Modelling,
            Electronics,
            Tutor
        }

        [BsonId]
        public long Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Activities MainActivity { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime PaidUntil { get; set; }
        public List<string> Roles { get; set; } = new List<string>();

        [BsonRef]
        public List<Attendance> Attendances { get; set; } = new List<Attendance>();

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

        public override bool Equals(object obj)
        {
            return this.IsEqual(obj);
        }

        public static List<Member> Search(IEnumerable<Member> members, List<SearchFragment> expression)
        {
            List<Member> list = new List<Member>();
            list.AddRange(members);
            IEnumerable<Member> result = list;

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
                            (t.Email != null && t.Email.ToLower().Contains(searchKey)) ||
                            (t.Phone != null && t.Phone.ToLower().Contains(searchKey))
                            );
                    }
                }
            }
            return result.ToList();
        }
    }
}
