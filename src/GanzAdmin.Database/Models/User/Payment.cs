using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Payment : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public long MemberId { get; set; }-
        
        public DateTime Occasion { get; set; }

        public int Value { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Occasion.ToString(); }
        }
    }
}
