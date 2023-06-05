using System;
using System.Collections.Generic;
using System.Text;
using GoalMonitoringApp.Core.Models;
using GoalMonitoringApp.Core.Services;
using System.Windows.Input;
using Xamarin.Forms;
using GoalMonitoringApp.Commands;

namespace GoalMonitoringApp.ViewModels
{
    public class GoalEditorViewModel : BaseViewModel
    {
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private DateTime targetDate;
        public DateTime TargetDate
        {
            get { return targetDate; }
            set
            {
                targetDate = value;
                OnPropertyChanged("TargetDate");
            }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged("StartDate"); }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged("EndDate"); }
        }



        public RelayCommand SaveGoalCommand { get; }
        public RelayCommand CancelCommand { get; }

        private readonly IGoalRepository goalRepository;

        public event EventHandler GoalAdded;
        private readonly INavigation navigation;

        public GoalEditorViewModel(IGoalRepository goalRepository, INavigation navigation)
        {
            this.goalRepository = goalRepository;
            this.navigation = navigation;

            SaveGoalCommand = new RelayCommand(SaveGoal);
            CancelCommand = new RelayCommand(Cancel);
        }



        private async void SaveGoal()
        {
            // Create a new instance of the Goal model with the entered data
            var goal = new Goals
            {
                Title = Title,
                Description = Description,
                TargetDate = TargetDate,
                CreatedDate = DateTime.Now,
            };

            // Save the goal using the repository
            await goalRepository.SaveGoalAsync(goal);

            // Raise the GoalAdded event
            GoalAdded?.Invoke(this, EventArgs.Empty);
        }

        public void Cancel()
        {
            navigation.PopAsync();
        }
    }
}
