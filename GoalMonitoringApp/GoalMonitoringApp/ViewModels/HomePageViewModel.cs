using System;
using System.Collections.Generic;
using System.Text;
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
            ViewGoalsCommand = new RelayCommand(ExecuteViewGoalsCommand);
            AddEditGoalCommand = new RelayCommand(ExecuteAddGoalCommand);
            this.navigation = navigation;
        }

        private void ExecuteViewGoalsCommand()
        {
            try
            {
                navigation.PushAsync(new GoalsListPage());
            }
            catch (Exception e)
            {
                LogHelpers.SendLogToText("[HomePageViewModel]" + e.Message);
            }
        }

        private void ExecuteAddGoalCommand()
        {
            try
            {
                navigation.PushAsync(new GoalEditorPage());
            }
            catch (Exception e)
            {
                //Xamarin.Forms.DependencyService.Get<IToastPopUp>().ShowToastMessage(e.Message);
                LogHelpers.SendLogToText("[HomePageViewModel]" + e.Message);
            }
        }
    }
}
