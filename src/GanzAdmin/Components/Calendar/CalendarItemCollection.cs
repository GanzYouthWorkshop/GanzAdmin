using GanzAdmin.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Components.Calendar
{
    public class CalendarItemCollection
    {
        public Type ConentType { get; set; }
        public string Color { get; set; }
        public Func<IEntity, string> DisplayText { get; set; }

        public List<IEvent> CollectedItems { get; set; }
    }
}
