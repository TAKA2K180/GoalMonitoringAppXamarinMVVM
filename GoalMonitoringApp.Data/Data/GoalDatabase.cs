using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.Core.Classes;
using GoalMonitoringApp.Core.Models;
using GoalMonitoringApp.Core.Services;
using GoalMonitoringApp.Helpers;
using SQLite;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;

namespace GoalMonitoringApp.Data.Data
{
    public class GoalDatabase : IGoalRepository
    {
        private SQLiteAsyncConnection database;

        public GoalDatabase()
        {
            try
            {
                database = new SQLiteAsyncConnection(AppSettings.DatabasePath);
                database.CreateTableAsync<Goals>().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
        }


        public async Task InitializeDatabaseAsync()
        {
            await database.CreateTableAsync<Goals>();
        }

        public async Task<List<Goals>> GetAllGoalsAsync()
        {
            return await database.Table<Goals>().ToListAsync();
        }

        public async Task<int> SaveGoalAsync(Goals goal)
        {
            try
            {
                if (goal.Id == Guid.Empty)
                {
                    goal.Id = Guid.NewGuid();
                    return await database.InsertAsync(goal);
                }
                else
                {
                    return await database.UpdateAsync(goal);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> DeleteGoalAsync(Goals goal)
        {
            return await database.DeleteAsync(goal);
        }
    }
}
