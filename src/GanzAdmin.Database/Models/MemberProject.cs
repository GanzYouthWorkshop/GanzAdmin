using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class MemberProject : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }
        public DateTime StartTime { get; set; }
        public string Logo { get; set; }

        [BsonRef]
        public List<Member> Members { get; set; }

        public List<LogEntry> ProjectLog { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }
    }
}
