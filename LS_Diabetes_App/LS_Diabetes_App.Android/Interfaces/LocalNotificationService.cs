﻿using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Java.Lang;
using LS_Diabetes_App.Droid.Interfaces;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using System;
using System.IO;
using System.Xml.Serialization;

[assembly: Xamarin.Forms.Dependency(typeof(LocalNotificationService))]

namespace LS_Diabetes_App.Droid.Interfaces
{
    public class LocalNotificationService : ILocalNotificationService
    {
        private int _notificationIconId { get; set; }
        private readonly DateTime _jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        internal string _randomNumber;

        public void LocalNotification(string title, string body, int id, DateTime notifyTime, int duration)
        {
            if (duration > 0)
            {
                //long repeateDay = 1000 * 60 * 60 * 24;
                long repeateForMinute = 0; // In milliseconds
                long totalMilliSeconds = (long)(notifyTime.ToUniversalTime() - _jan1st1970).TotalMilliseconds;
                if (totalMilliSeconds < JavaSystem.CurrentTimeMillis())
                {
                    totalMilliSeconds = totalMilliSeconds + repeateForMinute;
                }

                var intent = CreateIntent(id);
                var localNotification = new LocalNotification();
                localNotification.Title = title;
                localNotification.Body = body;
                localNotification.Id = id;
                localNotification.NotifyTime = notifyTime;

                if (_notificationIconId != 0)
                {
                    localNotification.IconId = _notificationIconId;
                }
                else
                {
                    //  localNotification.IconId = Resource.Drawable.notificationgrey;
                }

                var serializedNotification = SerializeNotification(localNotification);
                intent.PutExtra(ScheduledAlarmHandler.LocalNotificationKey, serializedNotification);

                Random generator = new Random();
                _randomNumber = generator.Next(100000, 999999).ToString("D6");
                intent.SetData(Android.Net.Uri.Parse("myalarms://" + _randomNumber));
                var pendingIntent = PendingIntent.GetBroadcast(Application.Context, Convert.ToInt32(_randomNumber), intent, PendingIntentFlags.Immutable);
                var alarmManager = GetAlarmManager();
                alarmManager.SetRepeating(AlarmType.RtcWakeup, totalMilliSeconds, repeateForMinute, pendingIntent);
            }
            else
            {
                //long repeateDay = 1000 * 60 * 60 * 24;
                long repeateForMinute = 0; // In milliseconds
                long reminderInterval = AlarmManager.IntervalDay * 1;
                long totalMilliSeconds = (long)(notifyTime.ToUniversalTime() - _jan1st1970).TotalMilliseconds;
                if (totalMilliSeconds < JavaSystem.CurrentTimeMillis())
                {
                    totalMilliSeconds = totalMilliSeconds + repeateForMinute;
                }

                var intent = CreateIntent(id);
                var localNotification = new LocalNotification();
                localNotification.Title = title;
                localNotification.Body = body;
                localNotification.Id = id;
                localNotification.NotifyTime = notifyTime;

                if (_notificationIconId != 0)
                {
                    localNotification.IconId = _notificationIconId;
                }
                else
                {
                    //  localNotification.IconId = Resource.Drawable.notificationgrey;
                }

                var serializedNotification = SerializeNotification(localNotification);
                intent.PutExtra(ScheduledAlarmHandler.LocalNotificationKey, serializedNotification);

                Random generator = new Random();
                _randomNumber = generator.Next(100000, 999999).ToString("D6");
                intent.SetData(Android.Net.Uri.Parse("myalarms://" + _randomNumber));
                var pendingIntent = PendingIntent.GetBroadcast(Application.Context, Convert.ToInt32(_randomNumber), intent, PendingIntentFlags.Immutable);
                var alarmManager = GetAlarmManager();
                alarmManager.SetRepeating(AlarmType.RtcWakeup, totalMilliSeconds, reminderInterval, pendingIntent);
            }
        }

        public void Cancel(int id)
        {
            var intent = CreateIntent(id);
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, Convert.ToInt32(_randomNumber), intent, PendingIntentFlags.Immutable);
            var alarmManager = GetAlarmManager();
            alarmManager.Cancel(pendingIntent);
            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.CancelAll();
            notificationManager.Cancel(id);
        }

        public static Intent GetLauncherActivity()
        {
            var packageName = Application.Context.PackageName;
            return Application.Context.PackageManager.GetLaunchIntentForPackage(packageName);
        }

        private Intent CreateIntent(int id)
        {
            return new Intent(Application.Context, typeof(ScheduledAlarmHandler))
                .SetAction("LocalNotifierIntent" + id);
        }

        private AlarmManager GetAlarmManager()
        {
            var alarmManager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            return alarmManager;
        }

        private string SerializeNotification(LocalNotification notification)
        {
            var xmlSerializer = new XmlSerializer(notification.GetType());

            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, notification);
                return stringWriter.ToString();
            }
        }
    }
}