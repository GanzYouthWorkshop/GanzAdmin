using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Event : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string> Permissions { get; set; }  
    }
}
