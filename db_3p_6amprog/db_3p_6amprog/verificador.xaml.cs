using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace db_3p_6amprog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class verificador : ContentPage
	{
		public verificador ()
		{
			InitializeComponent ();
		}

        

        private void consultar_Clicked(object sender, EventArgs e)
        {
            string connectionString = "Server=db4free.net;Port=3306;database=bdautos;User id=siferu;Password=siferu123;charset=utf-8";
            using (MySqlConnection connection = new MySqlConnection(connectionString))

            {
                connection.Open();


                string query = "SELECT marca,modelo,año,varios from autos WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", cons.Text);


                using (MySqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        string dato1 = reader.GetString("marca");
                        string dato2 = reader.GetString("modelo");
                        int dato3 = reader.GetInt32("año");
                        string dato4 = reader.GetString("varios");

                        mostrar.Text = $"marca: {dato1}\n modelo : {dato2}\n año: {dato3}\n varios: {dato4}";
                    }
                }

                connection.Close();

            }
        }
    }
}