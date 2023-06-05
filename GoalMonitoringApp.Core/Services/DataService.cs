using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.Core.Models;

namespace GoalMonitoringApp.Core.Services
{
    class DataService : IDataService
    {
        private readonly IGoalRepository _goalRepository;

        public DataService(IGoalRepository goalRepository)
        {
            this._goalRepository = goalRepository;
        }
        public Task<int> DeleteGoalAsync(Goals goal)
        {
            return _goalRepository.DeleteGoalAsync(goal);
        }

        public Task<List<Goals>> GetAllGoalsAsync()
        {
            return _goalRepository.GetAllGoalsAsync();
        }

        public Task<int> SaveGoalAsync(Goals goal)
        {
            return _goalRepository.SaveGoalAsync(goal);
        }
    }
}
