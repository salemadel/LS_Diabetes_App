using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.Views;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.AddData_ViewModels
{
    class AddData_ViewModel : INotifyPropertyChanged
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
        private GlycemiaConverter GlycemiaConverter { get; set; }
        private WeightConverter WeightConverter { get; set; }
        public Profil_Model Profil { get; set; }
        private INavigation Navigation { get; set; }

        public AddData_ViewModel(INavigation navigation, IDataStore _datastore , Profil_Model profil)
        {
            this.Navigation = navigation;
            this.dataStore = _datastore;
            Glucose = new Glucose_Model();
            Pression = new Pression_Model();
            Hb1Ac = new Hb1Ac_Model();
            Drug = new Drugs_Model();
            Insuline = new Insulune_Model();
            Weight = new Weight_Model();
            GlycemiaConverter = new GlycemiaConverter();
            WeightConverter = new WeightConverter();
            Profil = profil;
            
            SaveGlucoseCommand = new Command(async () =>
            {
                await ExecuteOnSaveGlucose();
            });
            SaveDrugsCommand = new Command(async () =>
            {
                await ExecuteOnSaveDrugs();
            });
            SaveHb1AcCommand = new Command(async () =>
            {
                await ExecuteOnSaveHb1ac();
            });
            SaveInsulineCommand = new Command(async () =>
            {
                await ExecuteOnSaveInsuline();
            });
            SavePressionCommand = new Command(async () =>
            {
                await ExecuteOnSavePression();
            });
            SaveWeightCommand = new Command(async () =>
            {
                await ExecuteOnSaveWeight();
            });
            GetPositionCommand = new Command(async () =>
            {
                await ExecuteOnGetPosition();
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public Command SaveGlucoseCommand { get; private set; }
        public Command SavePressionCommand { get; set; }
        public Command SaveWeightCommand { get; set; }
        public Command SaveHb1AcCommand { get; set; }
        public Command SaveDrugsCommand { get; set; }
        public Command SaveInsulineCommand { get; set; }
        public Command GetPositionCommand { get; private set; }
        public Command TakePictureCommand { get; private set; }
        public Command PictureTappedCommand { get; set; }

        private async Task ExecuteOnSaveGlucose()
        {
            IsBusy = true;
            Glucose.Date = AddDateTime;
            dataStore.AddGlucose(GlycemiaConverter.ConvertBack(Glucose , Profil.GlycemiaUnit));
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            IsBusy = false;
        }
        private async Task ExecuteOnSavePression()
        {
            IsBusy = true;
            Pression.Date = AddDateTime;
            dataStore.AddPression(Pression);
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            IsBusy = false;
        }
        private async Task ExecuteOnSaveWeight()
        {
            IsBusy = true;
            Weight.Date = AddDateTime;
            dataStore.AddWeight(WeightConverter.ConvertBack(Weight , Profil.WeightUnit));
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            IsBusy = false;
        }
        private async Task ExecuteOnSaveHb1ac()
        {
            IsBusy = true;
            Hb1Ac.Date = AddDateTime;
            dataStore.AddHbaAc(Hb1Ac);
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            IsBusy = false;
        }
        private async Task ExecuteOnSaveDrugs()
        {
            IsBusy = true;
            Drug.Date = AddDateTime;
            dataStore.AddDrugs(Drug);
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            IsBusy = false;
        }
        private async Task ExecuteOnSaveInsuline()
        {
            IsBusy = true;
            Insuline.Date = AddDateTime;
            dataStore.AddInsuline(Insuline);
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            IsBusy = false;
        }

        private async Task ExecuteOnTakePicture()
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
                    Name = "Ls_" + DateTime.Now.ToString() + ".jpg"
                });

                if (file == null)
                    return;

                Glucose.PicturePathe = file.Path;

            }
            catch
            {
            }
        }
        private async Task ExecuteOnGetPosition()
        {
            IsBusy = true;
            Position position = null;
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);

                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }
                if (status == PermissionStatus.Granted)
                {
                    var locator = CrossGeolocator.Current;

                    locator.DesiredAccuracy = 100;

                    if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
                    {
                        position = await locator.GetPositionAsync(TimeSpan.FromSeconds(30), null, true);
                    }
                    else
                    {
                        DependencyService.Get<ISettingService>().OpenSetting();
                    }
                }
            }
            catch
            {

            }
            finally
            {
                if (position != null)
                {

                    List<double> _position = new List<double>();
                    _position.Add(position.Latitude);
                    _position.Add(position.Longitude);
                    Glucose.Position = _position.ToArray();
                }
            }
            IsBusy = false;
        }

        private async Task ExecuteOnPictureTapped()
        {
            if (!string.IsNullOrWhiteSpace(Glucose.PicturePathe))
                await Navigation.PushModalAsync(new Picture_View(Glucose.Picture), true);
        }
    }
}
