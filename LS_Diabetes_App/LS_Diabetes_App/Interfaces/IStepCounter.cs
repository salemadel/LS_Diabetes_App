using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LS_Diabetes_App.Interfaces
{
    public interface IStepCounter : INotifyPropertyChanged
    {
        int Steps { get; set; }
        bool IsAvailable();
        void InitSensorService();

        void StopSensorService();

    }
}
