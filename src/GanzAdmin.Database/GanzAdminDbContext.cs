using GanzAdmin.Database.Models;
using LiteDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GanzAdmin.Utils;

namespace GanzAdmin.Database
{
    public class GanzAdminDbContext : IDisposable
    {
        public const string CONNECTION_STRING = @"_Data\litedb.bin";

        private bool m_IsDisposing;

        private LiteDatabase m_InnerDb;

        public ILiteCollection<Member> Members          { get { return this.m_InnerDb.GetCollection<Member>(); } }
        public ILiteCollection<Attendance> Attendances  { get { return this.m_InnerDb.GetCollection<Attendance>(); } }

        public void Dispose()
        {
            this.Transact();

            if(!this.m_IsDisposing)
            {
                this.m_InnerDb.Dispose();

                this.m_IsDisposing = true;
            }
        }

        public GanzAdminDbContext()
        {
            this.m_InnerDb = new LiteDatabase(CONNECTION_STRING);
            this.BeginTransaction();
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
                });
            }

            this.m_InnerDb.Checkpoint();
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
