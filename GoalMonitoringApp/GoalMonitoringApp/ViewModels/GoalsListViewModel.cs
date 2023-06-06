using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using GoalMonitoringApp.Core.Models;
using GoalMonitoringApp.Core.Services;
using GoalMonitoringApp.Helpers;
using GoalMonitoringApp.Views;
using Xamarin.Forms;

namespace GoalMonitoringApp.ViewModels
{
    public class GoalsListViewModel : BaseViewModel
    {
        private readonly IGoalRepository goalRepository;
        private ObservableCollection<Goals> goals;
        private readonly INavigation navigation;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Goals> Goals
        {
            get { return goals; }
            set
            {
                if (goals != value)
                {
                    goals = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Goals)));
                }
            }
        }

        public GoalsListViewModel(IGoalRepository goalRepository)
        {
            try
            {
                this.goalRepository = goalRepository;
                LoadGoals();
            }
            catch (Exception ex)
            {
                LogHelpers.SendLogToText(ex.Message);
            }
        }

        private async void LoadGoals()
        {
            Goals = new ObservableCollection<Goals>(await goalRepository.GetAllGoalsAsync());
        }

        private void AddGoal()
        {
            // Create a new instance of the GoalEditorViewModel
            
            var editorViewModel = new GoalEditorViewModel(goalRepository, navigation);

            // Create a new instance of the GoalEditorPage and pass the GoalEditorViewModel
            var editorPage = new GoalEditorPage();

            // Subscribe to the GoalEditorViewModel's event to handle when a new goal is added
            editorViewModel.GoalAdded += (sender, args) =>
            {
                // Reload the goals list after a new goal is added
                LoadGoals();
            };

            // Navigate to the GoalEditorPage
            App.Current.MainPage.Navigation.PushAsync(editorPage);
        }
    }
}
