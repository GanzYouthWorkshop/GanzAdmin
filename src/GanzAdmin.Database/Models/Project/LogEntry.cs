using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class LogEntry
    {
        [BsonId]
        public long Id { get; set; }

        public DateTime StartTime { get; set; }
        public string Content { get; set; }

        [BsonRef]
        public Member Writer { get; set; }


        //TODO
        [BsonIgnore]
        public string DisplayValue
        {
            get { return ""; }
        }
    }
}
