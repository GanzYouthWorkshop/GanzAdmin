using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class KitPart
    {
        [BsonRef]
        public Part Part { get; set; }

        public int Amount { get; set; }
    }
}
