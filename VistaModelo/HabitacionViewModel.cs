using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinal.Modelo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoFinal.VistaModelo
{
    class HabitacionViewModel : INotifyPropertyChanged
    {
        private int id_habitacion = 0;
        private int numero = 0;
        private string tipo = "";
        private double precio = 0.0;
        private string estado = "";
        private int piso = 0;
        private int idBuscar = 0;

        public int Id_habitacion
        {
            get { return id_habitacion; }
            set { id_habitacion = value; }
        }


        public int Numero
        {
            get { return numero; }
            set { numero = value;
                OnPropertyChanged("Numero");
            }
        }


        public string? Tipo
        {
            get { return tipo; }
            set { tipo = value;
                OnPropertyChanged("Tipo");
            }
        }


        public double Precio
        {
            get { return precio; }
            set { precio = value;
                OnPropertyChanged("Precio");
            }
        }


        public string? Estado
        {
            get { return estado; }
            set { estado = value; }
        }


        public int Piso
        {
            get { return piso; }
            set { piso = value; }
        }

        public int IdBuscar
        {
            get { return idBuscar; }
            set
            {
                idBuscar = value;
                OnPropertyChanged("id_habitacionBuscar");
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

        private Habitacion cargarHabitacion()
        {
            Habitacion Habitacion = new Habitacion();
            Habitacion.id_habitacion = id_habitacion;
            Habitacion.numero = numero;
            Habitacion.tipo = tipo;
            Habitacion.precio = precio;
            Habitacion.estado = estado;
            Habitacion.piso = piso;

            return Habitacion;
        }

        public void buscarHabitacion()
        {
            Habitacion Habitacion = new Habitacion();

            try
            {
                Habitacion = Habitacion.buscarHabitacion(idBuscar);
                id_habitacion = Habitacion.id_habitacion;
                numero = Habitacion.numero;
                tipo = Habitacion.tipo;
                precio = Habitacion.precio;
                estado = Habitacion.estado;
                piso = Habitacion.piso;

            }
            catch (Exception e)
            {
                throw new Exception("Consulta Habitacion - " + e.Message);
            }
        }

        public Habitacion buscarHabitacion(int id)
        {
            Habitacion habitacion = new Habitacion();

            try
            {
                habitacion = habitacion.buscarHabitacion(id);
                id = habitacion.id_habitacion;
                estado = habitacion.estado;
                tipo = habitacion.tipo;
                piso = habitacion.piso;
                numero = habitacion.numero;
                precio = habitacion.precio;

            }
            catch (Exception e)
            {
                throw new Exception("Consulta Cliente - " + e.Message);
            }

            return habitacion;
        }

        public void guardarHabitacion()
        {
            try
            {
                Habitacion Habitacion = cargarHabitacion();

                if (!Habitacion.guardarHabitacion(Habitacion))
                {
                    throw new Exception("Fallo al Guardar Habitacion");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Guardar Habitacion - " + e.Message);
            }
        }

        public void actualizarHabitacion()
        {
            try
            {
                Habitacion Habitacion = cargarHabitacion();

                if (!Habitacion.actualizarHabitacion(Habitacion))
                {
                    throw new Exception("Fallo al Actualizar Habitacion");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Actualizar Habitacion - " + e.Message);
            }
        }

        public void eliminarHabitacion()
        {
            try
            {
                Habitacion habitacion = cargarHabitacion();

                if (!habitacion.eliminarHabitacion(habitacion))
                {
                    throw new Exception("Fallo al Eliminar Habitacion");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Eliminar Habitacion - " + e.Message);
            }
        }

        public List<Habitacion> cargarHabitaciones()
        {
            HabitacionCollection habitacionColl = new HabitacionCollection();
            List<Habitacion> obsHabitaciones = new List<Habitacion>();
            obsHabitaciones = habitacionColl.cargarHabitaciones();

            return obsHabitaciones;
        }

        public void limpiar()
        {
            id_habitacion = 0;
            numero = 0;
            tipo = "";
            precio = 0.0;
            estado = "";
            piso = 0;
        }
    }
}
