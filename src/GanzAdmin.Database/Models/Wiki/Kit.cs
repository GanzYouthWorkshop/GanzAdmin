using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models.Wiki
{
    public class Kit : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<KitPart> Parts { get; set; } = new List<KitPart>();

        public string DisplayValue
        {
            get
            {
                return this.Name;
            }
        }
    }
}
