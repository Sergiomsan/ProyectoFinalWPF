using System.Windows;
using ProyectoFinal.VIsta;

namespace ProyectoFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Window window;
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Window window)
        {
            InitializeComponent();
            this.window = window;
        }

        private void botonHabitacion(object sender, RoutedEventArgs e)
        {
            HabitacionView window = new HabitacionView(this);
            window.Show();
            this.Hide();
        }

        private void botonCliente(object sender, RoutedEventArgs e)
        {
            ClienteView window = new ClienteView(this);
            window.Show();
            this.Hide();
        }

        private void botonReserva(object sender, RoutedEventArgs e)
        {
            ReservaView window = new ReservaView(this);
            window.Show();
            this.Hide();
        }

        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            Leer.Leer window = new Leer.Leer();
            window.Show();
            his.Close();
        }*/
    }
}