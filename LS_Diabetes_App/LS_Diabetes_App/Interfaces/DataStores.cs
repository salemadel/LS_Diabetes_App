using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.Servies;
using SQLite;
using System.Collections.Generic;

namespace LS_Diabetes_App.Interfaces
{
    public class DataStores : IDataStore
    {
        private SQLiteConnection _connection;

        public DataStores()
        {
            _connection = StaticSqlConnection.GetConnection();
        }

        public void AddDrugs(Drugs_Model data)
        {
            _connection.Insert(data);
        }

        public void AddGlucose(Glucose_Model data)
        {
            _connection.Insert(data);
        }

        public void AddHbaAc(Hb1Ac_Model data)
        {
            _connection.Insert(data);
        }

        public void AddSettings(Settings_Model data)
        {
            _connection.Insert(data);
        }

        public void AddObjectif(Objectif_Model data)
        {
            _connection.Insert(data);
        }

        public void AddPression(Pression_Model data)
        {
            _connection.Insert(data);
        }

        public void AddProfil(Profil_Model data)
        {
            _connection.Insert(data);
        }

        public void AddSteps(Steps_Model data)
        {
            _connection.Insert(data);
        }

        public void AddWeight(Weight_Model data)
        {
            _connection.Insert(data);
        }

        public void DeleteDrugs(Drugs_Model data)
        {
            _connection.Delete(data);
        }

        public void DeleteGlucose(Glucose_Model data)
        {
            _connection.Delete(data);
        }

        public void DeleteHb1Ac(Hb1Ac_Model data)
        {
            _connection.Delete(data);
        }

        public void DeleteSettings(Settings_Model data)
        {
            _connection.Delete(data);
        }

        public void DeleteObjectif(Objectif_Model data)
        {
            _connection.Delete(data);
        }

        public void DeletePression(Pression_Model data)
        {
            _connection.Delete(data);
        }

        public void DeleteProfil(Profil_Model data)
        {
            _connection.DeleteAll<Profil_Model>();
        }

        public void DeleteWeight(Weight_Model data)
        {
            _connection.Delete(data);
        }

        public IEnumerable<Drugs_Model> GetDrugsAsync()
        {
            return _connection.Table<Drugs_Model>();
        }

        public IEnumerable<Glucose_Model> GetGlucosAsync()
        {
            return _connection.Table<Glucose_Model>();
        }

        public IEnumerable<Hb1Ac_Model> GetHb1acAsync()
        {
            return _connection.Table<Hb1Ac_Model>();
        }

        public IEnumerable<Settings_Model> GetSettingsAsync()
        {
            return _connection.Table<Settings_Model>();
        }

        public IEnumerable<Objectif_Model> GetObjectifAsync()
        {
            return _connection.Table<Objectif_Model>();
        }

        public IEnumerable<Pression_Model> GetPressionAsync()
        {
            return _connection.Table<Pression_Model>();
        }

        public IEnumerable<Profil_Model> GetProfilAsync()
        {
            return _connection.Table<Profil_Model>();
        }

        public IEnumerable<Steps_Model> GetStepsAsync()
        {
            return _connection.Table<Steps_Model>();
        }

        public IEnumerable<Weight_Model> GetWeightAsync()
        {
            return _connection.Table<Weight_Model>();
        }

        public void UpdateDrugs(Drugs_Model data)
        {
            _connection.Update(data);
        }

        public void UpdateGlucose(Glucose_Model data)
        {
            _connection.Update(data);
        }

        public void UpdateHb1Ac(Hb1Ac_Model data)
        {
            _connection.Update(data);
        }

        public void UpdateSettings(Settings_Model data)
        {
            _connection.Update(data);
        }

        public void UpdateObjectif(Objectif_Model data)
        {
            _connection.Update(data);
        }

        public void UpdatePression(Pression_Model data)
        {
            _connection.Update(data);
        }

        public void UpdateProfil(Profil_Model data)
        {
            _connection.Update(data);
        }

        public void UpdateSteps(Steps_Model data)
        {
            _connection.Update(data);
        }

        public void UpdateWeight(Weight_Model data)
        {
            _connection.Update(data);
        }
    }
}