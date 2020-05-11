using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Diabetes_App.Interfaces
{
    public interface ILocalNotificationService
    {
        void LocalNotification(string title, string body, int id, DateTime notifyTime);
        void Cancel(int id);
    }
}
