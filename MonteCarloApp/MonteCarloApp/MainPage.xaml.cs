using MonteCarloLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonteCarloApp
{
    public partial class MainPage : ContentPage
    {
        List<double> numCollection;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void confirmBtn_Clicked(object sender, EventArgs e)
        {
            numCollection = new List<double>();
            try
            {
                int precision = Int32.Parse(precisionEntry.Text);

                Pi piCounter = new Pi(precision);

                piCounter.Randomize();
                piCounter.Count(numCollection);

                numList.ItemsSource = numCollection;
                resultLabel.TextColor = Color.White;
                resultLabel.Text = numCollection[numCollection.Count - 1].ToString();
            }
            catch (Exception)
            {
                await DisplayAlert("Błąd", "Błędne dane wejściowe", "OK");
            }
        }
    }
}
