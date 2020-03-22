using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Category : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public Category ParentCategory { get; set; }

        public List<PartParameter> DefaultParameters { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

    }
}
