using System;
using System.Collections.Generic;
using RemindMeAlready.Models;
using SQLite;
using System.Threading.Tasks;

namespace RemindMeAlready.Repositories
{
    public class ScheduleRepository
    {
        SQLiteAsyncConnection conn;
        public string StatusMessage { get; set; }

        public ScheduleRepository(SQLiteAsyncConnection conn)
        {
            this.conn = conn;
            conn.CreateTableAsync<Schedule>().Wait();
        }

        public async Task AddNewScheduleAsync(Schedule schedule)
        {
            int result = 0;
            try
            {
                if (schedule == null)
                {
                    throw new Exception("Valid schedule required");
                }
                result = await conn.InsertAsync(new Schedule
                {
                    Day = schedule.Day,
                    Id = schedule.Id,
                    IsActive = schedule.IsActive,
                    Time = schedule.Time
                });
                StatusMessage = $"{result} record(s) added for Day {schedule.Day}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to add {schedule.Day}. Error: {ex.Message}";
            }
        }

        public async Task UpdateScheduleAsync(Schedule schedule)
        {
            int result = 0;
            try
            {
                if (schedule == null)
                {
                    throw new Exception("Valid schedule required");
                }
                result = await conn.UpdateAsync(new Schedule
                {
                    Day = schedule.Day,
                    Id = schedule.Id,
                    IsActive = schedule.IsActive,
                    Time = schedule.Time
                });
                StatusMessage = $"{result} record(s) updated for Day {schedule.Day}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to update {schedule.Day}. Error: {ex.Message}";
            }
        }

        public async Task<List<Schedule>> GetAllSchedulesAsync()
        {
            List<Schedule> schedules = new List<Schedule>();
            try
            {
                schedules = await conn.Table<Schedule>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return schedules;
        }
    }
}
