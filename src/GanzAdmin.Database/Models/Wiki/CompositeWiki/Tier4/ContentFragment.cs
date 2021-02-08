using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class ContentFragment : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public DateTime LastModified { get; set; } = DateTime.Now;
        public int PointsWorth { get; set; } = 100;

        public string Content { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

          
    }
}
