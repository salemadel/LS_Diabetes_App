using Android.App;
using LS_Diabetes_App.Droid.Interfaces;
using LS_Diabetes_App.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(DialogShow))]

namespace LS_Diabetes_App.Droid.Interfaces
{
    public class DialogShow : IDialog
    {
        public async Task<bool> AlertAsync(string Title, string Message, string Positif, string Negatif)
        {
            var tcs = new TaskCompletionSource<bool>();

            using (var db = new AlertDialog.Builder(Forms.Context, Resource.Style.MyAlertDialogStyle))
            {
                db.SetTitle(Title);
                db.SetMessage(Message);
                db.SetPositiveButton(Positif, (sender, args) => { tcs.TrySetResult(true); });
                db.SetNegativeButton(Negatif, (sender, args) => { tcs.TrySetResult(false); });
                db.Show();
            }

            return await tcs.Task;
        }
    }
}