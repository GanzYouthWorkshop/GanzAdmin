using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Supplier : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Website { get; set; }
        public string ItemLinkTemplate { get; set; }

        public string DisplayValue
        {
            get { return this.Name; }
        }

        public override bool Equals(object obj)
        {
            return this.IsEqual(obj);
        }

    }
}
