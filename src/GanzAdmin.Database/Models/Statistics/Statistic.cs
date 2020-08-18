using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Statistic
    {
        public enum StatisticTypes : string
        {
            Parts = nameof(Parts),
            Stockpile = nameof(StockPile),
            YearsSinceOpening = nameof(YearsSinceOpening),
            MonthlyAttendance = nameof(MonthlyAttendance),
            Projects = nameof(Projects),
        }

        [BsonId]
        public long Id { get; set; }

        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
