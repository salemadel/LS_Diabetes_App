using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Diabetes_App.Models
{
    public class Notifications_Model
    {
        public string doctor { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public bool read { get; set; }

        public string Title
        {
            get { return doctor + " : " + title; }
        }
    }
}
