using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public interface IEntity
    {
        public long Id { get; set; }
        public string DisplayValue { get; }
    }
}
