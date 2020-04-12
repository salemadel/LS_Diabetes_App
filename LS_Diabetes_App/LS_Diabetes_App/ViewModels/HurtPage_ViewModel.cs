using LS_Diabetes_App.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class HurtPage_ViewModel : INotifyPropertyChanged
    {
       
        private bool isBusy { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                    isBusy = value;
                OnPropertyChanged();
            }
        }
        public HurtPage_ViewModel()
        {

        }
        public async Task ExecuteOnAppring()
        {
            IsBusy = true;
            await Task.Delay(1000);
            IsBusy = false;
            DependencyService.Get<IMessage>().ShortAlert("Aucune Montre est Connecté !");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
   
}
