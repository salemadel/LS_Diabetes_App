using LS_Diabetes_App.Servies;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Profil_ViewModels
{
    class Settings_ViewModel : ViewModelBase
    {
        public List<string> Languages { get; set; } = new List<string>()
        {
            "English",
            "Français",
            "العربية"
        };

        private string _SelectedLanguage;

        public string SelectedLanguage
        {
            get { return _SelectedLanguage; }
            set
            {
                switch(value)
                {
                    case "English":_SelectedLanguage = "EN";break;
                    case "Français":_SelectedLanguage = "FR";break;
                    case "العربية":_SelectedLanguage = "AR";break;
                    default:_SelectedLanguage = "EN";break;
                }
                SetLanguage();
            }
        }

        public Settings_ViewModel()
        {
            _SelectedLanguage = App.CurrentLanguage;
        }

        private void SetLanguage()
        {
            App.CurrentLanguage = SelectedLanguage;
            MessagingCenter.Send<object, CultureChangedMessage>(this,
                    string.Empty, new CultureChangedMessage(SelectedLanguage));
        }
    }
}
