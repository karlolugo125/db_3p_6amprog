using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace db_3p_6amprog
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

      

        private async void consultar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new verificador());
        }

        private async void registro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new registro());
        }

       
    }
}
