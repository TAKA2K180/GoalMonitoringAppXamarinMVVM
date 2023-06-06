using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Foundation;
using GoalMonitoringApp.Core.Classes;
using GoalMonitoringApp.Core.Services;
using GoalMonitoringApp.Data.Data;
using GoalMonitoringApp.Data.Services;
using GoalMonitoringApp.ViewModels;
using Xamarin.Forms;

namespace GoalMonitoringApp.Classes
{
    public class GoalFactory
    {
        public static GoalViewModel CreateGoalViewModel()
        {
            string databasePath = DbHelper.cs;
            var goalDatabase = new GoalDatabase();
            var goalRepository = new GoalRepository(goalDatabase);
            return new GoalViewModel(goalRepository);
        }
    }
}
