using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views;
using LS_Diabetes_App.Views.AddData_Views;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.AddData_ViewModels
{
    public class AddDrugs_ViewModel : INotifyPropertyChanged
    {
        private IDataStore DataStore { get; set; }
        private INavigation Navigation { get; set; }

        private string[] taking_times_list = { "Sans Importance", "En dehors des repas", "Avant les reapas", "Aprés les repas" };
        public List<string> Taking_Times_List
        {
            get { return taking_times_list.ToList(); }
        }
        private Drugs_Model drugs { get; set; }
        public Drugs_Model Drugs
        {
            get { return drugs; }
            set
            {
                if (drugs != value)
                    drugs = value;
                OnPropertyChanged();
            }
        }
        private bool indeterminer { get; set; }
        public bool Indeterminer
        {
            get { return indeterminer; }
            set
            {
                if (indeterminer != value)
                    indeterminer = value;
                OnPropertyChanged();
            }
        }
       private ObservableCollection<string> times_list { get; set; }
        public ObservableCollection<string> Times_List
        {
            get { return times_list; }
            set
            {
                if (times_list != value)
                    times_list = value;
                OnPropertyChanged();
            }
        }

        public Command SaveCommand { get; set; }
        public Command TakePictureCommand { get; set; }
        public AddDrugs_ViewModel(INavigation navigation , IDataStore dataStore)
        {
            DataStore = dataStore;
            Navigation = navigation;
            Drugs = new Drugs_Model();
            Times_List = new ObservableCollection<string>();
            PermissionsRequest = new PermissionsRequest();
            MessagingCenter.Subscribe<AddDrugs_View , string>(this, "AddDrugs", (sender , args) =>
            {
              Times_List.Add(args);
             });
            SaveCommand = new Command(async () =>
            {
                await ExecuteOnSave();
            });
            TakePictureCommand = new Command(async () =>
            {
                await ExecuteOnTakePicture();
            });
            PictureTappedCommand = new Command(async () =>
            {
                await ExecuteOnPictureTapped();
            });
        }
        public Command PictureTappedCommand { get; set; }
        private async Task ExecuteOnSave()
        {
            if (!string.IsNullOrEmpty(Drugs.Drug) & !string.IsNullOrEmpty(Drugs.Taking_Time) & Times_List.Count > 0 & Drugs.Dose > 0)
            {

                if (await DependencyService.Get<IDialog>().AlertAsync("", "Voulez Vous Enregistrer ?", "Oui", "Non"))
                {
                    Drugs.Times_List = Times_List.ToList();
                    DataStore.AddDrugs(Drugs);
                    MessagingCenter.Send(this, "DataUpdated");
                    RappelService.SetRappel();
                    await Navigation.PopModalAsync();
                

                }
            }
        }
        private string PicturePath { get; set; }
        private ImageSource picture { get; set; }
        public ImageSource Picture
        {
            get { return picture; }
            set
            {
                if (picture != value)
                    picture = value;
                OnPropertyChanged();
            }
        }
        private PermissionsRequest PermissionsRequest { get; set; }
        private async Task ExecuteOnTakePicture()
        {
            if (await PermissionsRequest.Check_permissions("Storage") == PermissionStatus.Granted)
            {
                try
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        return;
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                        CompressionQuality = 50,
                        Directory = "LS_Diabetes_pictures",
                        Name = "Sh_" + DateTime.Now.ToString() + ".jpg"
                    });

                    if (file == null)
                        return;

                    PicturePath = file.Path;
                    Picture = ImageSource.FromFile(PicturePath);
                }
                catch
                {
                }
            }
        }
        private async Task ExecuteOnPictureTapped()
        {
            if (!string.IsNullOrWhiteSpace(PicturePath))
                await Navigation.PushModalAsync(new Picture_View(Picture), true);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

}
