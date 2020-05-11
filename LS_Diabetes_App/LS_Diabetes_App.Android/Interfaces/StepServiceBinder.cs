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

namespace LS_Diabetes_App.Droid.Interfaces
{
    public class StepServiceBinder : Binder
	{
		StepCounter stepService;
		public StepServiceBinder(StepCounter service)
		{
			this.stepService = service;
		}

		public StepCounter StepService
		{
			get { return stepService; }
		}
	}
}