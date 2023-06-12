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
        public verificador()
        {
            InitializeComponent();
        }

        private void consultar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cons.Text))
            {
                string connectionString = "Server=db4free.net;Port=3306;database=bdautos;User id=siferu;Password=siferu123;charset=utf-8";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT autos.modelo, servicios.servicio, servicios.costo " +
                                   "FROM auto_servicio " +
                                   "INNER JOIN autos ON auto_servicio.id_auto = autos.id_auto " +
                                   "INNER JOIN servicios ON auto_servicio.id_servicio = servicios.id_servicio " +
                                   "WHERE autos.id_auto = @id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", cons.Text);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder sb = new StringBuilder();

                        while (reader.Read())
                        {
                            string modelo = reader.GetString("modelo");
                            string servicio = reader.GetString("servicio");
                            int costo = reader.GetInt32("costo");

                            sb.AppendLine($"Modelo: {modelo}");
                            sb.AppendLine($"Servicio: {servicio}");
                            sb.AppendLine($"Costo: {costo}");
                            sb.AppendLine("-----------------------------------");
                        }

                        mostrar.Text = sb.ToString();
                    }

                    connection.Close();
                }
            }
        }

        private void consnoid_Clicked(object sender, EventArgs e)
        {
            string connectionString = "Server=db4free.net;Port=3306;database=bdautos;User id=siferu;Password=siferu123;charset=utf-8";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT autos.modelo, servicios.servicio, servicios.costo " +
                               "FROM auto_servicio " +
                               "INNER JOIN autos ON auto_servicio.id_auto = autos.id_auto " +
                               "INNER JOIN servicios ON auto_servicio.id_servicio = servicios.id_servicio";

                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    StringBuilder sb = new StringBuilder();

                    while (reader.Read())
                    {
                        string modelo = reader.GetString("modelo");
                        string servicio = reader.GetString("servicio");
                        int costo = reader.GetInt32("costo");

                        sb.AppendLine($"Modelo: {modelo}");
                        sb.AppendLine($"Servicio: {servicio}");
                        sb.AppendLine($"Costo: {costo}");
                        sb.AppendLine("-----------------------------------");
                    }

                    mostrar.Text = sb.ToString();
                }

                connection.Close();
            }
        }
    }
}
