using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views;
using Plugin.Media;
using Plugin.Permissions.Abstractions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.AddData_ViewModels
{
    internal class AddData_ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IDataStore dataStore;
        private TimeSpan time { get; set; } = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        public TimeSpan Time
        {
            get { return time; }
            set
            {
                if (time != value)
                    time = value;
                OnPropertyChanged();
                OnPropertyChanged("AddDateTime");
            }
        }

        private DateTime date { get; set; } = DateTime.Now;

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                    date = value;
                OnPropertyChanged();
                OnPropertyChanged("AddDateTime");
            }
        }

        public DateTime AddDateTime
        {
            get
            {
                DateTime dt = new DateTime(Date.Year, Date.Month, Date.Day);
                return dt + Time;
            }
        }

        private Glucose_Model glucose { get; set; }

        public Glucose_Model Glucose
        {
            get { return glucose; }
            set
            {
                if (glucose != value)
                    glucose = value;
                OnPropertyChanged();
            }
        }

        private Pression_Model pression { get; set; }

        public Pression_Model Pression
        {
            get { return pression; }
            set
            {
                if (pression != value)
                    pression = value;
                OnPropertyChanged();
            }
        }

        private Weight_Model weight { get; set; }

        public Weight_Model Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                    weight = value;
                OnPropertyChanged();
            }
        }

        private Drugs_Model drug { get; set; }

        public Drugs_Model Drug
        {
            get { return drug; }
            set
            {
                if (drug != value)
                    drug = value;
                OnPropertyChanged();
            }
        }

        private Hb1Ac_Model hb1ac { get; set; }

        public Hb1Ac_Model Hb1Ac
        {
            get { return hb1ac; }
            set
            {
                if (hb1ac != value)
                    hb1ac = value;
                OnPropertyChanged();
            }
        }

        private Insulune_Model insuline { get; set; }

        public Insulune_Model Insuline
        {
            get { return insuline; }
            set
            {
                if (insuline != value)
                    insuline = value;
                OnPropertyChanged();
            }
        }

        private bool isbusy { get; set; }

        public bool IsBusy
        {
            get { return isbusy; }
            set
            {
                if (isbusy != value)
                    isbusy = value;
                OnPropertyChanged();
            }
        }

        public bool Befor_Eat { get; set; }
        public bool After_Eat { get; set; }
        public bool No_Eat { get; set; } = true;
        public bool At_Home { get; set; } = true;
        public bool IsVisible { get; set; }
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

        private GlycemiaConverter GlycemiaConverter { get; set; }
        private WeightConverter WeightConverter { get; set; }
        public Settings_Model Profil { get; set; }
        private PermissionsRequest PermissionsRequest { get; set; }

        private INavigation Navigation { get; set; }

        public AddData_ViewModel(string source, INavigation navigation, IDataStore _datastore, Settings_Model profil, object data = null)
        {
            this.Navigation = navigation;
            this.dataStore = _datastore;
            if (source == "Add_Data")
            {
                IsVisible = true;
                Glucose = new Glucose_Model();
                Pression = new Pression_Model();
                Hb1Ac = new Hb1Ac_Model();
                Drug = new Drugs_Model();
                Insuline = new Insulune_Model();
                Weight = new Weight_Model();
                GlycemiaConverter = new GlycemiaConverter();
                WeightConverter = new WeightConverter();
                PermissionsRequest = new PermissionsRequest();
            }
            else
            {
                IsVisible = false;
                Glucose = (typeof(Glucose_Model) == data.GetType()) ? data as Glucose_Model : null;
                Pression = (typeof(Pression_Model) == data.GetType()) ? data as Pression_Model : null;
                Hb1Ac = (typeof(Hb1Ac_Model) == data.GetType()) ? data as Hb1Ac_Model : null;
                Weight = (typeof(Weight_Model) == data.GetType()) ? data as Weight_Model : null;
            }
            Profil = profil;

            SaveGlucoseCommand = new Command(async () =>
            {
                await ExecuteOnSaveGlucose();
            });

            SaveHb1AcCommand = new Command(async () =>
            {
                await ExecuteOnSaveHb1ac();
            });

            SavePressionCommand = new Command(async () =>
            {
                await ExecuteOnSavePression();
            });
            SaveWeightCommand = new Command(async () =>
            {
                await ExecuteOnSaveWeight();
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

        public Command SaveGlucoseCommand { get; private set; }
        public Command SavePressionCommand { get; set; }
        public Command SaveWeightCommand { get; set; }
        public Command SaveHb1AcCommand { get; set; }

        public Command GetPositionCommand { get; private set; }
        public Command TakePictureCommand { get; private set; }
        public Command PictureTappedCommand { get; set; }

        private async Task ExecuteOnSaveGlucose()
        {
            //  IsBusy = true;
            if (Glucose.Glycemia == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("Glycemie Non Valide !");
                return;
            }
            Glucose.Date = AddDateTime;
            Glucose.PicturePathe = PicturePath;
            if (Befor_Eat)
            {
                Glucose.Glucose_time = "preparandial";
            }
            if (After_Eat)
            {
                Glucose.Glucose_time = "postparandial";
            }
            if (No_Eat)
            {
                Glucose.Glucose_time = "fasting";
            }
            dataStore.AddGlucose(GlycemiaConverter.ConvertBack(Glucose, Profil.GlycemiaUnit));
          //  System.Diagnostics.Debug.WriteLine(Glucose.Glucose_time);
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            //  IsBusy = false;
        }

        private async Task ExecuteOnSavePression()
        {
            //  IsBusy = true;
            if (Pression.Systolique == 0 | Pression.Diastolique == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("Pression Non Valide !");
                return;
            }
            Pression.Date = AddDateTime;
            Pression.PicturePathe = PicturePath;
            if (At_Home)
            {
                Pression.Where = "home";
            }
            else
            {
                Pression.Where = "clinic";
            }
            dataStore.AddPression(Pression);
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            IsBusy = false;
        }

        private async Task ExecuteOnSaveWeight()
        {
            //  IsBusy = true;
            if (Weight.Weight == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("Poids Non Valide !");
                return;
            }
            Weight.Date = AddDateTime;
            Weight.PicturePathe = PicturePath;
            dataStore.AddWeight(WeightConverter.ConvertBack(Weight, Profil.WeightUnit));
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            IsBusy = false;
        }

        private async Task ExecuteOnSaveHb1ac()
        {
            // IsBusy = true;
            if (Hb1Ac.Hb1Ac == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("Hb1Ac Non Valide !");
                return;
            }
            Hb1Ac.Date = AddDateTime;
            Hb1Ac.PicturePathe = PicturePath;
            dataStore.AddHbaAc(Hb1Ac);
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            IsBusy = false;
        }

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
    }
}