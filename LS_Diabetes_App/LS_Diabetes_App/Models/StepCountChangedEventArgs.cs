﻿using System;

namespace LS_Diabetes_App.Models
{
    public delegate void StepCountChangedEventHandler(Object sender, StepCountChangedEventArgs e);

    public class StepCountChangedEventArgs : EventArgs
    {
        public virtual double? Value { get; set; }
    }
}