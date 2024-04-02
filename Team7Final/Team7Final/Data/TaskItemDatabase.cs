using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using Team7Final.Models;
using System.Threading.Tasks;

namespace Team7Final.Data
{
    public class TaskItemDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<TaskItemDatabase> Instance = new AsyncLazy<TaskItemDatabase>(async () =>
        {
            var instance = new TaskItemDatabase();
            CreateTableResult result = await Database.CreateTableAsync<TaskItem>();
            return instance;

        });

        public TaskItemDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<TaskItem>> GetItemsAsync()
        {
            return Database.Table<TaskItem>().ToListAsync();
        }

        public Task<List<TaskItem>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<TaskItem>("SELECT * FROM [TaskItem] WHERE [Done] = 0");
        }

        public Task<TaskItem> GetItemAsync(int id)
        {
            return Database.Table<TaskItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TaskItem item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TaskItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
