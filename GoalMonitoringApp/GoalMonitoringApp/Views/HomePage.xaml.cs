using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoalMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            HomePageViewModel homePageViewModel = new HomePageViewModel(Navigation);
            BindingContext = homePageViewModel;
        }
    }
}