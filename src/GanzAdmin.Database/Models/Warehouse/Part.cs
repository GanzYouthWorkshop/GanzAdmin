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

        [BsonRef]
        public List<Stockpile> Stock { get; set; } = new List<Stockpile>();

        public int MinimumStock { get; set; } = 0;


        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

    }
}
