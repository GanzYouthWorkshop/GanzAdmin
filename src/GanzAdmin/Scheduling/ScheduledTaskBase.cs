using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Scheduling
{
    public abstract class ScheduledTaskBase
    {
        public DateTime LastRun { get; set; }
        public TimeSpan RunSchedule { get; set; }

        public abstract bool Run(DateTime datetime);

        public virtual bool CheckSpecialRunPermission()
        {
            return false;
        }
    }

}
