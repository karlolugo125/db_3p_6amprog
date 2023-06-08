using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace db_3p_6amprog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class registro : ContentPage
	{
		public registro ()
		{
			InitializeComponent ();
		}

     

        private void registros_Clicked(object sender, EventArgs e)
        {
            string connstring = "Server=db4free.net;Port=3306;database=bdautos;User id=siferu;Password=siferu123;charset=utf-8";

            MySqlConnection conn = new MySqlConnection(connstring);
            try
            {
                if (string.IsNullOrWhiteSpace(marca.Text) || string.IsNullOrWhiteSpace(modelo.Text) || string.IsNullOrWhiteSpace(año.Text) || string.IsNullOrWhiteSpace(varios.Text))
                {

                    DisplayAlert("error", "algun dato esta vacio", "cerrar");
                }
                else
                {
                    conn.Open();
                    string sql = "insert into autos(marca,modelo,año,varios) values('" + marca.Text + "','" + modelo.Text + "','" + año.Text + "','" + varios.Text + "')";

                    MySqlCommand command = new MySqlCommand(sql, conn);
                    command.ExecuteReaderAsync();


                    DisplayAlert("exito", "datos guardados correctamente", "cerrar");
                    marca.Text = null;
                    modelo.Text = null;
                    año.Text = null;
                    varios.Text = null;


                }

            }
            catch (MySqlException ex)
            {

                DisplayAlert("algun error", "conexion :" + ex.Message, "cerrar");

            }
            finally { conn.Close(); }
        }
    }
}