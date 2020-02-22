using GanzAdmin.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database
{
    public class GanzAdminDbContext : DbContext
    {
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=GanzAdmin;Trusted_Connection=True;");
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    ModelBuilder mb = modelBuilder;

        //    mb.Entity<Member>(entity =>
        //    {
        //        entity.Property(e => e.MemberId).HasColumnName("ID");

        //        entity.Property(e => e.Username).HasMaxLength(50).IsUnicode();
        //        entity.Property(e => e.Password).HasMaxLength(50).IsUnicode();

        //        entity.Property(e => e.Name).HasMaxLength(50).IsUnicode();
        //        entity.Property(e => e.Address).HasMaxLength(100).IsUnicode();
        //        entity.Property(e => e.Phone).HasMaxLength(20).IsUnicode();
        //    });
        //}
    }
}
