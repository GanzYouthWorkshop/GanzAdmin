using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Payment : IEntity, IEvent
    {
        [BsonId]
        public long Id { get; set; }

        [BsonRef]
        public Member Member { get; set; }

        public DateTime Occasion { get; set; } = DateTime.Today;

        public int Value { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Occasion.ToString(); }
        }
    }
}
