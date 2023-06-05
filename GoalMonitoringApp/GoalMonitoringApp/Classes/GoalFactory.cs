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
        AppSettings appSettings = new AppSettings();
        public static string dbPath = AppSettings.DatabasePath;
        public static GoalViewModel CreateGoalViewModel()
        {
            string databasePath = dbPath;
            var goalDatabase = new GoalDatabase(databasePath);
            var goalRepository = new GoalRepository(goalDatabase);
            return new GoalViewModel(goalRepository);
        }
    }
}
