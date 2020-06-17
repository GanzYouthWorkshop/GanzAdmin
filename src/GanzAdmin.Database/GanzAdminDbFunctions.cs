using GanzAdmin.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GanzAdmin.Database
{
    public class GanzAdminDbFunctions
    {
        public static void LogAttendance(Member member, DateTime date)
        {
            Attendance result = new Attendance()
            {
                MemberId = member.Id,
                Length = new TimeSpan(3, 0, 0),
                Occasion = date
            };
            member.Attendances.Add(result);
            GanzAdminDbEngine.Instance.Attendances.Insert(result);
            GanzAdminDbEngine.Instance.Members.Update(member);
            GanzAdminDbEngine.Instance.Transact();
        }

        public static List<Tuple<int, string>> GetMembersForTags()
        {
            return MembersToTuples(GanzAdminDbEngine.Instance.Members.FindAll());
        }

        public static List<Tuple<int, string>> MembersToTuples(IEnumerable<Member> members)
        {
            return members.Select(m => new Tuple<int, string>((int)m.Id, m.Name)).ToList();
        }
    }
}
