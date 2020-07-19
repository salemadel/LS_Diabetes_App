using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Servies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Profil_ViewModels
{
    public class About_ViewModel : ViewModelBase
    {
        public Command FacebookCommand { get; set; }
        public Command WebSiteCommand { get; set; }
        public Command LinkedinCommand { get; set; }
        public Command FlickrCommand { get; set; }

        public About_ViewModel()
        {
            FacebookCommand = new Command(async () =>
            {
                await ExecuteOnFacebook();
            });
            WebSiteCommand = new Command(async () =>
            {
                await ExecuteOnWebSite();
            });
            LinkedinCommand = new Command(async () =>
            {
                await ExecuteOnLinkedin();

            });
            FlickrCommand = new Command(async () =>
            {
                await ExecuteOnFlickr();
            });
        }

      
        private async Task ExecuteOnFacebook()
        {
             await Browser.OpenAsync(new Uri("https://www.facebook.com/Salem.Pharma") , BrowserLaunchMode.SystemPreferred);
          //  DependencyService.Get<IMessage>().ShortAlert("test");
        }
        private async Task ExecuteOnWebSite()
        {
            await Browser.OpenAsync(new Uri("https://www.labosalem.dz"), BrowserLaunchMode.SystemPreferred);
        }
        private async Task ExecuteOnLinkedin()
        {
            await Browser.OpenAsync(new Uri("https://www.linkedin.com/company/laboratoires-salem/"), BrowserLaunchMode.SystemPreferred);
        }
        private async Task ExecuteOnFlickr()
        {
            await Browser.OpenAsync(new Uri("https://www.instagram.com/laboratoires.salem/"), BrowserLaunchMode.SystemPreferred);
        }
    }
}
