using LS_Diabetes_App.Api;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.Login_Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class ForgotPassword_ViewModel : ViewModelBase
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        private RestApi RestApi { get; set; }

        private bool isBusy { get; set; }
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                    isBusy = value;
                OnPropertyChanged();
            }
        }
        private bool iscodevisible { get; set; }
        public bool IsCodeVisible
        {
            get { return iscodevisible; }
            set
            {
                if (iscodevisible != value)
                    iscodevisible = value;
                OnPropertyChanged();
            }
        }
        private bool ispasswordvisible { get; set; }
        public bool IsPasswordVisible
        {
            get { return iscodevisible; }
            set
            {
                if (iscodevisible != value)
                    iscodevisible = value;
                OnPropertyChanged();
            }
        }
        public Command SendEmailCommand { get; set; }
        public Command SendCodeCommand { get; set; }
        public Command ResetPasswodCommand { get; set; }
        public ForgotPassword_ViewModel()
        {
            RestApi = new RestApi();
            SendEmailCommand = new Command(async () =>
            {
                await ExecuteOnSendEmail();
            });
            SendCodeCommand = new Command(async () =>
            {
                await ExecuteOnSendCode();
            });
            ResetPasswodCommand = new Command(async () =>
            {
                await ExecuteOnResetPassword();
            });
        }

        private async Task ExecuteOnSendEmail()
        {
            if(!IsValidEmail(Email))
            {
                DependencyService.Get<IMessage>().ShortAlert("Mail Error !");
                return;
            }
            IsBusy = true;
            IsCodeVisible = false;
            IsPasswordVisible = false;
            await Task.Delay(2000);
            IsCodeVisible = true;
            IsBusy = false;
        }
        private async Task ExecuteOnSendCode()
        {
            IsBusy = true;
            IsPasswordVisible = false;
            await Task.Delay(2000);
            IsPasswordVisible = true;
            IsBusy = false;
        }
        private async Task ExecuteOnResetPassword()
        {
            string checkpassword = CheckPassword();
            if (!string.IsNullOrEmpty(checkpassword))
            {
                DependencyService.Get<IMessage>().ShortAlert(checkpassword);
                return;
            }
            IsBusy = true;
            await Task.Delay(2000);
            App.Current.MainPage = new Login_Page();
            IsBusy = false;
        }
        private string CheckPassword()
        {
            if (!Password.Equals(ConfirmPassword))
            {
                return Resources["ConfirmPasswordMessage"];
            }
            if (Password.Length < 6)
            {
                return Resources["SixLettersMessage"];
            }
            return string.Empty;
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
