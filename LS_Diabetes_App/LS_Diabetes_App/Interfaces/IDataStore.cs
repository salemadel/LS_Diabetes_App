using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using System.Collections.Generic;

namespace LS_Diabetes_App.Interfaces
{
    public interface IDataStore
    {
        IEnumerable<Glucose_Model> GetGlucosAsync();
        IEnumerable<Steps_Model> GetStepsAsync();
        IEnumerable<Pression_Model> GetPressionAsync();

        IEnumerable<Weight_Model> GetWeightAsync();

        IEnumerable<Drugs_Model> GetDrugsAsync();

        IEnumerable<Settings_Model> GetSettingsAsync();

        IEnumerable<Hb1Ac_Model> GetHb1acAsync();

        IEnumerable<Profil_Model> GetProfilAsync();
        IEnumerable<Objectif_Model> GetObjectifAsync();

        void AddPression(Pression_Model data);
        void AddObjectif(Objectif_Model data);
        void AddGlucose(Glucose_Model data);
        void AddSteps(Steps_Model data);
        void AddWeight(Weight_Model data);

        void AddDrugs(Drugs_Model data);

        void AddSettings(Settings_Model data);

        void AddHbaAc(Hb1Ac_Model data);

        void AddProfil(Profil_Model data);

        void UpdateGlucose(Glucose_Model data);
        void UpdateObjectif(Objectif_Model data);
        void UpdatePression(Pression_Model data);
        void UpdateSteps(Steps_Model data);
        void UpdateWeight(Weight_Model data);

        void UpdateDrugs(Drugs_Model data);

        void UpdateSettings(Settings_Model data);

        void UpdateHb1Ac(Hb1Ac_Model data);

        void UpdateProfil(Profil_Model data);

        void DeleteGlucose(Glucose_Model data);

        void DeletePression(Pression_Model data);
        void DeleteObjectif(Objectif_Model data);
        void DeleteWeight(Weight_Model data);

        void DeleteDrugs(Drugs_Model data);

        void DeleteSettings(Settings_Model data);

        void DeleteHb1Ac(Hb1Ac_Model data);

        void DeleteProfil(Profil_Model data);

    }
}