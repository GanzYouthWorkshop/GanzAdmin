using GanzAdmin.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Part : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }

        [BsonRef]
        public Category Category { get; set; }

        public List<Stockpile> Stock { get; set; } = new List<Stockpile>();
        public int MinimumStock { get; set; } = 0;
        public string StockUnit { get; set; } = "db";

        public List<SupplySource> SupplySources { get; set; } = new List<SupplySource>();
        
        [BsonIgnore]
        public bool LowStock
        {
            get
            {
                return this.MinimumStock > this.Stock.Sum(s => s.Amount);
            }
        }

        [BsonIgnore]
        public long SumInStock
        {
            get
            {
                return this.Stock.Sum(s => s.Amount);
            }
        }

        public string ShortDescription { get; set; }
        public string DateSheetUrl { get; set; }

        public List<PartParameter> Parameters { get; set; } = new List<PartParameter>();


        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

        public override bool Equals(object obj)
        {
            return this.IsEqual(obj);
        }
        
        public static List<Part> Search(IEnumerable<Part> parts, List<SearchFragment> expression)
        {
            List<Part> list = new List<Part>();
            list.AddRange(parts);
            IEnumerable<Part> result = list;

            foreach(SearchFragment fragment in expression)
            {
                string searchKey = fragment.Key.Trim().ToLower();
                string searchVal = fragment.Value?.Trim().ToLower();
                float searchNumeric = float.NaN;
                float.TryParse(searchVal, out searchNumeric);

                switch (fragment.Type)
                {
                    case SearchFragment.ExpressionType.Main:
                        if (searchKey != "")
                        {
                            result = result.Where(t =>
                                (t.Name != null && t.Name.ToLower().Contains(searchKey)) ||
                                (t.ShortDescription != null && t.ShortDescription.ToLower().Contains(searchKey))
                                );
                        }
                        break;

                    case SearchFragment.ExpressionType.Attribute:
                        if(searchKey == "hiányos")
                        {
                            result = result.Where(t => t.LowStock);
                        }
                        break;

                    case SearchFragment.ExpressionType.SetTo:
                        result = result.Where(t => t.Parameters.FirstOrDefault(p => p.Name == searchKey)?.Value == searchVal);
                        break;

                    case SearchFragment.ExpressionType.EqualTo:
                        if(!float.IsNaN(searchNumeric))
                        {
                            result = result.Where(t => t.Parameters.FirstOrDefault(p => p.Name == searchKey)?.Value.ParseOrNaN() == searchNumeric);
                        }
                        break;

                    case SearchFragment.ExpressionType.GreaterThan:
                        searchVal = fragment.Value.Trim().ToLower();

                        result = result.Where(t => t.Parameters.FirstOrDefault(p => p.Name == searchKey)?.Value.ParseOrNaN() > searchNumeric);
                        break;

                    case SearchFragment.ExpressionType.LessThan:
                        searchVal = fragment.Value.Trim().ToLower();

                        result = result.Where(t => t.Parameters.FirstOrDefault(p => p.Name == searchKey)?.Value.ParseOrNaN() < searchNumeric);
                        break;
                }
            }
            return result.ToList();
        }
    }
}
