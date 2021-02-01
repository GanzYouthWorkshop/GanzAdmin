using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using GanzAdmin.Database;
using GanzAdmin.Database.Models;
using System.Threading;

namespace GanzAdmin.Scheduling
{
    public class Scheduler
    {
        public static List<ScheduledTaskBase> Tasks { get; } = new List<ScheduledTaskBase>();

        public static void Start()
        {
            new Thread(new ThreadStart(Runner))
            {
                IsBackground = true,
                Name = "Scheduler Thread"
            }.Start();
        }

        private static void Runner()
        {
            while(true)
            {
                DateTime now = DateTime.Now;
                foreach(ScheduledTaskBase task in Tasks)
                {
                    if(task.LastRun.Add(task.RunSchedule) < now || task.CheckSpecialRunPermission())
                    {
                        task.Run(now);
                        task.LastRun = now;
                    }
                }
                Thread.Sleep(60000);
            }
        }
    }
}
