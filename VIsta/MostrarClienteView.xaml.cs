using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProyectoFinal.Modelo;

namespace ProyectoFinal.VIsta
{
    /// <summary>
    /// Lógica de interacción para MostrarClienteView.xaml
    /// </summary>
    public partial class MostrarClienteView : Window
    {
        public Window clienteView;

        ClienteViewModel clienteViewModel = new ClienteViewModel();
       
        public MostrarClienteView()
        {
            InitializeComponent();
            List<Cliente> listClient = clienteViewModel.cargarClientes();
            listaClientes.ItemsSource = listClient;
        }

        public MostrarClienteView(Window clienteView)
        {
            InitializeComponent();
            this.clienteView = clienteView;
            List<Cliente> listClient = clienteViewModel.cargarClientes();
            listaClientes.ItemsSource = listClient;
        }

        private void volver(object sender, RoutedEventArgs e)
        {
            if (clienteView != null)
            {
                ClienteView insertarClienteView = new ClienteView(clienteView);
                insertarClienteView.Show();
            }
            else
            {
                clienteView.Show();
            }
            this.Close();
        }
    }
}
