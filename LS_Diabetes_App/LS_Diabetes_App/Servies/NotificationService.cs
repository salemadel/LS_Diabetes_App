using Plugin.LocalNotifications;

namespace LS_Diabetes_App.Servies
{
    public class NotificationService
    {
        public NotificationService(string title, string body)
        {
            CrossLocalNotifications.Current.Show(title, body);
        }
    }
}