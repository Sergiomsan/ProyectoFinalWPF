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
    /// Lógica de interacción para ClienteView.xaml
    /// </summary>
    public partial class ClienteView : Window
    {
        private Window clienteView;

        ClienteViewModel clienteViewModel = new ClienteViewModel();

        Cliente categoria = new Cliente();

        ClienteCollection obsCategorias = new ClienteCollection();

        public ClienteView()
        {
            InitializeComponent(); 
            DataContext = clienteViewModel;
            System.Diagnostics.Debug.WriteLine("Inicio Aplicación");
        }

        public ClienteView(Window clienteView)
        {
            InitializeComponent();
            this.clienteView = clienteView;
            DataContext = clienteViewModel;
            System.Diagnostics.Debug.WriteLine("Inicio Aplicación");
        }

        private void ejecutarGuardar(object sender, RoutedEventArgs e)
        {
            if (btnInsertar.IsChecked == true)
            {
                try
                {
                    clienteViewModel.guardarCliente();
                    MessageBox.Show("Producto Guardado");
                    clienteViewModel.limpiar();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            } else if (btnLeer.IsChecked == true)
            {
                try
                {
                   clienteViewModel.buscarCliente();

                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            } else if (btnActualizar.IsChecked == true)
            {
                try
                {
                    clienteViewModel.actualizarCliente();
                    MessageBox.Show("Producto Actualizado");
                    clienteViewModel.limpiar();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            } else if (btnBorrar.IsChecked == true)
            {
                try
                {
                    clienteViewModel.eliminarCliente();
                    MessageBox.Show("Producto Eliminado");
                    clienteViewModel.limpiar();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
        }

        private void btnInsertar_Checked(object sender, RoutedEventArgs e)
        {
            txtDni.IsReadOnly = false;
            txtNombre.IsReadOnly = false;
            txtCorreo.IsReadOnly = false;
            txtTelefono.IsReadOnly = false;
            btnGuardar.Visibility = Visibility.Visible;
            btnGuardar.Content = "Guardar";
            btnLimpiar.Visibility = Visibility.Visible;
        }

        private void btnLeer_Checked(object sender, RoutedEventArgs e)
        {
            txtDni.IsReadOnly = true;
            txtNombre.IsReadOnly = true;
            txtCorreo.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;
            btnGuardar.Visibility = Visibility.Hidden;
            btnLimpiar.Visibility = Visibility.Hidden;

        }

        private void btnActualizar_Checked(object sender, RoutedEventArgs e)
        {
            txtDni.IsReadOnly = true;
            txtNombre.IsReadOnly = false;
            txtCorreo.IsReadOnly = false;
            txtTelefono.IsReadOnly = false;
            btnGuardar.Visibility = Visibility.Visible;
            btnGuardar.Content = "Actualizar";
            btnLimpiar.Visibility = Visibility.Hidden;
        }

        private void btnBorrar_Checked(object sender, RoutedEventArgs e)
        {
            txtDni.IsReadOnly = true;
            txtNombre.IsReadOnly = true;
            txtCorreo.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;
            btnGuardar.Visibility = Visibility.Visible;
            btnGuardar.Content = "Borrar";
            btnLimpiar.Visibility = Visibility.Hidden;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String dni = txtDniBuscar.Text;
                Cliente cliente = clienteViewModel.buscarCliente(dni);

                txtDni.Text = cliente.dni;
                txtCorreo.Text = cliente.email;
                txtNombre.Text = cliente.nombre;
                txtTelefono.Text = cliente.telefono;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            clienteViewModel.limpiar();
        }

        private void leerTodos(object sender, RoutedEventArgs e)
        {
            MostrarClienteView mostrarClienteView = new MostrarClienteView(this);
            mostrarClienteView.Show();
            this.Hide();
        }

        private void volverAlInicio(object sender, RoutedEventArgs e)
        {
            if (clienteView == null)
            {
                MainWindow ventana = new MainWindow(clienteView);
                ventana.Show();
            } else
            {
                clienteView.Show();
            }

            this.Close();
        }
    }
}
