using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.Classes;
using GoalMonitoringApp.Core.Classes;
using GoalMonitoringApp.Data.Data;
using GoalMonitoringApp.Data.Services;
using GoalMonitoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoalMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalEditorPage : ContentPage
    {
        public GoalEditorPage()
        {
            InitializeComponent();


            var database = new GoalDatabase();
            var goalRepository = new GoalRepository(database);
            var navigation = Navigation;
            BindingContext = new GoalEditorViewModel(goalRepository, Navigation);
        }
    }
}