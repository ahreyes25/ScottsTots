using RemindMeAlready.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RemindMeAlready.Repositories
{
    public class ReminderRepository
    {
        SQLiteAsyncConnection conn;
        public string StatusMessage { get; set; }

        public ReminderRepository(SQLiteAsyncConnection conn)
        {
            this.conn = conn;
            conn.CreateTableAsync<Reminder>().Wait();
        }

        public async Task AddNewReminderAsync(string name)
        {
            int result = 0;
            try
            {
                if (string.IsNullOrEmpty(name)) 
                {
                    throw new Exception("Valid name required");
                }
                result = await conn.InsertAsync(new Reminder { Name = name });
                StatusMessage = $"{result} record(s) added [Name: {name}]";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
            }
        }
        public async Task<List<Reminder>> GetAllRemindersAsync()
        {
            try
            {
                return await conn.Table<Reminder>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Reminder>();
        }
    }
}
