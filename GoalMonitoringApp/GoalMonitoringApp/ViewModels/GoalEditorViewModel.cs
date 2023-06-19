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
using GoalMonitoringApp.Enums;
using static GoalMonitoringApp.Enums.NameEnums;
using System.Linq;
using GoalMonitoringApp.Helpers;

namespace GoalMonitoringApp.ViewModels
{
    public class GoalEditorViewModel : BaseViewModel
    {
        #region Variables
        private readonly IGoalRepository goalRepository;

        public event EventHandler GoalAdded;
        private readonly INavigation navigation;
        #endregion

        #region Properties
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

        private DateTime? _endDate;

        public DateTime? EndDate
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

        private bool _isFinished;

        public bool IsFinished
        {
            get { return _isFinished; }
            set { _isFinished = value; OnPropertyChanged("IsFinished"); }
        }

        private DateTime? _finishedDate;

        public DateTime? FinishedDate
        {
            get { return _finishedDate; }
            set { _finishedDate = value; OnPropertyChanged("FinishedDate"); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private NameEnums.GoalOwner _selectedOwner;

        public NameEnums.GoalOwner SelectedOwner
        {
            get { return _selectedOwner; }
            set { _selectedOwner = value; OnPropertyChanged("SelectedOwner");}
        }

        public List<NameEnums.GoalOwner> goalOwners { get; set; }

        public RelayCommand SaveGoalCommand { get; }
        public RelayCommand CancelCommand { get; }
        #endregion

        #region Constructor
        public GoalEditorViewModel(IGoalRepository goalRepository, INavigation navigation)
        {
            this.goalRepository = goalRepository;
            this.navigation = navigation;

            this.TargetDate = DateTime.Now;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.EndTime = DateTime.Now;
            this.IsFinished = false;
            goalOwners = Enum.GetValues(typeof(GoalOwner)).OfType<GoalOwner>().ToList();

            SaveGoalCommand = new RelayCommand(SaveGoal);
            CancelCommand = new RelayCommand(Cancel);

            if (GoalHelper.isFromList == true)
            {
                this.title = GoalHelper.GoalbyId.Title;
                this.description = GoalHelper.GoalbyId.Description;
                this.targetDate = GoalHelper.GoalbyId.TargetDate;
                this._endDate = GoalHelper.GoalbyId.FinishedDate;
                this.IsFinished = GoalHelper.GoalbyId.IsCompleted;
                this.Name = GoalHelper.GoalbyId.Name;
            }
        }
        #endregion


        #region Methods
        private async void SaveGoal()
        {
            try
            {
                // Create a new instance of the Goal model with the entered data
                if (this.Title != "" && this.Description != "" && this.Title != null && this.Description != null)
                {
                    if (IsFinished == true)
                    {
                        this.FinishedDate = DateTime.Now;
                    }
                    else
                    {
                        this.FinishedDate = null;
                    }

                    var goal = new Goals
                    {
                        Title = Title,
                        Description = Description,
                        TargetDate = TargetDate,
                        CreatedDate = DateTime.Now,
                        Id = Guid.Empty,
                        IsCompleted = IsFinished,
                        FinishedDate = FinishedDate,
                        Name = this.SelectedOwner.ToString()
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
        #endregion
    }
}
