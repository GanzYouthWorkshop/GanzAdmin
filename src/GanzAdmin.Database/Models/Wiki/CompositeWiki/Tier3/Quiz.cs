using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Quiz : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Text { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
        public int PointsWorth
        {
            get
            {
                return this.Questions.Sum(Questions => Questions.PointsWorth);
            }
        }

        public List<string> Tags { get; set; } = new List<string>();

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Text; }
        }
    }
}
