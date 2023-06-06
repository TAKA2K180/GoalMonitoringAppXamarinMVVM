using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.Classes;
using GoalMonitoringApp.Core.Classes;
using GoalMonitoringApp.Core.Services;
using GoalMonitoringApp.Data.Data;
using GoalMonitoringApp.Data.Services;
using GoalMonitoringApp.Helpers;
using GoalMonitoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoalMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalsListPage : ContentPage
    {
        public GoalsListPage()
        {
            try
            {
                InitializeComponent();

                // Specify the path for the database
                GoalFactory goalFactory = new GoalFactory();

                // Create an instance of GoalDatabase with the dbPath
                GoalDatabase database = new GoalDatabase(AppSettings.DatabasePath);

                // Create an instance of GoalRepository with the GoalDatabase instance
                IGoalRepository goalRepository = new GoalRepository(database);

                // Create an instance of GoalsListViewModel and pass the IGoalRepository instance
                var viewModel = new GoalsListViewModel(goalRepository);

                // Set the view model as the BindingContext
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                LogHelpers.SendLogToText(ex.Message);
            }
        }
    }
}