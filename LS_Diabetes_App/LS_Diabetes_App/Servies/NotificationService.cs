using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LS_Diabetes_App.Servies
{
    public class NotificationService
    {
        public NotificationService(string title , string body)
        {
            CrossLocalNotifications.Current.Show(title, body);
        }
    }
}
