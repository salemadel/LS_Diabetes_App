using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace LS_Diabetes_App.ViewModels.Profil_ViewModels
{
    public class Objectifs_ViewModel : INotifyPropertyChanged
    {
        private IDataStore DataStore { get; set; }
        private INavigation Navigation { get; set; }
        public Profil_Model Profil { get; set; }
        public Objectif_Model Objectifs { get; set; }
        private GlycemiaConverter GlycemiaConverter { get; set; }
        private WeightConverter WeightConverter { get; set; }
        public Command SaveCommand { get; set; }
        public Objectifs_ViewModel(IDataStore dataStore , INavigation navigation)
        {
            DataStore = dataStore;
            Navigation = navigation;
            Profil = DataStore.GetProfilAsync().First();
            GlycemiaConverter = new GlycemiaConverter();
            WeightConverter = new WeightConverter();
           
            Objectifs = DataStore.GetObjectifAsync().First();
            var new_max = GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit);
            var new_min = GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia, Profil.GlycemiaUnit);
            var new_weght = WeightConverter.DoubleWeightConvet(Objectifs.Weight_Objectif, Profil.WeightUnit);
            Objectifs.Max_Glycemia = new_max;
            Objectifs.Min_Glycemia = new_min;
            Objectifs.Weight_Objectif = new_weght;
           
            SaveCommand = new Command(async () =>
            {
                await ExecuteOnSaveObjectifs();
            });
        }
        private async Task ExecuteOnSaveObjectifs()
        {
            if ((Objectifs.Min_Glycemia <= Objectifs.Max_Glycemia) & Objectifs.Max_Glycemia > 0 & Objectifs.Min_Glycemia > 0)
            {
                if (await DependencyService.Get<IDialog>().AlertAsync("Info", "Voulez vous Modifier les objectifs ?", "Oui", "Non"))
                {
                    Objectifs.Max_Glycemia = GlycemiaConverter.DoubleGlycemiaConvertBack(Objectifs.Max_Glycemia, Profil.GlycemiaUnit);
                    Objectifs.Min_Glycemia = GlycemiaConverter.DoubleGlycemiaConvertBack(Objectifs.Min_Glycemia, Profil.GlycemiaUnit);
                    Objectifs.Weight_Objectif = WeightConverter.DoubleWeightConvetBack(Objectifs.Weight_Objectif, Profil.WeightUnit);
                    DataStore.UpdateObjectif(Objectifs);
                    MessagingCenter.Send(this, "DataUpdated");
                    await Navigation.PopModalAsync();
                }

            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Objectifs Glycemie Non Valide !");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
