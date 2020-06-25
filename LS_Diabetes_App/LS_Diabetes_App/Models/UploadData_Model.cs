using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Diabetes_App.Models
{
    public class UploadData_Model
    {
        public string patient { get; set; }
        public string type { get; set; }
        public double? systolic { get; set; }
        public double? diastolic { get; set; }
        public double? heart_frequency { get; set; }
        public bool? atrial_fibrilation { get; set; }
        public double? value { get; set; }
        public string period { get; set; }
        public string place_taken { get; set; }
        public DateTime date_taken { get; set; }
        public bool? activity { get; set; }
        public bool? took_medication { get; set; }
        public string note { get; set; }
        public string laboratory { get; set; }
    }
    public class Objectifs_Upload_Model
    {
        public string type { get; set; }
        public double greater { get; set; }
        public double less { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

    public class Drugs_Upload_Model
    {
        [JsonProperty("created_at")]
        public DateTime Date { get; set; }
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("dose")]
        public double Dose { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("drug")]
        public string Drug { get; set; }
        [JsonProperty("period")]
        public string Taking_Time { get; set; }
        [JsonProperty("times")]
        public string Times { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("indetermine")]
        public bool Indeterminer { get; set; }
    }
}
