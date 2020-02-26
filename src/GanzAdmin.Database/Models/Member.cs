using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Member
    {
        [BsonId]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public DateTime PaidUntil { get; set; }
        public List<Attendance> Attendances { get; set; }
        public Roles Role { get; set; }
    }
}
