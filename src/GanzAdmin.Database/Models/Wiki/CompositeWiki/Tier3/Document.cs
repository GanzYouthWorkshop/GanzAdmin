using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Document : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }

        public List<ContentFragment> Fragments { get; } = new List<ContentFragment>();

        public int PointsWorth
        {
            get
            {
                return this.Fragments.Sum(f => f.PointsWorth);
            }
        }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }
    }
}
