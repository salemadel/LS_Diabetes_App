using LS_Diabetes_App.Servies;
using Newtonsoft.Json;
using SQLite;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LS_Diabetes_App.Models
{
    public class Folowers_Model : ViewModelBase
    {
        public Doctor doctor { get; set; }
        public bool accepted { get; set; }

        public string AcceptedSort
        {
            get
            {
                if(accepted)
                {
                    return Resources["Accepted"];
                }
                else
                {
                    return Resources["Not_Accepted"];
                }
            }
        }
        public Folowers_Model()
        {
            doctor = new Doctor();
        }
    }

    public class Doctor
    {
        public string _id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string speciality { get; set; }
        public string FullName
        {
            get { return lastname.First().ToString().ToUpper() + lastname.Substring(1) + " " + firstname.First().ToString().ToUpper() + firstname.Substring(1); }
        }
    }
}