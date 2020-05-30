using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Diabetes_App.Models.Data_Models
{
    public class Token_Model
    {
        public string id { get; set; }
        public double iat { get; set; }
        public double exp { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string diabete_type { get; set; }

    }
}
