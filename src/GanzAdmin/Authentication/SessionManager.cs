using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Authentication
{
    public class SessionManager
    {
        public class Session
        {
            public int MemberId { get; set; }
            public string SecurityToken { get; set; }
            public DateTime ExirationDateUtc { get; set; }
        }

        public object m_Lock;
        public List<Session> SessionEntries { get; set; }

        public List<Session> GetSessionsForMember(int id)
        {
            List<Session> result = null;
            lock (this.m_Lock)
            {
                this.SessionEntries.RemoveAll(s => s.ExirationDateUtc < DateTime.UtcNow);
                result = this.SessionEntries.Where(s => s.MemberId == id).ToList();
            }
            return result;
        }

        public Session RegisterNewSession(int id)
        {
            Session result = new Session()
            {
                MemberId = id,
                ExirationDateUtc = DateTime.UtcNow.AddMinutes(30),
                SecurityToken = Guid.NewGuid().ToString(),
            };

            lock (this.m_Lock)
            {
                this.SessionEntries.Add(result);
            }

            return result;
        }

        public void RevokeSession(string sessionToken)
        {
            lock (this.m_Lock)
            {
                this.SessionEntries.RemoveAll(s => s.SecurityToken == sessionToken);
            }
        }

        public int CheckTokenValidity(string sessionToken)
        {
            int result = 0;
            lock(this.m_Lock)
            {
                Session sessin = this.SessionEntries.FirstOrDefault(s => s.SecurityToken == sessionToken && s.ExirationDateUtc < DateTime.UtcNow);
                if(sessin != null)
                {
                    result = sessin.MemberId;
                }
            }

            return result;
        }
    }
}
