using System;
using System.Collections.Generic;
using System.Text;

namespace RemindMeAlready
{
    public interface INotificationHelper
    {
        void SendNotification();
        void SetUpAlarm();
    }
}
