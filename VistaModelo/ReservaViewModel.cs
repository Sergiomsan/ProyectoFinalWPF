using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinal.Modelo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoFinal.VistaModelo
{
    public partial class ReservaViewModel : INotifyPropertyChanged
    {
        private int id = 0;
        private string dniCliente = "";
        private int idHabitacion = 0;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private string estado = "";
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Dni
        {
            get { return dniCliente; }
            set
            {
                dniCliente = value;
                OnPropertyChanged("Dni");
            }
        }
        public int Id_habitacion
        {
            get { return idHabitacion; }
            set
            {
                idHabitacion = value;
                OnPropertyChanged("Id_habitacion");
            }
        }
        public DateTime Fecha_inicio
        {
            get { return fechaInicio; }
            set
            {
                fechaInicio = value;
                OnPropertyChanged("Fecha_inicio");
            }
        }

        public DateTime Fecha_Fin
        {
            get
            {
                return fechaFin;
            }
            set
            {
                fechaFin = value;
                OnPropertyChanged("Fecha_Fin");
            }
        }

        public string Estado
        {
            get { return estado; }
            set
            {
                estado = value;
                OnPropertyChanged("Estado");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void buscarReserva()
        {
            Reserva reserva = new();

            try
            {
                reserva = reserva.buscarReserva(Id);

                id = reserva.idReserva;
                dniCliente = reserva.dniCliente;
                idHabitacion = reserva.idHabitacion;
                fechaInicio = reserva.fechaInicio;
                fechaFin = reserva.fechaFin;
                estado = reserva.estado;

            }
            catch (Exception e)
            {
                throw new Exception("Consulta Reserva - " + e.Message);
            }
        }

        public Reserva buscarReserva(int id)
        {
            Reserva reserva = new Reserva();

            try
            {
                reserva = reserva.buscarReserva(id);
                id = reserva.idReserva;
                dniCliente = reserva.dniCliente;
                idHabitacion = reserva.idHabitacion;
                fechaInicio = reserva.fechaInicio;
                fechaFin = reserva.fechaFin;
                estado = reserva.estado;

            }
            catch (Exception e)
            {
                throw new Exception("Consulta Cliente - " + e.Message);
            }

            return reserva;
        }

        public void guardarReserva()
        {
            try
            {
                Reserva Reserva = cargaReserva();

                if (!Reserva.guardarReserva(Reserva))
                {
                    throw new Exception("Fallo al Guardar Reserva");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Guardar Reserva - " + e.Message);
            }
        }

        public void actualizarReserva()
        {
            try
            {
                Reserva Reserva = cargaReserva();

                if (!Reserva.actualizarReserva(Reserva))
                {
                    throw new Exception("Fallo al Actualizar Reserva");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Actualizar Reserva - " + e.Message);
            }
        }

        public void eliminarReserva()
        {
            try
            {
                Reserva Reserva = new Reserva();

                Reserva.idReserva = id;

                if (!Reserva.eliminarReserva(Reserva))
                {
                    throw new Exception("Fallo al Eliminar Reserva");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Eliminar Reserva - " + e.Message);
            }
        }

        private Reserva cargaReserva()
        {
            Reserva Reserva = new Reserva();
            Reserva.idReserva = Id;
            Reserva.dniCliente = dniCliente;
            Reserva.idHabitacion = idHabitacion;
            Reserva.fechaInicio = fechaInicio;
            Reserva.fechaFin = fechaFin;
            Reserva.estado = estado;

            return Reserva;
        }

        public void limpiar()
        {
            Id = 1;
            dniCliente = "1";
            idHabitacion = 1;
            fechaInicio = DateTime.Now;
            fechaFin = DateTime.MinValue;
            Estado = "Pendiente";
        }

        public List<Reserva> cargarReservas()
        {
            ReservaCollection reservaColl = new ReservaCollection();
            List<Reserva> obsReservas = new List<Reserva>();

            obsReservas = reservaColl.CargarReservas();

            return obsReservas;
        }
    }
}
