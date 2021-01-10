using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Event : IEntity, IEvent
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public DateTime Occasion { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string> Permissions { get; set; }  

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }
    }
}
