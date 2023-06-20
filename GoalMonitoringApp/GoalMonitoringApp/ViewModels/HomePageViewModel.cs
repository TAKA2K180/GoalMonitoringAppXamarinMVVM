using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GoalMonitoringApp.Commands;
using GoalMonitoringApp.Helpers;
using GoalMonitoringApp.Views;
using Plugin.Toast.Abstractions;
using Xamarin.Forms;

namespace GoalMonitoringApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public RelayCommand ViewGoalsCommand { get; }
        public RelayCommand AddEditGoalCommand { get; }

        private readonly INavigation navigation;

        public HomePageViewModel(INavigation navigation)
        {
            ViewGoalsCommand = new RelayCommand(async () => await ExecuteViewGoalsCommand());
            AddEditGoalCommand = new RelayCommand(async () => await ExecuteAddGoalCommand());
            this.navigation = navigation;
        }

        private async Task ExecuteViewGoalsCommand()
        {
            await navigation.PushAsync(new GoalsListPage());
        }

        private async Task ExecuteAddGoalCommand()
        {
            await navigation.PushAsync(new GoalEditorPage());
        }
    }
}
