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
    /// Lógica de interacción para MostrarReservaView.xaml
    /// </summary>
    public partial class MostrarReservaView : Window
    {
        public Window reservaView;

        ReservaViewModel reservaViewModel = new ReservaViewModel();

        public MostrarReservaView()
        {
            InitializeComponent();
            List<Reserva> listReservas = reservaViewModel.cargarReservas();
            listaReservas.ItemsSource = listReservas;
        }

        public MostrarReservaView(Window reservaView)
        {
            InitializeComponent();
            this.reservaView = reservaView;
            List<Reserva> listReservas = reservaViewModel.cargarReservas();
            listaReservas.ItemsSource = listReservas;
        }
        private void volver(object sender, RoutedEventArgs e)
        {
            if (reservaView != null)
            {
                ReservaView insertarReservaView = new ReservaView(reservaView);
                insertarReservaView.Show();
            }
            else
            {
                reservaView.Show();
            }
            this.Close();
        }
    }
}
