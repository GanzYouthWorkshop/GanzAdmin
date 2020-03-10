using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Stockpile : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        [BsonRef]
        public Location Location { get; set; }

        public long PartId { get; set; }
        public long Amount { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return ""; } //TODO
        }

    }
}
