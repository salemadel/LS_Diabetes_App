using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LS_Diabetes_App.Servies
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public LocalizedResources Resources
        {
            get;
            private set;
        }

        public ViewModelBase()
        {
            Resources = new LocalizedResources(typeof(AppResources), App.CurrentLanguage);
        }

        public void OnPropertyChanged([CallerMemberName]string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}