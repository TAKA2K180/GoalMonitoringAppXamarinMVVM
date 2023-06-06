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
        private AppSettings appSettings;

        public GoalDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(AppSettings.DatabasePath);
            database.CreateTableAsync<Goals>().ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                {
                    LogHelpers.SendLogToText(t.Result.ToString());
                }
            }, TaskScheduler.Current);
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

        public async Task<int> DeleteGoalAsync(Goals goal)
        {
            return await database.DeleteAsync(goal);
        }
    }
}
