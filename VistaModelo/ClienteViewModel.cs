using ProyectoFinal.Modelo;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoFinal
{
    public class ClienteViewModel : INotifyPropertyChanged
    {
        private string dni = "";
        private string nombre = "";
        private string correo = "";
        private string telefono = "";
        private string dniBuscar = "";

        public string Dni
        {
            get { return dni; }
            set
            {
                dni = value;
                OnPropertyChanged("Id_habitacion");
            }
        }
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged("Nombre");
            }
        }
        public string Correo
        {
            get { return correo; }
            set
            {
                correo = value;
                OnPropertyChanged("Correo");
            }
        }
        public string Telefono
        {
            get { return telefono; }
            set
            {
                telefono = value;
                OnPropertyChanged("Telefono");
            }
        }

        public string DniBuscar
        {
            get { return dniBuscar; }
            set {
                dniBuscar = value;
                OnPropertyChanged("DniBuscar");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private Cliente cargaCliente()
        {
            Cliente Cliente = new Cliente();
            Cliente.dni = dni;
            Cliente.nombre = nombre;
            Cliente.email = correo;
            Cliente.telefono = telefono;

            return Cliente;
        }

        public void buscarCliente()
        {
            Cliente Habitacion = new Cliente();

            try
            {
                Habitacion = Habitacion.buscarCliente(dniBuscar);
                dni = Habitacion.dni;
                nombre = Habitacion.nombre;
                telefono = Habitacion.telefono;
                correo = Habitacion.email;

            }
            catch (Exception e)
            {
                throw new Exception("Consulta Habitacion - " + e.Message);
            }
        }

        public Cliente buscarCliente(string dni)
        {
            Cliente Cliente = new Cliente();

            try
            {
                Cliente = Cliente.buscarCliente(dni);
                dni = Cliente.dni;
                nombre = Cliente.nombre;
                telefono = Cliente.telefono;
                correo = Cliente.email;

            }
            catch (Exception e)
            {
                throw new Exception("Consulta Cliente - " + e.Message);
            }

            return Cliente;
        }

        public void guardarCliente()
        {
            try
            {
                Cliente Cliente = cargaCliente();

                if (!Cliente.guardarCliente(Cliente))
                {
                    throw new Exception("Fallo al Guardar Cliente");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Guardar Cliente - " + e.Message);
            }
        }

        public void actualizarCliente()
        {
            try
            {
                Cliente Cliente = cargaCliente();

                if (!Cliente.actualizarCliente(Cliente))
                {
                    throw new Exception("Fallo al Actualizar Cliente");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Actualizar Cliente - " + e.Message);
            }
        }

        public void eliminarCliente()
        {
            try
            {
                Cliente Cliente = cargaCliente();

                if (!Cliente.eliminarCliente(Cliente))
                {
                    throw new Exception("Fallo al Eliminar Cliente");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Eliminar Cliente - " + e.Message);
            }
        }

        public List<Cliente> cargarClientes()
        {
            ClienteCollection clientesColl = new ClienteCollection();   
            List<Cliente> obsClientes = new List<Cliente>();
            obsClientes = clientesColl.cargarClientes();

            return obsClientes;
        }

        public void limpiar()
        {
            dni = "";
            nombre = "";
            correo = "";
            telefono = "";
        }
    }
}
