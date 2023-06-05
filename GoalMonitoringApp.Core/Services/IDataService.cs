using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.Core.Models;

namespace GoalMonitoringApp.Core.Services
{
    public interface IDataService
    {
        Task<int> SaveGoalAsync(Goals goal);
        Task<int> DeleteGoalAsync(Goals goal);
        Task<List<Goals>> GetAllGoalsAsync();
    }

    public interface IDataServiceFactory
    {
        IDataService Create();
    }
}
