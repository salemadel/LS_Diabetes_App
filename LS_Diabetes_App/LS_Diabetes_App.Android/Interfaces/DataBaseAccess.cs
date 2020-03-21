using LS_Diabetes_App.Droid.Interfaces;
using LS_Diabetes_App.Interfaces;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DataBaseAccess))]

namespace LS_Diabetes_App.Droid.Interfaces
{
    public class DataBaseAccess : IDatabaseAccess
    {
        public string DatabasePath()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LS_Diabets_App_Db");

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            return path;
        }
    }
}