using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public interface IEvent
    {
        public DateTime Occasion { get; set; }
    }
}
