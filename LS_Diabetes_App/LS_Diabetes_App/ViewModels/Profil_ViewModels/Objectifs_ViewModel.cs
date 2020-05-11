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
        public Objectifs_ViewModel(IDataStore dataStore , INavigation navigation , Profil_Model profil)
        {
            DataStore = dataStore;
            Navigation = navigation;
            Profil = profil;
            GlycemiaConverter = new GlycemiaConverter();
            WeightConverter = new WeightConverter();
            if(DataStore.GetObjectifAsync().Count() > 0)
            {
                Objectifs = DataStore.GetObjectifAsync().First();
                Objectifs.Max_Glycemia = GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit);
                Objectifs.Min_Glycemia = GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia, Profil.GlycemiaUnit);
                Objectifs.Weight_Objectif = WeightConverter.DoubleWeightConvet(Objectifs.Weight_Objectif, Profil.WeightUnit);
            }
            SaveCommand = new Command(async () =>
            {
                await ExecuteOnSaveObjectifs();
            });
        }
        private async Task ExecuteOnSaveObjectifs()
        {
            if (Objectifs.Min_Glycemia <= Objectifs.Max_Glycemia)
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
