using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Attendance
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public int MemberId { get; set; }
        
        public DateTime Occasion { get; set; }
        public TimeSpan Length { get; set; }
    }
}
