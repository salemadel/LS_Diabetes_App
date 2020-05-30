using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels.Statistiques_ViewModels;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Statistiques_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlucoseStatistique_Page : ContentPage
    {
        public GlucoseStatistique_Page()
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new GlucoseStatistique_ViewModel(Navigation, datastore);
            NumericalStripLine stripLine1 = new NumericalStripLine()
            {
                Start = 0,
                Width = (BindingContext as GlucoseStatistique_ViewModel).Min_Glycemia,

                FillColor = Color.FromHex("#50f1c40f")
            };
            NumericalStripLine stripLine2 = new NumericalStripLine()
            {
                Start = (BindingContext as GlucoseStatistique_ViewModel).Min_Glycemia,
                Width = (BindingContext as GlucoseStatistique_ViewModel).Good_Wigh,

                FillColor = Color.FromHex("#502ecc71")
            };
            NumericalStripLine stripLine3 = new NumericalStripLine()
            {
                Start = (BindingContext as GlucoseStatistique_ViewModel).Max_Glycemia,
                Width = (BindingContext as GlucoseStatistique_ViewModel).Max_Widh,

                FillColor = Color.FromHex("#50e74c3c")
            };
            Numerical.StripLines.Add(stripLine1);
            Numerical.StripLines.Add(stripLine2);
            Numerical.StripLines.Add(stripLine3);
        }

        private void Handle_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            MessagingCenter.Send(this, "Filter");
        }
    }
}