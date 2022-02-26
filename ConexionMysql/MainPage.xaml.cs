using Microsoft.Toolkit.Uwp.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ConexionMysql
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ApplicationDataStorageHelper helper;
        private UserConexionSerializer mySerializer = new UserConexionSerializer();
        public ObservableCollection<UserConexion> Conexions  = new ObservableCollection<UserConexion>();

        StandardUICommand deleteCommand;
        StandardUICommand openCommand;
        public MainPage()
        {
            this.InitializeComponent();
            helper = ApplicationDataStorageHelper.GetCurrent(mySerializer);
            deleteCommand = new StandardUICommand(StandardUICommandKind.Delete);
            openCommand = new StandardUICommand(StandardUICommandKind.Delete);
            //
            openCommand.ExecuteRequested += OpenCommand_ExecuteRequested;
            deleteCommand.ExecuteRequested += DeleteCommand_ExecuteRequested;
            verifyConexiones();

        }
        private  void verifyConexiones()
        {
            if (helper.KeyExists("conexiones"))
            {
                //Conexions = ;
                var res  = helper.Read<ObservableCollection<UserConexion>>("conexiones");
                DeleteAll.Visibility = Visibility.Visible;


                Conexions.Clear();
                for (var i = 0; i < res.Count; i++)
                {
                    res[i].Id = i.ToString();
                    res[i].Command = deleteCommand;
                    res[i].open = openCommand;
                    Conexions.Add(res[i]);
                }
                return;
            }
            DeleteAll.Visibility = Visibility.Collapsed;
            Conexions.Clear();
        }
        public void saveConexions()
        {
            var save = new ObservableCollection<UserConexion>();
            foreach (var item in Conexions)
            {
                item.Command = null;
                item.open = null;
                save.Add(item);
            }
            helper.Save("conexiones", save);
            if (Conexions.Count > 0)
            {
                DeleteAll.Visibility = Visibility.Visible;

            }
            
        }
        private void DeleteCommand_ExecuteRequested(
            XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            // If possible, remove specfied item from collection.
            if (args.Parameter != null)
            {
                foreach (var i in Conexions)
                {
                    if (i.Id == (args.Parameter as string))
                    {
                        Conexions.Remove(i);
                        saveConexions();
                        return;
                    }
                }
            }
          
        }

        private void OpenCommand_ExecuteRequested(
            XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            // If possible, remove specfied item from collection.
            var id = args.Parameter as string;
            var item = Conexions.First(e => e.Id == id);

            Frame.Navigate(typeof(DashBoard),item);
    
        }

        public static AppWindow appWindow;
        private async void NewConexion_Click(object sender, RoutedEventArgs e)
        {
            if (appWindow != null)
            {
                return;
            }

            //Frame.Navigate(typeof(NuevaConexion));
             appWindow = await AppWindow.TryCreateAsync();
            Frame appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(typeof(NuevaConexion));
          

            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);
            appWindow.Closed += delegate
            {
                if (NuevaConexion.Conexion != null)
                {

                    Conexions.Add(NuevaConexion.Conexion);
                    saveConexions();    
                    verifyConexiones();

                }
                appWindowContentFrame.Content = null;
                appWindow = null;
            };

            await appWindow.TryShowAsync();


        }

        public async Task<ContentDialogResult> MostrarMensaje(string Title,
                string Content)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = 
                Title,
                Content =
                Content,
                CloseButtonText = "OK"
            };
            return await dialog.ShowAsync();
        }
     

       

        private void DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            helper.TryDelete("conexiones");
            verifyConexiones();
        }

      
    }
}
