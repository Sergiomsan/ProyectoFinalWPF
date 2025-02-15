using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MySqlConnector;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Input; // Asegúrate de tener esta directiva using
//using GalaSoft.MvvmLight.Command; // Agrega esta directiva using para RelayCommand
using System.Windows.Controls; // Agrega esta directiva using
using System.Windows.Media;
using System.Diagnostics;
using System.Net.Security;

namespace ProyectoFinal.VistaModelo
{
    public class VistaModeloCliente : INotifyPropertyChanged
    {

        /*public VistaModeloCliente()
        {
            // Inicialización de las propiedades si es necesario
            Dni = null;  // Ejemplo de valor inicial para el DNI
            Nombre = string.Empty;
            Email = string.Empty;
            Telefono = string.Empty;
        }*/

        private string _dni;
        public string dni
        {
            get { return _dni; }
            set
            {
                _dni = value;
                OnPropertyChanged();
            }
        }

        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string email
        {
            get { return _email; }
            set
            {
                _email= value;
                OnPropertyChanged();
            }
        }

        private string _telefono;
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
