using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LS_Diabetes_App.Interfaces
{
    public interface IDataStore
    {
         IEnumerable<Glucose_Model> GetGlucosAsync();

         Data_Model GetData(int id);
        void AddData(Data_Model data);
        void AddGlucose(Glucose_Model data);
        void UpdateData(Data_Model data);
        void DeleteData(Data_Model data);
    }
}
