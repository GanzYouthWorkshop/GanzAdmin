using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }
        
        public DateTime Occasion { get; set; }
        public TimeSpan Length { get; set; }
    }
}
