using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Part : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        
        [BsonRef]
        public Category Category { get; set; }

        public List<Stockpile> Stock { get; set; } = new List<Stockpile>();

        public int MinimumStock { get; set; } = 0;

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string ImagePath { get; set; }

        public string DateSheet { get; set; }


        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

    }
}
