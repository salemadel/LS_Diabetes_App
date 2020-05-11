using Newtonsoft.Json;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LS_Diabetes_App.Models
{
    public class Objectif_Model : INotifyPropertyChanged
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonIgnore]
        [Ignore]
        private double min_glycemia {get;set;}
        public double Min_Glycemia
        {
            get { return min_glycemia; }
            set
            {
                if (min_glycemia != value)
                    min_glycemia = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        [Ignore]
        private double max_glycemia { get; set; }
        public double Max_Glycemia
        {
            get { return max_glycemia; }
            set
            {
                if (max_glycemia != value)
                    max_glycemia = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private double weight_objectif { get; set; }
        
        public double Weight_Objectif
        {
            get { return weight_objectif; }
            set
            {
                if (weight_objectif != value)
                    weight_objectif = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        [Ignore]
        private int steps_objectif { get; set; }
        public int Steps_Objectif
        {
            get { return steps_objectif; }
            set
            {
                if (steps_objectif != value)
                    steps_objectif = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}