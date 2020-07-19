using LS_Diabetes_App.Api;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.Servies
{
    public class Upload_Service
    {
        private IDataStore DataStore { get; set; }
        private RestApi RestApi { get; set; }
        public Upload_Service()
        {
            DataStore = new DataStores();
            RestApi = new RestApi();
        }

        public async Task Upload()
        {
            var Patien_Id = DataStore.GetProfilAsync().First().Api_Id;
            var Drugs_To_Upload = DataStore.GetDrugsAsync().Where(i => i.Uploaded == false);
            var Glucose_To_Upload = DataStore.GetGlucosAsync().Where(i => i.Uploaded == false);
            var Pressure_To_Upload = DataStore.GetPressionAsync().Where(i => i.Uploaded == false);
            var Hb1Ac_To_Upload = DataStore.GetHb1acAsync().Where(i => i.Uploaded == false);
            var Weight_To_Upload = DataStore.GetWeightAsync().Where(i => i.Uploaded == false);
            if(Drugs_To_Upload.Count() > 0)
            {
                foreach(var drug in Drugs_To_Upload)
                {
                    Drugs_Upload_Model data = new Drugs_Upload_Model();
                    data.StartDate = drug.StartDate;
                    data.Dose = drug.Dose;
                    data.Drug = drug.Drug;
                    data.Duration = drug.Duration;
                    data.Indeterminer = drug.Indeterminer;
                    data.Note = drug.Note;
                    data.Taking_Time = drug.Taking_Time;
                    data.Times = drug.Times;
                    string json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    var Result = await RestApi.Post_Drugs(json , data);
                    if (Result.Item1)
                    {
                        drug.Uploaded = true;
                        DataStore.UpdateDrugs(drug);
                    }

                }
            }
            if(Glucose_To_Upload.Count() > 0)
            {
                foreach(var Glucose in Glucose_To_Upload)
                {
                    UploadData_Model Data = new UploadData_Model();
                  //  Data.patient = Patien_Id;
                    Data.type = "glucose";
                    Data.value = Glucose.Glycemia;
                    Data.date_taken = Glucose.Date;
                    Data.note = Glucose.Note;
                    Data.period = Glucose.Glucose_time;
                    Data.activity = Glucose.Activity;
                    Data.took_medication = Glucose.Taking_Medication;
                    string json = JsonConvert.SerializeObject(Data, Formatting.None,new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                    var Result = await RestApi.Post_Activity(json);
                    if(Result.Item1)
                    {
                        Glucose.Uploaded = true;
                        DataStore.UpdateGlucose(Glucose);
                    }
                }
            }
            if(Pressure_To_Upload.Count() > 0)
            {
                foreach (var Pressure in Pressure_To_Upload)
                {
                    UploadData_Model Data = new UploadData_Model();
                  //  Data.patient = Patien_Id;
                    Data.type = "pressure";
                    Data.systolic = Pressure.Systolique;
                    Data.diastolic = Pressure.Diastolique;
                    Data.date_taken = Pressure.Date;
                    Data.note = Pressure.Note;
                    Data.place_taken = Pressure.Where;
                    Data.heart_frequency = Pressure.Heart_Freaquancy;
                    Data.atrial_fibrilation = Pressure.Atrial_Fibrilation;
                    string json = JsonConvert.SerializeObject(Data, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    System.Diagnostics.Debug.WriteLine(json);
                    var Result = await RestApi.Post_Activity(json);
                    if (Result.Item1)
                    {
                        Pressure.Uploaded = true;
                        DataStore.UpdatePression(Pressure);
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert(Result.Item2);
                    }
                }
            }
            if(Hb1Ac_To_Upload.Count() > 0)
            {
                foreach (var Hb1ac in Hb1Ac_To_Upload)
                {
                    UploadData_Model Data = new UploadData_Model();
                  //  Data.patient = Patien_Id;
                    Data.type = "hba1c";
                    Data.value = Hb1ac.Hb1Ac;
                    Data.laboratory = Hb1ac.Laboratory;
                    Data.date_taken = Hb1ac.Date;
                    Data.note = Hb1ac.Note;
                   
                    string json = JsonConvert.SerializeObject(Data, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    var Result = await RestApi.Post_Activity(json);
                    if (Result.Item1)
                    {
                        Hb1ac.Uploaded = true;
                        DataStore.UpdateHb1Ac(Hb1ac);
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert(Result.Item2);
                    }
                }
            }
            if(Weight_To_Upload.Count() > 0)
            {
                foreach (var Weight in Weight_To_Upload)
                {
                    UploadData_Model Data = new UploadData_Model();
                   // Data.patient = Patien_Id;
                    Data.type = "weight";
                    Data.value = Weight.Weight;
                  
                    Data.date_taken = Weight.Date;
                    Data.note = Weight.Note;

                    string json = JsonConvert.SerializeObject(Data, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    var Result = await RestApi.Post_Activity(json);
                    if (Result.Item1)
                    {
                        Weight.Uploaded = true;
                        DataStore.UpdateWeight(Weight);
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert(Result.Item2);
                    }
                }
            }
        }
    }
}
