using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LS_Diabetes_App.Droid.Interfaces;
using LS_Diabetes_App.Interfaces;
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