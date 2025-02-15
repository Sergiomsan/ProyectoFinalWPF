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
using ProyectoFinal.VistaModelo;

namespace ProyectoFinal.VIsta
{
    /// <summary>
    /// Lógica de interacción para HabitacionView.xaml
    /// </summary>
    public partial class HabitacionView : Window
    {
        public Window window;
        HabitacionViewModel habitacionViewModel = new HabitacionViewModel();

        Habitacion habitacion = new Habitacion();

        HabitacionCollection obsCategorias = new HabitacionCollection();

        public HabitacionView()
        {
            InitializeComponent();
            DataContext = habitacionViewModel;

            System.Diagnostics.Debug.WriteLine("Inicio Aplicación");
            habitacionViewModel.limpiar();
        }

        public HabitacionView(Window window)
        {
            InitializeComponent();
            DataContext = habitacionViewModel;

            System.Diagnostics.Debug.WriteLine("Inicio Aplicación");
            habitacionViewModel.limpiar();
        }

        private void ejecutarGuardar(object sender, RoutedEventArgs e)
        {
            if (btnInsertar.IsChecked == true)
            {
                try
                {
                    habitacionViewModel.guardarHabitacion();
                    MessageBox.Show("Producto Guardado");
                    habitacionViewModel.limpiar();
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
                    habitacionViewModel.buscarHabitacion();
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
                    habitacionViewModel.actualizarHabitacion();
                    MessageBox.Show("Producto Actualizado");
                    habitacionViewModel.limpiar();
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
                    habitacionViewModel.eliminarHabitacion();
                    MessageBox.Show("Producto Eliminado");
                    habitacionViewModel.limpiar();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
        }

        private void btnInsertar_Checked(object sender, RoutedEventArgs e)
        {
            txtIdHabit.IsReadOnly = false;
            txtNumero.IsReadOnly = false;
            txtEstado.IsReadOnly = false;
            txtTipo.IsReadOnly = false;
            txtPiso.IsReadOnly = false;
            txtPrecio.IsReadOnly = false;
            btnGuardar.Visibility = Visibility.Visible;
            btnGuardar.Content = "Guardar";
            btnLimpiar.Visibility = Visibility.Visible;
        }

        private void btnLeer_Checked(object sender, RoutedEventArgs e)
        {
            txtIdHabit.IsReadOnly = true;
            txtNumero.IsReadOnly = true;
            txtEstado.IsReadOnly = true;
            txtTipo.IsReadOnly = true;
            txtPiso.IsReadOnly = true;
            txtPrecio.IsReadOnly = true;
            btnGuardar.Visibility = Visibility.Hidden;
            btnGuardar.Content = "Leer";
            btnLimpiar.Visibility = Visibility.Hidden;
        }

        private void btnActualizar_Checked(object sender, RoutedEventArgs e)
        {
            txtIdHabit.IsReadOnly = true;
            txtNumero.IsReadOnly = false;
            txtEstado.IsReadOnly = false;
            txtTipo.IsReadOnly = false;
            txtPiso.IsReadOnly = false;
            txtPrecio.IsReadOnly = false;
            btnGuardar.Visibility = Visibility.Visible;
            btnGuardar.Content = "Actualizar";
            btnLimpiar.Visibility = Visibility.Hidden;
        }
        private void btnBorrar_Checked(object sender, RoutedEventArgs e)
        {
            txtIdHabit.IsReadOnly = true;
            txtNumero.IsReadOnly = true;
            txtEstado.IsReadOnly = true;
            txtTipo.IsReadOnly = true;
            txtPiso.IsReadOnly = true;
            txtPrecio.IsReadOnly = true;
            btnGuardar.Visibility = Visibility.Visible;
            btnGuardar.Content = "Borrar";
            btnLimpiar.Visibility = Visibility.Hidden;
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtIdBuscar.Text.ToString());
                Habitacion habitacion = habitacionViewModel.buscarHabitacion(id);

                txtIdHabit.Text = habitacion.id_habitacion.ToString();
                txtPiso.Text = habitacion.piso.ToString();
                txtPrecio.Text = habitacion.precio.ToString();
                txtTipo.Text = habitacion.tipo;
                txtEstado.Text = habitacion.estado;
                txtEstado.Text = habitacion.numero.ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            habitacionViewModel.limpiar();
        }

        private void leerTodos(object sender, RoutedEventArgs e)
        {
            MostrarHabitacionView mostrarHabitacionView = new MostrarHabitacionView(this);
            mostrarHabitacionView.Show();
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
