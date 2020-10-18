using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Theme : IEntity
    {
        public enum Types
        {
            Practical,
            Theoretical,
        }

        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public Types Type { get; set; }

        public List<Topic> Topics { get; } = new List<Topic>();

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }
    }
}
