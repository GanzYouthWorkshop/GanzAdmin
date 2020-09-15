using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Scheduling
{
    public abstract class ScheduledTaskBase
    {
        public abstract DateTime LastRun { get; set; }
        public abstract TimeSpan RunSchedule { get; set; }

        public abstract bool Run(DateTime datetime);
    }

}
