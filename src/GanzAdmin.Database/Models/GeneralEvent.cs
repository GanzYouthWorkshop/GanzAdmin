using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class GeneralEvent : IEvent
    {
        public DateTime Occasion { get; set; }
    }
}
