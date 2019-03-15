using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RemindMeAlready.Models
{
    [Table("Reminders")]
    public class Reminder
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Name { get; set; }
        [Column("IsNotified")]
        public bool IsNotified { get; set; }
    }
}
