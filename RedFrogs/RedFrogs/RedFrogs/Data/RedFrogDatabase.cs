using RedFrogs.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedFrogs.Data
{
    public class RedFrogDatabase
    {
        readonly SQLiteAsyncConnection database;

        public RedFrogDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Events>().Wait();
            database.CreateTableAsync<FeedBack>().Wait();

        }

        public Task<List<Events>> GetEventsAsync()
        {
            return database.Table<Events>().ToListAsync();
        }

        public Task<List<FeedBack>> GetFeedbacksAsync()
        {
            return database.Table<FeedBack>().ToListAsync();
        }

        public Task<Events> GetEventAsync(int id)
        {
            return database.Table<Events>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<FeedBack> GetFeedbackAsync(int id)
        {
            return database.Table<FeedBack>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
    }
}
