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

            using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
            {
                db.Attendances.Insert(result);
                db.Members.Update(member);
                db.Transact();
            }
        }

        public static List<Tuple<int, string>> GetMembersForTags()
        {
            using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
            {
                return MembersToTuples(db.Members.FindAll());
            }
        }

        public static List<Tuple<int, string>> MembersToTuples(IEnumerable<Member> members)
        {
            return members.Select(m => new Tuple<int, string>((int)m.Id, m.Name)).ToList();
        }
    }
}
