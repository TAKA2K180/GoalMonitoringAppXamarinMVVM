using System;
using System.Collections.Generic;
using System.Text;
using GoalMonitoringApp.Core.Models;
using GoalMonitoringApp.Core.Services;
using System.Windows.Input;
using Xamarin.Forms;
using GoalMonitoringApp.Commands;
using Xamarin.Essentials;
using Android.Widget;

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

        private DateTime _endTime;

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
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

            this.TargetDate = DateTime.Now;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.EndTime = DateTime.Now;

            SaveGoalCommand = new RelayCommand(SaveGoal);
            CancelCommand = new RelayCommand(Cancel);
        }



        private async void SaveGoal()
        {
            try
            {
                // Create a new instance of the Goal model with the entered data
                if (this.Title != "" && this.Description != "" && this.Title != null && this.Description != null)
                {
                    var goal = new Goals
                    {
                        Title = Title,
                        Description = Description,
                        TargetDate = TargetDate,
                        CreatedDate = DateTime.Now,
                        Id = Guid.Empty
                    };

                    // Save the goal using the repository
                    await goalRepository.SaveGoalAsync(goal);

                    // Raise the GoalAdded event
                    GoalAdded?.Invoke(this, EventArgs.Empty);

                    await Application.Current.MainPage.DisplayAlert("Success", "Goal saved", "OK");
                    this.Title = "";
                    this.Description = "";
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Please fill required fields", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public void Cancel()
        {
            navigation.PopAsync();
        }
    }
}
