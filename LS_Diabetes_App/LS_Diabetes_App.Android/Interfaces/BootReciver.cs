﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LS_Diabetes_App.Servies;

namespace LS_Diabetes_App.Droid.Interfaces
{
    [BroadcastReceiver]
    [IntentFilter(new[] { "android.intent.action.BOOT_COMPLETED", "android.intent.action.MY_PACKAGE_REPLACED" })]
    public class BootReciver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var stepServiceIntent = new Intent(context, typeof(StepCounter));
            context.StartService(stepServiceIntent);
            RappelService.ReRappel();
            RappelService.SetRappel();
        }
    }
}