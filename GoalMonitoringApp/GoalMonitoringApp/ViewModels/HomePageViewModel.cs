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
        #region Properties
        public RelayCommand ViewGoalsCommand { get; }
        public RelayCommand AddEditGoalCommand { get; }
        public RelayCommand LeftButtonCommand { get; }
        public RelayCommand MiddleButtonCommand { get; }
        public RelayCommand RightButtonCommand { get; }

        private readonly INavigation navigation;

        private int buttonSequenceIndex;
        private readonly int[] requiredButtonSequence = { 0, 1, 2 };

        private bool isAdminPageVisible;
        public bool IsAdminPageVisible
        {
            get { return isAdminPageVisible; }
            set
            {
                if (isAdminPageVisible != value)
                {
                    isAdminPageVisible = value;
                    OnPropertyChanged(nameof(IsAdminPageVisible));
                }
            }
        } 
        #endregion

        public HomePageViewModel(INavigation navigation)
        {
            GoalHelper.isFromList = false;
            GoalHelper.GoalbyId = null;
            ViewGoalsCommand = new RelayCommand(async () => await ExecuteViewGoalsCommand());
            AddEditGoalCommand = new RelayCommand(async () => await ExecuteAddGoalCommand());
            this.navigation = navigation;

            LeftButtonCommand = new RelayCommand(ShowAdminPageFromLeft);
            MiddleButtonCommand = new RelayCommand(ShowAdminPageFromMiddle);
            RightButtonCommand = new RelayCommand(ShowAdminPageFromRight);
        }

        private async Task ExecuteViewGoalsCommand()
        {
            await navigation.PushAsync(new GoalsListPage());
        }

        private async Task ExecuteAddGoalCommand()
        {
            await navigation.PushAsync(new GoalEditorPage());
        }

        private void ShowAdminPageFromLeft()
        {
            Task.Run(async () => await CheckButtonSequence(0));
        }

        private void ShowAdminPageFromMiddle()
        {
            Task.Run(async () => await CheckButtonSequence(1));
        }

        private void ShowAdminPageFromRight()
        {
            Task.Run(async () => await CheckButtonSequence(2));
        }

        private async Task CheckButtonSequence(int buttonIndex)
        {
            if (buttonIndex == requiredButtonSequence[buttonSequenceIndex])
            {
                buttonSequenceIndex++;
                if (buttonSequenceIndex == requiredButtonSequence.Length)
                {
                    await navigation.PushAsync(new AdminPage());
                    buttonSequenceIndex = 0;
                }
            }
            else
            {
                buttonSequenceIndex = 0;
            }
        }
    }
}
