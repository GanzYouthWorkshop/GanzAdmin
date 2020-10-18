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
        public int Difficulty { get; set; } //todo

        public List<ContentFragment> Fragments { get; } = new List<ContentFragment>();

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }
    }
}
