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

namespace RemindMeAlready.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    [IntentFilter(new[] { "RemindMeAlready.Droid.SetAlarm" })]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //var resultIntent = new Intent(context, typeof(MainActivity));
            //resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

            //var pending = PendingIntent.GetActivity(context, 0, resultIntent, PendingIntentFlags.CancelCurrent);

            var notifHelper = new NotificationHelper();
            notifHelper.SendNotification();
        }
    }
}