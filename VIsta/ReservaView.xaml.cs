using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ProyectoFinal.VistaModelo;

namespace ProyectoFinal.VIsta
{
    public partial class ReservaView : Window
    {
        public Window window;

        ReservaViewModel reservaViewModel = new ReservaViewModel();

        Reserva categoria = new Reserva();

        ReservaCollection obsCategorias = new ReservaCollection();

        public ReservaView()
        {
            InitializeComponent();
            DataContext = reservaViewModel;

            System.Diagnostics.Debug.WriteLine("Inicio Aplicación");


            reservaViewModel.limpiar();
        }
        public ReservaView(Window window)
        {
            InitializeComponent();
            this.window = window;
            DataContext = reservaViewModel;

            System.Diagnostics.Debug.WriteLine("Inicio Aplicación");

            reservaViewModel.limpiar();
        }

        private void ComprobarBuscar(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (txtIdBuscar.Text != "");
        }

        private void EjecutarBuscar(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                reservaViewModel.buscarReserva();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void EjecutarGuardar(object sender, ExecutedRoutedEventArgs e)
        {
            if (btnInsertar.IsChecked == true)
            {
                try
                {
                    reservaViewModel.guardarReserva();
                    MessageBox.Show("Producto Guardado");
                    reservaViewModel.limpiar();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
            else if (btnLeer.IsChecked == true)
            {
                try
                {
                    reservaViewModel.buscarReserva();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
            else if (btnActualizar.IsChecked == true)
            {
                try
                {
                    MessageBox.Show("Producto Actualizado");
                    reservaViewModel.limpiar();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
            else if (btnBorrar.IsChecked == true)
            {
                try
                {
                    MessageBox.Show("Producto Eliminado");
                    reservaViewModel.limpiar();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
        }
        private void EjecutarActualizar(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Producto Actualizado");
                reservaViewModel.limpiar();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }
        private void EjecutarEliminar(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                reservaViewModel.eliminarReserva();
                MessageBox.Show("Producto Eliminado");
                reservaViewModel.limpiar();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void ComprobarLimpiar(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EjecutarLimpiar(object sender, ExecutedRoutedEventArgs e)
        {
            btnLimpiar.Focus();
            reservaViewModel.limpiar();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (btnInsertar.IsChecked == true)
            {
                try
                {
                    reservaViewModel.guardarReserva();
                    MessageBox.Show("Producto Guardado");
                    reservaViewModel.limpiar();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
            else if (btnLeer.IsChecked == true)
            {
                try
                {
                    reservaViewModel.buscarReserva();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
            else if (btnActualizar.IsChecked == true)
            {
                try
                {
                    reservaViewModel.actualizarReserva();
                    MessageBox.Show("Producto Actualizado");
                    reservaViewModel.limpiar();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
            else if (btnBorrar.IsChecked == true)
            {
                try
                {
                    reservaViewModel.eliminarReserva();
                    MessageBox.Show("Producto Eliminado");
                    reservaViewModel.limpiar();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            reservaViewModel.limpiar();
        }

        private void btnBuscar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtIdBuscar.Text.ToString());
                Reserva reserva = reservaViewModel.buscarReserva(id);

                txtIdReserva.Text = reserva.idReserva.ToString();
                txtDni.Text = reserva.dniCliente.ToString();
                txtIdHabitacion.Text = reserva.idHabitacion.ToString();
                txtfechaInicio.Text = reserva.fechaInicio.ToString();
                txtfechaFin.Text = reserva.fechaFin.ToString();
                txtEstado.Text = reserva.estado;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void insertarReserva(object sender, RoutedEventArgs e)
        {
            txtIdReserva.IsReadOnly = false;
            txtDni.IsReadOnly = false;
            txtIdHabitacion.IsReadOnly = false;
            txtfechaInicio.IsReadOnly = false;
            txtEstado.IsReadOnly = false;

            btnGuardar.Visibility = Visibility.Visible;
            btnGuardar.Content = "Guardar";
            btnLimpiar.Visibility = Visibility.Visible;
        }

        private void leerReserva(object sender, RoutedEventArgs e)
        {
            txtIdReserva.IsReadOnly = true;
            txtDni.IsReadOnly = true;
            txtIdHabitacion.IsReadOnly = true;
            txtfechaInicio.IsReadOnly = true;
            txtEstado.IsReadOnly = true;

            btnGuardar.Visibility = Visibility.Hidden;
            btnLimpiar.Visibility = Visibility.Hidden;
        }

        private void actualizarReserva(object sender, RoutedEventArgs e)
        {
            txtIdReserva.IsReadOnly = true;
            txtDni.IsReadOnly = false;
            txtIdHabitacion.IsReadOnly = false;
            txtfechaInicio.IsReadOnly = false;
            txtEstado.IsReadOnly = false;

            btnGuardar.Visibility = Visibility.Visible;
            btnGuardar.Content = "Actualizar";
            btnLimpiar.Visibility = Visibility.Visible;
        }

        private void borrarReserva(object sender, RoutedEventArgs e)
        {
            txtIdReserva.IsReadOnly = true;
            txtDni.IsReadOnly = true;
            txtIdHabitacion.IsReadOnly = true;
            txtfechaInicio.IsReadOnly = true;
            txtEstado.IsReadOnly = true;

            btnGuardar.Visibility = Visibility.Visible;
            btnLimpiar.Visibility = Visibility.Hidden;
        }

        private void leerTodos(object sender, RoutedEventArgs e)
        {
            MostrarReservaView reservaView = new MostrarReservaView(this);
            reservaView.Show();
            this.Hide();
        }

        private void volverAlInicio(object sender, RoutedEventArgs e)
        {
            if (window == null)
            {
                MainWindow ventana = new MainWindow(window);
                ventana.Show();
            }
            else
            {
                window.Show();
            }

            this.Close();
        }
    }
}
