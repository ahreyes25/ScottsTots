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
using Android.Support.V4.App;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using RemindMeAlready.Droid;
using Android.Support.V7.App;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationHelper))]
namespace RemindMeAlready.Droid
{
    public class NotificationHelper : INotificationHelper
    {
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";

        public void SendNotification()
        {
            var valuesForActivity = new Bundle();
            valuesForActivity.PutString("a", "not sure this will be used");

            //var stackBuilder = TaskStackBuilder.Create(Application.Context);
            //stackBuilder.AddParentStack(Class.FromType(typeof(SecondActivity)));
            //stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:
            //var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            var notificationBuilder = new NotificationCompat.Builder(Application.Context, CHANNEL_ID)
                                      .SetAutoCancel(true)
                                      .SetContentTitle("Button Clicked")
                                      .SetSmallIcon(Resource.Drawable.notify_panel_notification_icon_bg)
                                      .SetContentText($"The button was clicked...");

            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(NOTIFICATION_ID, notificationBuilder.Build());
        }

        public void SetUpAlarm()
        {
            Intent alarmIntent = new Intent(Application.Context, typeof(AlarmReceiver));
            alarmIntent.SetAction("SetAlarm");
            var pending = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

            var alarmManager = Application.Context.GetSystemService(Context.AlarmService).JavaCast<AlarmManager>();
            var alarmTime = (DateTime.UtcNow.AddMinutes(1.0).Ticks) / TimeSpan.TicksPerMillisecond; //todo change to ticks per day * 7
            var timefromMillis = alarmTime * TimeSpan.TicksPerMillisecond;
            var oneMinuteFromNow = new DateTime(timefromMillis);
            alarmManager.SetExact(AlarmType.RtcWakeup, alarmTime, pending); //todo change to ticks per day * 7
        }


    }
}