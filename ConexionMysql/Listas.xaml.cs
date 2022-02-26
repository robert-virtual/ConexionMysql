using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Listas : Page
    {
        public  ObservableCollection<Cliente> Clientes
        = new ObservableCollection<Cliente>();


        public Listas()
        {
            this.InitializeComponent();
            var mylist = new ObservableCollection<Cliente>();

            mylist.Add(new Cliente() { name = "Nombre 1" });
            mylist.Add(new Cliente() { name = "Nombre 2" });
            mylist.Add(new Cliente() { name = "Nombre 3" });
            Clientes = mylist;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Clientes.Add(new Cliente() { name = NewName.Text });
            NewName.Text = "";
        }
    }
}
