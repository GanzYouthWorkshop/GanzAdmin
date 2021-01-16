using GanzAdmin.Database;
using GanzAdmin.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Components.Calendar
{
    public class CalendarItem
    {
        public IEvent Event { get; set; }
        public string Url { get; set; }
        public string DisplayText { get; set; }
    }

    public class CalendarItemCollection
    {
        public string Color { get; set; }
        public IEnumerable<CalendarItem> CollectedItems { get; set; }
        public Func<IEnumerable<CalendarItem>> Collect { get; set; }
    }

    public class CalendarItemCollection<T> : CalendarItemCollection where T : IEvent, IEntity
    {
        public List<T> DefaultItems
        {
            get
            {
                return GanzAdminDbEngine.Instance.GetCollection<T>().FindAll().ToList();
            }
        }
    }
}
