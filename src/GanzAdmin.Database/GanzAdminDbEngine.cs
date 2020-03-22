using GanzAdmin.Database.Models;
using GanzAdmin.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database
{
    public class GanzAdminDbEngine
    {
        public const string CONNECTION_STRING = @"Filename=_Data\ganzadmin.db; Mode=Shared";

        private bool m_IsDisposing;

        private LiteDatabase m_InnerDb;

        public static GanzAdminDbEngine Instance { get; } = new GanzAdminDbEngine();

        public ILiteCollection<Member> Members { get { return this.m_InnerDb.GetCollection<Member>().Include(i => i.Attendances); } }
        public ILiteCollection<Attendance> Attendances { get { return this.m_InnerDb.GetCollection<Attendance>(); } }

        public ILiteCollection<Event> Events { get { return this.m_InnerDb.GetCollection<Event>(); } }

        public ILiteCollection<Location> Locations { get { return this.m_InnerDb.GetCollection<Location>(); } }
        public ILiteCollection<Category> Categories { get { return this.m_InnerDb.GetCollection<Category>(); } }

        public void Dispose()
        {
            this.Transact();

            if (!this.m_IsDisposing)
            {
                this.m_InnerDb.Dispose();

                this.m_IsDisposing = true;
            }
        }

        public GanzAdminDbEngine()
        {
            this.m_InnerDb = new LiteDatabase(CONNECTION_STRING);
        }

        public void EnsureCreated()
        {
            Member adminMember = this.Members.FindOne(m => m.Username == "sa");
            if (adminMember == null)
            {
                this.Members.Insert(new Member()
                {
                    Active = true,
                    Address = "1039 Budapest Hímző utca 11.",
                    Name = "SuperAdmin",
                    PaidUntil = DateTime.Now.AddYears(100),
                    Password = GanzUtils.Sha256("admin"),
                    Phone = "+3699 999 9999",
                    Username = "sa",
                    Roles = new List<string>()
                    {
                        Permissions.Overlord,
                    }
                });
            }

            this.m_InnerDb.Checkpoint();
        }

        public ILiteCollection<T> GetCollection<T>()
        {
            if(typeof(T).Equals(typeof(Member)))
            {
                return (ILiteCollection<T>)this.Members;
            }
            return this.m_InnerDb.GetCollection<T>();
        }

        public void BeginTransaction()
        {
            this.m_InnerDb.BeginTrans();
        }

        public void Transact()
        {
            this.m_InnerDb.Commit();
            this.m_InnerDb.Checkpoint();
        }

        public void Rollback()
        {
            this.m_InnerDb.Rollback();
        }
    }
}
