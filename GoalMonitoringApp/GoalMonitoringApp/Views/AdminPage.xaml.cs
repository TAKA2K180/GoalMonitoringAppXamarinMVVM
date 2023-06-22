using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.Core.Services;
using GoalMonitoringApp.Data.Data;
using GoalMonitoringApp.Data.Services;
using GoalMonitoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoalMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            InitializeComponent();
            try
            {
                // Create an instance of GoalDatabase with the dbPath
                GoalDatabase database = new GoalDatabase();

                // Create an instance of GoalRepository with the GoalDatabase instance
                IGoalRepository goalRepository = new GoalRepository(database);
                var admin = new AdminViewModel(goalRepository);
                BindingContext = admin;
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
            }
            
        }
    }
}