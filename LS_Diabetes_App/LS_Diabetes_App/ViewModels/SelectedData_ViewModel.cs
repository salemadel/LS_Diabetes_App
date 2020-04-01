using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
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
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class SelectedData_ViewModel : INotifyPropertyChanged
    {
        private Data_Model data { get; set; }

        public Data_Model Data
        {
            get { return data; }
            set
            {
                if (data != value)
                    data = value;
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

        private bool updating { get; set; }

        public bool Updating
        {
            get { return updating; }
            set
            {
                if (updating != value)
                    updating = value;
                OnPropertyChanged();
            }
        }

        private bool delting { get; set; }

        public bool Delting
        {
            get { return delting; }
            set
            {
                if (delting != value)
                    delting = value;
                OnPropertyChanged();
            }
        }

        private bool isVisible { get; set; } = true;

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible != value)
                    isVisible = value;
                OnPropertyChanged();
            }
        }

        private INavigation Navigation { get; set; }
        private IDataStore dataStore;

        public SelectedData_ViewModel(Data_Model _data, IDataStore _datastore, INavigation _navigation)
        {
            this.Data = _data;
            this.dataStore = _datastore;
            this.Navigation = _navigation;
            UpdateCommand = new Command(async () =>
           {
               await ExecuteOnUpdateData();
           });
            DeleteCommand = new Command(async () =>
           {
               await ExecuteOnDeleteData();
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
            UpdateButtonTappedCommand = new Command(() =>
            {
                ExecuteOnUpdateButtonTapped();
            });
            CancelButtonTappedCommand = new Command(() =>
            {
                ExecuteOnCancelButtonTapped();
            });
            DeleteButtonTappedCommand = new Command(() =>
            {
                ExecuteOnDeleteButtonTapped();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Command UpdateCommand { get; private set; }
        public Command DeleteCommand { get; private set; }
        public Command GetPositionCommand { get; private set; }
        public Command TakePictureCommand { get; private set; }
        public Command PictureTappedCommand { get; set; }
        public Command UpdateButtonTappedCommand { get; set; }
        public Command CancelButtonTappedCommand { get; set; }
        public Command DeleteButtonTappedCommand { get; set; }

        private async Task ExecuteOnUpdateData()
        {
            IsBusy = true;
            // dataStore.UpdateData(Data);
            MessagingCenter.Send(this, "DataUpdated");
            await Navigation.PopModalAsync();
            IsBusy = false;
        }

        private async Task ExecuteOnDeleteData()
        {
            IsBusy = true;
            //  dataStore.DeleteData(Data);
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

                Data.PicturePathe = file.Path;
            }
            catch
            {
            }
        }

        private async Task ExecuteOnGetPosition()
        {
            if (Updating)
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
                        Data.Position = _position.ToArray();
                    }
                }
                IsBusy = false;
            }
        }

        private async Task ExecuteOnPictureTapped()
        {
            if (!string.IsNullOrWhiteSpace(Data.PicturePathe))
                await Navigation.PushModalAsync(new Picture_View(Data.Picture), true);
        }

        private void ExecuteOnUpdateButtonTapped()
        {
            IsVisible = false;
            Updating = true;
        }

        private void ExecuteOnCancelButtonTapped()
        {
            IsVisible = true;
            Updating = false;
            Delting = false;
        }

        private void ExecuteOnDeleteButtonTapped()
        {
            IsVisible = false;
            Delting = true;
        }
    }
}