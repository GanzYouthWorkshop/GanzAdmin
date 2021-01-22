using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using GanzAdmin.Database;
using GanzAdmin.Database.Models;
using System.Linq;

namespace GanzAdmin.Scheduling
{
    public class StatisticsScheduledTask : ScheduledTaskBase
    {
        public StatisticsScheduledTask()
        {
            this.RunSchedule = new TimeSpan(1, 0, 0, 0, 0);
        }

        public override bool Run(DateTime datetime)
        {
            Dictionary<string, string> Data = new Dictionary<string, string>()
            {
                { nameof(Statistic.StatisticTypes.Parts), GanzAdminDbEngine.Instance.Parts.Count().ToString() },
                { nameof(Statistic.StatisticTypes.Projects), GanzAdminDbEngine.Instance.MemberProjects.Count().ToString() },
                { nameof(Statistic.StatisticTypes.Members), GanzAdminDbEngine.Instance.Members.Count().ToString() },
                { nameof(Statistic.StatisticTypes.FinishedProjects), GanzAdminDbEngine.Instance.MemberProjects.Count(e => e.IsFinished).ToString() },
                { nameof(Statistic.StatisticTypes.MonthlyAttendance), GanzAdminDbEngine.Instance.Attendances.Count(a => a.Occasion.Year == datetime.Year && a.Occasion.Month == datetime.Month).ToString() },
                { nameof(Statistic.StatisticTypes.Stockpile), GanzAdminDbEngine.Instance.Parts.FindAll().Sum(p => p.SumInStock).ToString() },
            };

            GanzAdminDbEngine.Instance.BeginTransaction();

            foreach (KeyValuePair<string, string> stat in Data)
            {
                Statistic s = new Statistic()
                {
                    Date = datetime,
                    Name = stat.Key,
                    Value = stat.Value,
                };

                GanzAdminDbEngine.Instance.GetCollection<Statistic>().Insert(s);
            }

            GanzAdminDbEngine.Instance.Transact();

            return true;
        }
    }
}
