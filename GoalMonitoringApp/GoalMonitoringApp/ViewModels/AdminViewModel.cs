using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GoalMonitoringApp.Core.Models;
using GoalMonitoringApp.Data.Services;
using System.Threading.Tasks;
using GoalMonitoringApp.Core.Services;
using GoalMonitoringApp.Commands;
using GoalMonitoringApp.Helpers;
using GoalMonitoringApp.Views;
using Xamarin.Forms;
using System.Linq;

namespace GoalMonitoringApp.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private ObservableCollection<Goals> goals;
        private readonly IGoalRepository goalRepository;
        private readonly INavigation navigation;

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

        public ObservableCollection<Goals> GoalList
        {
            get { return goals; }
            set
            {
                if (goals != value)
                {
                    goals = value;
                    OnPropertyChanged("GoalList");
                }
            }
        }

        private List<Goals> _goalbyId;
        public List<Goals> GoadbyId
        {
            get { return _goalbyId; }
            set { _goalbyId = value; OnPropertyChanged("GoalbyId"); }
        }

        private List<Goals> selectedGoal;
        public List<Goals> SelectedGoal
        {
            get { return selectedGoal; }
            set
            {
                selectedGoal = value;
                OnPropertyChanged(nameof(SelectedGoal));
            }
        }

        private bool _isItemSelected;

        public bool IsItemSelected
        {
            get { return _isItemSelected; }
            set { _isItemSelected = value; OnPropertyChanged("IsItemSelected"); }
        }

        public RelayCommand GetCheckedItem { get; }
        public RelayCommand DeleteSelected { get; }
        public RelayCommand ArchiveSelected { get; }
        public RelayCommand HomeCommand { get; }
        #endregion

        #region Constructor
        public AdminViewModel(IGoalRepository goalRepository)
        {
            this.goalRepository = goalRepository;

            Task.Run(async () => await LoadGoals());

            GetCheckedItem = new RelayCommand(async () => await GetCheckedGoal());
            DeleteSelected = new RelayCommand(async () => await DeleteCheckedGoal());
            ArchiveSelected = new RelayCommand(async () => await ArchiveSelectedGoal());
            HomeCommand = new RelayCommand(async () => await HomeClick());
        }
        #endregion

        #region Methods
        private async Task LoadGoals()
        {
            GoalList = new ObservableCollection<Goals>(await goalRepository.GetAllGoalsAsync());
            foreach (var item in GoalList)
            {
                this.Description = item.Description;
                this.Title = item.Title;
            }
        }

        private async Task GetCheckedGoal()
        {
            ObservableCollection<Goals> selectedGoal = new ObservableCollection<Goals>();

            foreach (var item in GoalList)
            {
                var id = await goalRepository.GetGoalsById(item.Id);

                selectedGoal.Add(id);
                IsItemSelected = true;
            }
        }

        private async Task DeleteCheckedGoal()
        {
            try
            {
                await GetCheckedGoal();
                if (selectedGoal != null)
                {
                    bool result = await Application.Current.MainPage.DisplayAlert("Confirmation", "Delete selected goal?", "Yes", "No");

                    if (result)
                    {
                        foreach (var item in selectedGoal)
                        {
                            await goalRepository.DeleteGoalById(item.Id);
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Notice", "Please select a goal first", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task ArchiveSelectedGoal()
        {
            try
            {
                await GetCheckedGoal();
                if (selectedGoal != null)
                {
                    bool result = await Application.Current.MainPage.DisplayAlert("Confirmation", "Archive selected goal?", "Yes", "No");
                    if (result)
                    {
                        foreach (var item in selectedGoal)
                        {
                            var archive = new Goals { Id = item.Id, isArchived = true };
                            await goalRepository.SaveGoalAsync(archive);
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Notice", "Please select a goal first", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }

        private async Task HomeClick()
        {
            await navigation.PushAsync(new HomePage());
        }
        #endregion

    }
}
