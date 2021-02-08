using GanzAdmin.Database.Models.Wiki.CompositeWiki.SecondTier;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Module : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }

        public List<Document> Documents { get; } = new List<Document>();
        public List<Quiz> Quizes { get; } = new List<Quiz>();

        public List<string> Tags { get; set; } = new List<string>();

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }
    }
}
