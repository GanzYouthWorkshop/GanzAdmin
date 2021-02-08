using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Topic : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public List<Module> Modules { get; set; } = new List<Module>();
        public List<string> Tags { get; set; } = new List<string>();

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }
    }
}
