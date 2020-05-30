using LS_Diabetes_App.Api;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.Login_Pages;
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
            if (!IsValidEmail(Email))
            {
                DependencyService.Get<IMessage>().ShortAlert("Mail Error !");
                return;
            }
            IsBusy = true;
            IsCodeVisible = false;
            IsPasswordVisible = false;
            var result = await RestApi.EmailForgotCheck(Email);
            if (result.Item1)
            {
                IsCodeVisible = true;
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert(result.Item2);
            }

            IsBusy = false;
        }

        private async Task ExecuteOnSendCode()
        {
            IsBusy = true;
            IsPasswordVisible = false;
            var result = await RestApi.PostCode(Code);
            if (result.Item1)
            {
                IsPasswordVisible = true;
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert(result.Item2);
            }

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
            var result = await RestApi.ForgotPasswordEdit(Password);
            if (result.Item1)
            {
                DependencyService.Get<ISnackBar>().Show(Resources["SuccesMessage"]);
                App.Current.MainPage = new Login_Page();
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert(result.Item2);
            }

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

        private bool IsValidEmail(string email)
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