using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models.Files
{
    class FileObject : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Filename { get; set; }
        public string Path { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Filename; }
        }

    }
}
