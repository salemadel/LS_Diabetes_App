using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.AddData_ViewModels
{
    public class AddDrugs_ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IDataStore DataStore { get; set; }
        private INavigation Navigation { get; set; }

        /*  private string[] taking_times_list = { "Sans Importance", "En dehors des repas", "Avant les reapas", "Aprés les repas" };
          public List<string> Taking_Times_List
          {
              get { return taking_times_list.ToList(); }
          }*/
        public List<string> Taking_Times_List { get; set; }
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
        private int index { get; set; }
        public int Index
        {
            get { return index; }
            set
            {
                if (index != value)
                    index = value;
                OnPropertyChanged();
            }
        }
        public bool IsVisible { get; set; }

        public Command SaveCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command TakePictureCommand { get; set; }
        public List<string> DrugsList { get; set; }
        public AddDrugs_ViewModel(INavigation navigation, IDataStore dataStore, Drugs_Model drug = null)
        {
            DataStore = dataStore;
            Navigation = navigation;
            Taking_Times_List = new List<string>();
            Taking_Times_List.Add(Resources["Unimportant"]);
            Taking_Times_List.Add(Resources["Without food"]);
            Taking_Times_List.Add(Resources["Before_Meal"]);
            Taking_Times_List.Add(Resources["After_Meals"]);
            if (drug != null)
            {
                Drugs = drug;
                Times_List = new ObservableCollection<string>(Drugs.Times_List);
                PicturePath = Drugs.PicturePathe;
                Picture = ImageSource.FromFile(PicturePath);
                IsVisible = true;
            }
            else
            {
                Drugs = new Drugs_Model();
                Times_List = new ObservableCollection<string>();
            }
            string JsonFile = "DrugsJson.json";
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AddDrugs_ViewModel)).Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{JsonFile}");
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                DrugsList = JsonConvert.DeserializeObject<List<DrugsJsonModel>>(json).Select(i =>i.Name).ToList();
            }
            PermissionsRequest = new PermissionsRequest();
            MessagingCenter.Subscribe<AddDrugs_View, string>(this, "AddDrugs", (sender, args) =>
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
            DeleteCommand = new Command(async () =>
            {
                await ExecuteOnDelete();
            });
            EditCommand = new Command(async () =>
            {
                await ExecuteOnEdit();
            });
        }

        public Command PictureTappedCommand { get; set; }

        private async Task ExecuteOnSave()
        {
            if (Drugs.Duration < 1 & !Drugs.Indeterminer)
            {
                DependencyService.Get<IMessage>().ShortAlert("Invalid Duration !");
                return;
            }
            if (!string.IsNullOrEmpty(Drugs.Drug) & index >=0 & Times_List.Count > 0 & Drugs.Dose > 0)
            {
                if (await DependencyService.Get<IDialog>().AlertAsync("", "Voulez Vous Enregistrer ?", "Oui", "Non"))
                {
                    Drugs.Times_List = Times_List.ToList();
                    switch(Index)
                    {
                        case 0:Drugs.Taking_Time = "unimportant"; break;
                        case 1: Drugs.Taking_Time = "without food"; break;
                        case 2: Drugs.Taking_Time = "preparandial"; break;
                        case 3: Drugs.Taking_Time = "postparandial"; break;
                    }
                    Drugs.PicturePathe = PicturePath;
                    DataStore.AddDrugs(Drugs);
                    MessagingCenter.Send(this, "DataUpdated");
                    RappelService.SetRappel();
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Invalide Data !");
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
                        Directory = "SmartHealth_pictures",
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

        private async Task ExecuteOnDelete()
        {
            if (Drugs != null)
            {
                if (Drugs.Duration < 1 & !Drugs.Indeterminer)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Invalid Duration !");
                    return;
                }
                if (!string.IsNullOrEmpty(Drugs.Drug) & !string.IsNullOrEmpty(Drugs.Taking_Time) & Times_List.Count > 0 & Drugs.Dose > 0)
                {
                    if (await DependencyService.Get<IDialog>().AlertAsync("", Resources["DeleteMessage"], "Oui", "Non"))
                    {
                        DataStore.DeleteDrugs(Drugs);
                        MessagingCenter.Send(this, "DataUpdated");
                        RappelService.SetRappel();
                        await Navigation.PopModalAsync();
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Invalid Data !");
                }
            }
        }

        private async Task ExecuteOnEdit()
        {
            if (Drugs != null)
            {
                if (Drugs.Duration < 1 & !Drugs.Indeterminer)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Invalid Duration !");
                    return;
                }
                if (!string.IsNullOrEmpty(Drugs.Drug) & !string.IsNullOrEmpty(Drugs.Taking_Time) & Times_List.Count > 0 & Drugs.Dose > 0)
                {
                    if (await DependencyService.Get<IDialog>().AlertAsync("", Resources["EditMessage"], Resources["Yes"], Resources["No"]))
                    {
                        Drugs.Times_List = Times_List.ToList();
                        Drugs.PicturePathe = PicturePath;
                        DataStore.UpdateDrugs(Drugs);

                        MessagingCenter.Send(this, "DataUpdated");
                        RappelService.SetRappel();
                        await Navigation.PopModalAsync();
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Invalid Data !");
                }
            }
        }
    }
}