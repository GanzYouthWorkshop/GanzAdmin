using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class ContentFragment : IEntity
    {
        public const string CODE_SEPARATOR = "\n{{}}\n";

        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public bool IsCoded { get; set; }
        public DateTime LastModified { get; set; }

        public string Content { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

          
    }
}
