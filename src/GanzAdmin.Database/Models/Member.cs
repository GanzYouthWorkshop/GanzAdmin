using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Username { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string Phone { get; set; }

        public bool Active { get; set; }
        public DateTime PaidUntil { get; set; }

        public List<Attendance> Attendances { get; set; }
    }
}
