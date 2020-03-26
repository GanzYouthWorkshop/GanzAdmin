using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class WikiPage : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
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
    }
}
