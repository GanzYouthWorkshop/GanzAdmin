﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace GanzNet.Authentication
{
    public class SessionManager
    {
        public class Session
        {
            public long MemberId { get; set; }
            public string SecurityToken { get; set; }
            public DateTime ExirationDateUtc { get; set; }
            public bool Remind { get; set; }
        }

        public object m_Lock = false;
        public List<Session> SessionEntries { get; set; } = new List<Session>();

        public List<Session> GetSessionsForMember(long id)
        {
            List<Session> result = null;
            lock (this.m_Lock)
            {
                this.RemoveExpiredSessions();

                result = this.SessionEntries.Where(s => s.MemberId == id).ToList();
            }
            return result;
        }

        public Session RegisterNewSession(long id, int daysToExpire, bool remind)
        {
            Session result = new Session()
            {
                MemberId = id,
                ExirationDateUtc = DateTime.UtcNow.AddDays(daysToExpire),
                SecurityToken = Guid.NewGuid().ToString(),
                Remind = remind
            };

            lock (this.m_Lock)
            {
                this.SessionEntries.Add(result);
            }

            return result;
        }

        public void RevokeSession(Session session)
        {
            lock (this.m_Lock)
            {
                this.SessionEntries.Remove(session);
            }
        }

        public Session CheckTokenValidity(string sessionToken)
        {
            Session result = null;
            lock(this.m_Lock)
            {
                this.RemoveExpiredSessions();

                Session session = this.SessionEntries.FirstOrDefault(s => s.SecurityToken == sessionToken && s.ExirationDateUtc > DateTime.UtcNow);
                if(session != null)
                {
                    if(session.Remind)
                    {
                        session.ExirationDateUtc.AddDays(30);
                    }

                    result = session;
                }
            }

            return result;
        }

        public Session CheckSessionValidity(Session session)
        {
            Session result = null;
            lock (this.m_Lock)
            {
                this.RemoveExpiredSessions();

                if(this.SessionEntries.Contains(session))
                {
                    if (session.Remind)
                    {
                        session.ExirationDateUtc.AddDays(30);
                    }

                    result = session;
                }
            }

            return result;
        }

        private void RemoveExpiredSessions()
        {
            lock (this.m_Lock)
            {
                this.SessionEntries.RemoveAll(s => s.ExirationDateUtc < DateTime.UtcNow);
            }
        }
    }
}
