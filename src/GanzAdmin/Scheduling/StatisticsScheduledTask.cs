using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using GanzAdmin.Database;
using GanzAdmin.Database.Model;

namespace GanzAdmin.Scheduling
{
    public class StatisticsScheduledTask : ScheduledTaskBase
    {
        public TimeSpan RunSchedule { get; set; } = new TimeSpan();

        public bool Run(DateTime datetime)
        {
            Statisic s = new w
            GanzAdminDbEngine.Instance.Parts.Count;
        }
    }
}
