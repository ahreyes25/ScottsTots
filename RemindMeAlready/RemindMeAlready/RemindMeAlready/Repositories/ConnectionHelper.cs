using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RemindMeAlready.Repositories
{
    public class ConnectionHelper
    {
        public SQLiteAsyncConnection Connection { get; set; }

        public ConnectionHelper(string dbPath)
        {
            Connection = new SQLiteAsyncConnection(dbPath);
        }
    }
}
