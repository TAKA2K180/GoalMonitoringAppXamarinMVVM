using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.Core.Models;
using GoalMonitoringApp.Core.Services;
using GoalMonitoringApp.Data.Data;

namespace GoalMonitoringApp.Data.Services
{
    public class GoalRepository : IGoalRepository
    {
        private readonly GoalDatabase database;

        public GoalRepository(GoalDatabase database)
        {
            this.database = database;
        }

        public Task<int> DeleteGoalAsync(Goals goal)
        {
            return database.DeleteGoalAsync(goal);
        }

        public Task<List<Goals>> GetAllGoalsAsync()
        {
            return database.GetAllGoalsAsync();
        }

        public Task<int> SaveGoalAsync(Goals goal)
        {
            return database.SaveGoalAsync(goal);
        }
    }
}
