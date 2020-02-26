using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Attendance
    {
        public Member Member { get; set; }
        
        public DateTime Occasion { get; set; }
        public TimeSpan Length { get; set; }
    }
}
