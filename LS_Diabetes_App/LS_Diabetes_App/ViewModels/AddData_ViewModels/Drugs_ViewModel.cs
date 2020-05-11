using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.Views.AddData_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.AddData_ViewModels
{
    public class Drugs_ViewModel : INotifyPropertyChanged
    {
        private IDataStore DataStore { get; set; }
        private INavigation Navigation { get; set; }

        private ObservableCollection<Drugs_Model> drugs { get; set; }
        public ObservableCollection<Drugs_Model> Drugs
        {
            get { return drugs; }
            set
            {
                if (drugs != value)
                    drugs = value;
                OnPropertyChanged();
            }
        }
        public Drugs_Model Selected_Item { get; set; }
        private Drugs_Model old_selected { get; set; }

        public Command DeleteCommand { get; set; }
        public Command ItemTappedCommand { get; set; }
        public Command AddDrugCommand { get; set; }
        public Drugs_ViewModel(INavigation navigation , IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
            if(DataStore.GetDrugsAsync().Count() > 0)
            {
                Drugs = new ObservableCollection<Drugs_Model>(DataStore.GetDrugsAsync().OrderBy(i => i.StartDate));
            }
            AddDrugCommand = new Command(async () =>
            {
                await ExecuteOnAddDrug();
            });
            ItemTappedCommand = new Command(() =>
            {
                ExecuteOnItemTapped();
            });
            DeleteCommand = new Command(async () =>
            {
                await ExecuteOnDelete();
            });
            MessagingCenter.Subscribe<AddDrugs_ViewModel>(this, "DataUpdated", (sender) =>
            {
                Drugs = new ObservableCollection<Drugs_Model>(DataStore.GetDrugsAsync().OrderBy(i => i.StartDate));
                Selected_Item = null;
                old_selected = null;
            });
        }

        private void ExecuteOnItemTapped()
        {
            if (Selected_Item == old_selected)
            {
                Selected_Item.IsVisible = !Selected_Item.IsVisible;
                UpdateList(Selected_Item);
            }
            else
            {
                if (old_selected != null)
                {
                    old_selected.IsVisible = false;
                    UpdateList(old_selected);
                }
                Selected_Item.IsVisible = true;
                UpdateList(Selected_Item);
            }
            old_selected = Selected_Item;
        }
        private void UpdateList(Drugs_Model selected_item)
        {
            var index = Drugs.IndexOf(selected_item);
            Drugs.Remove(selected_item);
            Drugs.Insert(index, selected_item);
        }

        private async Task ExecuteOnDelete()
        {
            if (Selected_Item != null)
            {
                if (await DependencyService.Get<IDialog>().AlertAsync("", "Voulez Vous Supprimer ?", "Oui", "Non"))
                {
                    DataStore.DeleteDrugs(Selected_Item);
                    Drugs.Remove(Selected_Item);
                    Selected_Item = null;
                    old_selected = null;
                }
            }
        }
            private async Task ExecuteOnAddDrug()
        {
            await Navigation.PushModalAsync(new AddDrugs_View() , true);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
