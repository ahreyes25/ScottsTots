using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RemindMeAlready.Models
{
    [Table("Schedules")]
    public class Schedule
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Column("Day")]
        public string Day { get; set; }
        [Column("Time")]
        public int Time { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}
