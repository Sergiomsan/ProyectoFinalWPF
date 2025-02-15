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
    /// Lógica de interacción para MostrarHabitacionView.xaml
    /// </summary>
    public partial class MostrarHabitacionView : Window
    {
        public Window habitacionView;

        HabitacionViewModel habitacionViewModel = new HabitacionViewModel();

        public MostrarHabitacionView()
        {
            InitializeComponent();
            List<Habitacion> listHabitaciones = habitacionViewModel.cargarHabitaciones();
            listaHabitaciones.ItemsSource = listHabitaciones;
        }

        public MostrarHabitacionView(Window habitacionView)
        {
            InitializeComponent();
            this.habitacionView = habitacionView;
            List<Habitacion> listHabitaciones = habitacionViewModel.cargarHabitaciones();
            listaHabitaciones.ItemsSource = listHabitaciones;
        }

        private void volver(object sender, RoutedEventArgs e)
        {
            if (habitacionView != null)
            {
                HabitacionView insertarHabitacionView = new HabitacionView(habitacionView);
                insertarHabitacionView.Show();
            }
            else
            {
                habitacionView.Show();
            }
            this.Close();
        }
    }
}
