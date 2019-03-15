using RemindMeAlready.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RemindMeAlready
{
    public class InitialSchedule
    {
        private Dictionary<int, string> days = new Dictionary<int, string>
        {
            {0, "Monday" },
            {1, "Tuesday" },
            {2, "Wednesday" },
            {3, "Thursday" },
            {4, "Friday" },
            {5, "Saturday" },
            {6, "Sunday" }
        };
        public List<Schedule> InitialSchedules { get; set; }
        public InitialSchedule()
        {
            InitialSchedules = new List<Schedule>();
            foreach (var day in days)
            {
                var timePicker = new TimeSpan(9, 30, 0);
                InitialSchedules.Add(new Schedule
                {
                    Day = day.Value,
                    Id = day.Key,
                    IsActive = false,
                    Time = (int)timePicker.TotalMinutes
                });
            }
        }
    }
}
