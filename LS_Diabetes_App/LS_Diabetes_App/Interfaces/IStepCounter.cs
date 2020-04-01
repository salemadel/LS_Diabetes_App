using System.ComponentModel;

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