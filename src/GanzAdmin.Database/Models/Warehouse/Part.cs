using GanzAdmin.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string StockUnit { get; set; }

        [BsonIgnore]
        public bool LowStock
        {
            get
            {
                return this.MinimumStock > this.Stock.Sum(s => s.Amount);
            }
        }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImagePath { get; set; }
        public string DateSheet { get; set; }

        public List<PartParameter> Parameters { get; set; } = new List<PartParameter>();


        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

        public static List<Part> Search(List<SearchFragment> expression)
        {
            return new List<Part>();
        }
    }
}
