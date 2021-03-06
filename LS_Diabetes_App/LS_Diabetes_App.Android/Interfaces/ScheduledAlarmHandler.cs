﻿using Android.Content;
using Android.Media;
using Android.OS;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using System.IO;
using System.Xml.Serialization;

namespace LS_Diabetes_App.Droid.Interfaces
{
    [BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
    public class ScheduledAlarmHandler : BroadcastReceiver
    {
        public const string LocalNotificationKey = "LocalNotification";

        public override void OnReceive(Context context, Intent intent)
        {
            var extra = intent.GetStringExtra(LocalNotificationKey);
            var notification = DeserializeNotification(extra);
            MediaPlayer player;
            player = MediaPlayer.Create(context, RingtoneManager.GetDefaultUri(RingtoneType.Notification));
            var v = (Vibrator)Android.App.Application.Context.GetSystemService(Android.App.Application.VibratorService);
            v.Vibrate(50);
            player.Start();
            var pushnotification = new NotificationService(notification.Title, notification.Body);
        }

        private LocalNotification DeserializeNotification(string notificationString)
        {
            var xmlSerializer = new XmlSerializer(typeof(LocalNotification));
            using (var stringReader = new StringReader(notificationString))
            {
                var notification = (LocalNotification)xmlSerializer.Deserialize(stringReader);
                return notification;
            }
        }
    }
}