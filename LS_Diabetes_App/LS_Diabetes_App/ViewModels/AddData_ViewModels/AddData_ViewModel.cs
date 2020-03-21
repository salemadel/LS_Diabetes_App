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
        private INavigation Navigation { get; set; }

        public AddData_ViewModel(INavigation navigation, IDataStore _datastore)
        {
            this.Navigation = navigation;
            this.dataStore = _datastore;
            Glucose = new Glucose_Model();
            SaveGlucoseCommand = new Command(async () =>
            {
                await ExecuteOnSaveGlucose();
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
        public Command GetPositionCommand { get; private set; }
        public Command TakePictureCommand { get; private set; }
        public Command PictureTappedCommand { get; set; }

        private async Task ExecuteOnSaveGlucose()
        {
            IsBusy = true;
            dataStore.AddGlucose(Glucose);
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
