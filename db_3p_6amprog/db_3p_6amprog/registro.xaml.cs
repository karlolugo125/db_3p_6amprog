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
        public registro()
        {
            InitializeComponent();
        }

        private void registros_Clicked(object sender, EventArgs e)
        {
            string connstring = "Server=db4free.net;Port=3306;database=bdautos;User id=siferu;Password=siferu123;charset=utf-8";

            MySqlConnection conn = new MySqlConnection(connstring);
            try
            {
                if (string.IsNullOrWhiteSpace(marca.Text) || string.IsNullOrWhiteSpace(modelo.Text) || string.IsNullOrWhiteSpace(año.Text))
                {
                    DisplayAlert("Error", "Algun dato está vacío", "Cerrar");
                }
                else
                {
                    conn.Open();
                    string autoQuery = "INSERT INTO autos (marca, modelo, año) VALUES (@marca, @modelo, @año)";
                    using (MySqlCommand autoCommand = new MySqlCommand(autoQuery, conn))
                    {
                        autoCommand.Parameters.AddWithValue("@marca", marca.Text);
                        autoCommand.Parameters.AddWithValue("@modelo", modelo.Text);
                        autoCommand.Parameters.AddWithValue("@año", año.Text);
                        autoCommand.ExecuteNonQuery();

                        // Obtener el último ID insertado en la tabla "autos"
                        string lastInsertIdQuery = "SELECT LAST_INSERT_ID()";
                        using (MySqlCommand lastInsertIdCommand = new MySqlCommand(lastInsertIdQuery, conn))
                        {
                            int autoId = Convert.ToInt32(lastInsertIdCommand.ExecuteScalar());

                            // Insertar la relación en la tabla "auto_servicio"
                            string servicioQuery = "INSERT INTO auto_servicio (id_servicio, id_auto) VALUES (@idServicio, @idAuto)";
                            using (MySqlCommand servicioCommand = new MySqlCommand(servicioQuery, conn))
                            {
                                // Obtener el texto ingresado en el campo "servicio"
                                string servicioText = servicio.Text;

                                // Realizar una consulta para obtener el ID del servicio
                                string selectQuery = "SELECT id_servicio FROM servicios WHERE servicio = @servicioText";
                                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, conn))
                                {
                                    selectCommand.Parameters.AddWithValue("@servicioText", servicioText);
                                    object result = selectCommand.ExecuteScalar();
                                    if (result != null) // Verificar si se encontró un ID de servicio
                                    {
                                        int idServicio = Convert.ToInt32(result);
                                        servicioCommand.Parameters.AddWithValue("@idServicio", idServicio);
                                        servicioCommand.Parameters.AddWithValue("@idAuto", autoId);
                                        servicioCommand.ExecuteNonQuery();

                                        DisplayAlert("Éxito", "Datos guardados correctamente", "Cerrar");
                                        marca.Text = null;
                                        modelo.Text = null;
                                        año.Text = null;
                                        servicio.Text = null;
                                    }
                                    else
                                    {
                                        DisplayAlert("Error", "El servicio ingresado no existe", "Cerrar");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                DisplayAlert("Error", "Conexión: " + ex.Message, "Cerrar");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
