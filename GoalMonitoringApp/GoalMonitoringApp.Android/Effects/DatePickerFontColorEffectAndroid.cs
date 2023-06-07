using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GoalMonitoringApp.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("GoalMonitoringApp")]
[assembly: ExportEffect(typeof(DatePickerFontColorEffectAndroid), "DatePickerFontColorEffect")]

namespace GoalMonitoringApp.Droid.Effects
{
    public class DatePickerFontColorEffectAndroid : PlatformEffect
    {
        protected override void OnAttached()
        {
            var control = Control as EditText;
            if (control != null)
            {
                control.SetTextColor(Color.Black.ToAndroid());
                control.SetBackgroundColor(Color.White.ToAndroid());
            }
        }

        protected override void OnDetached()
        {
            // Clean up if needed
        }
    }
}