using Microsoft.Toolkit.Uwp.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ConexionMysql
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NuevaConexion : Page
    {
        public static UserConexion Conexion;
        public NuevaConexion()
        {
            this.InitializeComponent();
           
        }
      
        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            MainPage.appWindow.CloseAsync();
        }
    
        public  void  connect()
        {
            try
            {
                string M_str_sqlcon = $"server={host.Text};user id={user.Text};password={clave.Text};database={database.Text}";
                MySqlConnection mysqlcon = new MySqlConnection(M_str_sqlcon);
                MySqlCommand mysqlcom = new MySqlCommand("select 1", mysqlcon);
                mysqlcon.Open();
                MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
                string contenido = "Conexion fallo";
                if (mysqlread.Read())
                {

                    contenido = "Conexion Exitosa";
                    mysqlcon.Close();
                }
                message.Text = contenido;
                test.IsEnabled = false;

            }
            catch (Exception e)
            {

                message.Text = e.Message;
                throw e;
                //MostrarMensaje(titulo, e.Message);
            }
        }
        private void agregar_Click(object sender, RoutedEventArgs e)
        {
            Conexion = new UserConexion()
            {
                Host = host.Text,
                Color = color.Color.ToString(),
                DataBase = database.Text,
                Password = clave.Text,
                Name = nombre.Text,
                Port = port.Text,
                User = user.Text

            };
           
            
           

            MainPage.appWindow.CloseAsync();
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            test.IsEnabled = false;
            connect();

        }
    }
}
