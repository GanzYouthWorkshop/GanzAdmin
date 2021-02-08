using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Question : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Text { get; set; }
        public List<string> PossibleAnswers { get; set; } = new List<string>();
        public int RightAnswerIndex { get; set; }

        public int PointsWorth { get; set; } = 1000;

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Text; }
        }
    }
}
