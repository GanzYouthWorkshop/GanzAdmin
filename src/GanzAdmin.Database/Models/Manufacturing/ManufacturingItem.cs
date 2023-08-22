using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class ManufacturingItem
    {
        [BsonId]
        public long Id { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public DateTime EndTime { get; set; }
        public int  Progress { get; set; }
        public string ManufactureFileUrl { get; set; }

        //TODO
        [BsonIgnore]
        public string DisplayValue
        {
            get { return ""; }
        }

    }
}
