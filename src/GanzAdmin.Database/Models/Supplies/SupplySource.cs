using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class SupplySource
    {
        [BsonRef]
        public Supplier Supplier { get; set; }

        public string SupplierItemId { get; set; }
    }
}
