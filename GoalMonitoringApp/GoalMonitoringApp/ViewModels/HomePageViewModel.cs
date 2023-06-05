using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GoalMonitoringApp.Views;
using Xamarin.Forms;

namespace GoalMonitoringApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public ICommand ViewGoalsCommand { get; }
        public ICommand AddEditGoalCommand { get; }

        public HomePageViewModel(INavigation navigation)
        {
            ViewGoalsCommand = new Command(async () => await navigation.PushAsync(new GoalsListPage()));
            AddEditGoalCommand = new Command(async () => await navigation.PushAsync(new GoalEditorPage()));
        }
    }
}
