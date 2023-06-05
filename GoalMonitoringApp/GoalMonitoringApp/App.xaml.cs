using System;
using GoalMonitoringApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoalMonitoringApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            MainPage = new NavigationPage(new HomePage());
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var aggregateException = e.ExceptionObject as AggregateException;

            if (aggregateException != null)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    // Log or handle each inner exception here
                    // You can access the inner exception's details using innerException.Message, innerException.StackTrace, etc.
                }
            }
            else
            {
                var exception = e.ExceptionObject as Exception;
                // Log or handle the exception here
            }

            // Prevent the application from terminating
            Environment.FailFast("Unhandled exception occurred");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
