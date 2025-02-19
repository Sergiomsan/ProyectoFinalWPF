using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProyectoFinal.VistaModelo
{
    public class VistaModeloCliente : INotifyPropertyChanged
    {

        private string _dni = "";
        public string dni
        {
            get { return _dni; }
            set
            {
                _dni = value;
                OnPropertyChanged();
            }
        }

        private string _nombre = "";
        public string nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }

        private string _email= "";
        public string email
        {
            get { return _email; }
            set
            {
                _email= value;
                OnPropertyChanged();
            }
        }

        private string _telefono= "";
        public string telefono
        {
            get { return _telefono; }
            set
            {
                _telefono = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
