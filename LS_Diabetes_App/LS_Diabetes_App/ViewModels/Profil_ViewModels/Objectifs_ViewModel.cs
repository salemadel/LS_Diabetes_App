using LS_Diabetes_App.Api;
using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using Org.BouncyCastle.Crypto.Prng;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Profil_ViewModels
{
    public class Objectifs_ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IDataStore DataStore { get; set; }
        private INavigation Navigation { get; set; }
        public Settings_Model Profil { get; set; }
        public Objectif_Model Objectifs { get; set; }
        public string Min_Glycemia { get; set; }
        public string Max_Glycemia { get; set; }
        private GlycemiaConverter GlycemiaConverter { get; set; }
        private WeightConverter WeightConverter { get; set; }
        public Command SaveCommand { get; set; }

        public Objectifs_ViewModel(IDataStore dataStore, INavigation navigation)
        {
            DataStore = dataStore;
            Navigation = navigation;
            Profil = DataStore.GetSettingsAsync().First();
            GlycemiaConverter = new GlycemiaConverter();
            WeightConverter = new WeightConverter();

            Objectifs = DataStore.GetObjectifAsync().First();
            var new_max = GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit);
            var new_min = GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia, Profil.GlycemiaUnit);
            var new_weght = WeightConverter.DoubleWeightConvet(Objectifs.Weight_Objectif, Profil.WeightUnit);
            Max_Glycemia = new_max.ToString();
            Min_Glycemia = new_min.ToString();
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
                if (await DependencyService.Get<IDialog>().AlertAsync("", Resources["EditMessage"], Resources["Yes"], Resources["No"]))
                {
                    var restapi = new RestApi();
                    Objectifs.Max_Glycemia = GlycemiaConverter.DoubleGlycemiaConvertBack(double.Parse(Max_Glycemia), Profil.GlycemiaUnit);
                    Objectifs.Min_Glycemia = GlycemiaConverter.DoubleGlycemiaConvertBack(double.Parse(Min_Glycemia), Profil.GlycemiaUnit);
                    Objectifs.Weight_Objectif = WeightConverter.DoubleWeightConvetBack(Objectifs.Weight_Objectif, Profil.WeightUnit);
                    var result = await restapi.Post_Objectifs("glucose", Objectifs.Max_Glycemia, Objectifs.Min_Glycemia);
                    var result2 = await restapi.Post_Objectifs("weight", Objectifs.Weight_Objectif, Objectifs.Weight_Objectif);
                    var result3 = await restapi.Post_Objectifs("walk", Objectifs.Steps_Objectif, Objectifs.Steps_Objectif);
                    if(!(result.Item1 & result2.Item1 & result3.Item1))
                    {
                        DependencyService.Get<IMessage>().ShortAlert(result.Item2);
                        return;
                    }
                    DataStore.UpdateObjectif(Objectifs);
                    MessagingCenter.Send(this, "DataUpdated");
                    DependencyService.Get<ISnackBar>().Show(Resources["SuccesMessage"]);
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Invalide Data !");
            }
        }
    }
}