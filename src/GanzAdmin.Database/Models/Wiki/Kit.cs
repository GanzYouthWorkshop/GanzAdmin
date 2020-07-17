using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Kit : IEntity
    {
        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<KitPart> Parts { get; set; } = new List<KitPart>();

        public bool IsAvailableForBuild
        {
            get
            {
                bool result = true;

                foreach(KitPart part in this.Parts)
                {
                    if(part.Part.SumInStock < part.Amount)
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }
        }

        public string DisplayValue
        {
            get
            {
                return this.Name;
            }
        }
    }
}
