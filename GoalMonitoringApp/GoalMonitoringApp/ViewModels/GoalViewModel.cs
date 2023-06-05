using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.Commands;
using GoalMonitoringApp.Core.Models;
using GoalMonitoringApp.Core.Services;

namespace GoalMonitoringApp.ViewModels
{
    public class GoalViewModel : BaseViewModel
    {
        #region Properties

        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set { _id = value;}
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged("Title"); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
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

        private bool _isFinished;

        public bool IsFinished
        {
            get { return _isFinished; }
            set { _isFinished = value; OnPropertyChanged("IsFinished"); }
        }

        private DateTime _targetDate;

        public DateTime TargetDate
        {
            get { return _targetDate; }
            set { _targetDate = value; OnPropertyChanged("TargetDate"); }
        }


        private readonly IGoalRepository goalRepository;

        public ObservableCollection<Goals> Goals { get; set; }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        #endregion

        #region Constructor

        public GoalViewModel(IGoalRepository goalRepository)
        {
            this.goalRepository = goalRepository;
            Goals = new ObservableCollection<Goals>();
        }

        #endregion

        #region Methods

        private async void SaveGoal()
        {
            // Combine the date and time to create the target datetime
            DateTime targetDateTime = TargetDate;

            // Create the Goal object with the entered values
            var goal = new Goals
            {
                Title = Title,
                Description = Description,
                TargetDate = targetDateTime
            };

            // Call the SaveGoalAsync method in the repository to save the goal
            await goalRepository.SaveGoalAsync(goal);

            // Reload the goals after saving
            await LoadGoalsAsync();
        }

        public async Task LoadGoalsAsync()
        {
            var goals = await goalRepository.GetAllGoalsAsync();
            Goals.Clear();
            foreach (var goal in goals)
            {
                Goals.Add(goal);
            }
        }

        public async Task SaveGoalAsync(Goals goal)
        {
            await goalRepository.SaveGoalAsync(goal);
            await LoadGoalsAsync();
        }

        public async Task DeleteGoalAsync(Goals goal)
        {
            await goalRepository.DeleteGoalAsync(goal);
            await LoadGoalsAsync();
        }


        #endregion
    }
}
