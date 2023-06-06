using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace GoalMonitoringApp.Core.Classes
{
    public class AppSettings
    {
        public AppSettings()
        {
            GetDatabasePath();
        }
        public static string DatabasePath;

        public string GetDatabasePath()
        {
            string databaseFilename = "database.db3";
            string databasePath = string.Empty;

            try
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseFilename);
                    DatabasePath = databasePath;

                }
                //else if (Device.RuntimePlatform == Device.iOS)
                //{
                //    string documentsPath = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User, true)[0];
                //    databasePath = Path.Combine(documentsPath, databaseFilename);
                //}

                return databasePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            
        }
    }
}
