﻿using GanzAdmin.Database.Models;
using GanzAdmin.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database
{
    public class GanzAdminDbEngine : IDisposable
    {
        private bool m_IsDisposing;

        private static string s_StandardConnectionString;
        private LiteDatabase m_InnerDb;

        public static GanzAdminDbEngine Instance { get; set; }

        public ILiteCollection<Member> Members { get { return this.m_InnerDb.GetCollection<Member>().Include(i => i.Attendances); } }

        public ILiteCollection<Attendance> Attendances { get { return this.m_InnerDb.GetCollection<Attendance>(); } }
        public ILiteCollection<Event> Events { get { return this.m_InnerDb.GetCollection<Event>(); } }

        public ILiteCollection<Location> Locations { get { return this.m_InnerDb.GetCollection<Location>(); } }
        public ILiteCollection<Category> Categories { get { return this.m_InnerDb.GetCollection<Category>(); } }
        
        public ILiteCollection<Part> Parts
        {
            get
            {
                return this.m_InnerDb.GetCollection<Part>()
                    .Include(BsonExpression.Create("$.Category"))
                    .Include(BsonExpression.Create("$.Stock[*].Location"))
                    .Include(BsonExpression.Create("$.SupplySources[*].Supplier"));
            }
        }

        public ILiteCollection<MemberProject> MemberProjects
        {
            get { return this.m_InnerDb.GetCollection<MemberProject>().Include(i => i.Members); }
        }

        public ILiteCollection<LogEntry> LogEntries
        {
            get { return this.m_InnerDb.GetCollection<LogEntry>().Include(i => i.Writer); }
        }

        public ILiteCollection<Kit> Kits
        {
            get
            {
                return this.m_InnerDb.GetCollection<Kit>()
                    .Include(BsonExpression.Create("$.Parts[*].Part"))
                    .Include(BsonExpression.Create("$.Parts[*].Part.Stock[*].Location"));
            }
        }

        public ILiteCollection<Payment> Payments
        {
            get
            {
                return this.m_InnerDb.GetCollection<Payment>()
                    .Include(BsonExpression.Create("$.Member"));
            }
        }

        public ILiteCollection<GlobalSetting> GlobalSettings { get { return this.m_InnerDb.GetCollection<GlobalSetting>(); } }

        public void Dispose()
        {
            if (!this.m_IsDisposing)
            {
                this.Transact();

                this.m_InnerDb.Dispose();

                this.m_IsDisposing = true;
            }
        }

        public static void InitializeSystem(string connectionString)
        {
            s_StandardConnectionString = connectionString;
        }

        public static GanzAdminDbEngine GetInstance()
        {
            return Instance;// new GanzAdminDbEngine(s_StandardConnectionString);
        }

        public GanzAdminDbEngine(string connectionString)
        {
            this.m_InnerDb = new LiteDatabase(connectionString);
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

            this.Transact();
        }

        public ILiteCollection<T> GetCollection<T>()
        {
            if(typeof(T).Equals(typeof(Member)))
            {
                return (ILiteCollection<T>)this.Members;
            }
            if(typeof(T).Equals(typeof(Part)))
            {
                return (ILiteCollection<T>)this.Parts;
            }
            if (typeof(T).Equals(typeof(MemberProject)))
            {
                return (ILiteCollection<T>)this.MemberProjects;
            }
            if (typeof(T).Equals(typeof(Kit)))
            {
                return (ILiteCollection<T>)this.Kits;
            }
            if (typeof(T).Equals(typeof(LogEntry)))
            {
                return (ILiteCollection<T>)this.LogEntries;
            }
            if (typeof(T).Equals(typeof(Payment)))
            {
                return (ILiteCollection<T>)this.Payments;
            }
            return this.m_InnerDb.GetCollection<T>();
        }

        public bool BeginTransaction()
        {
            return this.m_InnerDb.BeginTrans();
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
