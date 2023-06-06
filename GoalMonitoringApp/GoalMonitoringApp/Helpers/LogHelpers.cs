using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoalMonitoringApp.Helpers
{
    public class LogHelpers
    {
        public static void SendLogToText(string Message)
        {
            var line = "\n";

            try
            {
                string filepath = "C:\\Users\\USER\\source\repos\\GoalMonitoringApp\\GoalMonitoringApp\\GoalMonitoringApp\\bin\\Debug\\netstandard2.0" + "\\Logs\\";  //Text File Path


                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".log";   //Text File Name

                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string body = "Log Written Date:" + " " + DateTime.Now.ToString() + "\n[" + Message + "]";
                    sw.WriteLine("-------------------------------Log Details-----------------------------------");
                    sw.WriteLine("-------------------------------------------------------------------------------");
                    sw.WriteLine(body);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }
            }
            catch (Exception e)
            {
                e.ToString();

            }
        }
    }
}
