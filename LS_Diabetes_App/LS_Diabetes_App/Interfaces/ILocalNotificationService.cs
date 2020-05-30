using System;

namespace LS_Diabetes_App.Interfaces
{
    public interface ILocalNotificationService
    {
        void LocalNotification(string title, string body, int id, DateTime notifyTime, int duration);

        void Cancel(int id);
    }
}