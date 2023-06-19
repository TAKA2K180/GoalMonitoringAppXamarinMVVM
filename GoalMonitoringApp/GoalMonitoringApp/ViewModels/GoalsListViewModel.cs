using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using GoalMonitoringApp.Commands;
using GoalMonitoringApp.Core.Models;
using GoalMonitoringApp.Core.Services;
using GoalMonitoringApp.Helpers;
using GoalMonitoringApp.Views;
using Xamarin.Forms;

namespace GoalMonitoringApp.ViewModels
{
    public class GoalsListViewModel : BaseViewModel
    {
        #region Variables
        private readonly IGoalRepository goalRepository;
        private ObservableCollection<Goals> goals;
        private readonly INavigation navigation;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Goals> _list;
        #endregion


        #region Properties
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

        private Goals _goalbyId;

        public Goals GoadbyId
        {
            get { return _goalbyId; }
            set { _goalbyId = value; OnPropertyChanged("GoalbyId"); }
        }


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

        private Goals selectedGoal;
        public Goals SelectedGoal
        {
            get { return selectedGoal; }
            set
            {
                selectedGoal = value;
                OnPropertyChanged(nameof(SelectedGoal));
            }
        }

        public RelayCommand ItemTap { get; }

        public RelayCommand AddGoalButton { get; }
        #endregion



        #region Constructor
        public GoalsListViewModel(IGoalRepository goalRepository)
        {
            this.goalRepository = goalRepository;
            LoadGoals();

            ItemTap = new RelayCommand(ItemTapNavigate);
            AddGoalButton = new RelayCommand(AddGoal);
        }
        #endregion


        #region Methods
        private async void LoadGoals()
        {
            GoalList = new ObservableCollection<Goals>(await goalRepository.GetAllGoalsAsync());
            foreach (var item in GoalList)
            {
                this.Description = item.Description;
                this.Title = item.Title;
            }
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

        public async void ItemTapNavigate()
        {
            Goals selectedGoal = SelectedGoal;
            if (selectedGoal != null)
            {
                // Navigate to the GoalEditorPage with the selectedGoal

                var id = await goalRepository.GetGoalsById(selectedGoal.Id);

                GoadbyId = id;

                GoalHelper.GoalbyId = GoadbyId;

                GoalHelper.isFromList = true;

                INavigation navigation = DependencyService.Get<INavigation>();
                if (navigation != null)
                {
                    await navigation.PushAsync(new GoalEditorPage());
                }
            }
        } 
        #endregion
    }
}
