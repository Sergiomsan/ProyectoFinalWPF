using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace ProyectoFinal.Modelo
{
    class ReservaCollection : ObservableCollection<Reserva>
    {
        public ReservaCollection cargarReservas()
        {

            ReservaCollection ReservaCollection = new ReservaCollection();

            try
            {
                MySqlConnection conexionBD = Conexion.obtenerConexionAbierta();

                if (conexionBD == null)
                {
                    throw new Exception("Fallo en la conexion a BD");
                }
                else
                {
                    try
                    {
                        String consulta = "select * from reserva";
                        using var comando = new MySqlCommand(consulta, conexionBD);

                        using var reader = comando.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ReservaCollection.Add(new Reserva()
                                {
                                    idReserva = reader.GetInt32(0),
                                    dniCliente = reader.GetString(1),
                                    idHabitacion = reader.GetInt32(2),
                                    fechaInicio = reader.GetDateTime(3),
                                    fechaFin = reader.GetDateTime(4),
                                    estado = reader.GetString(3)
                                });
                            }
                        }
                        return ReservaCollection;
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new Exception("Error al cargar las categorias: " + ex.Message);
                    }
                }
            }
            catch (MySqlException e)
            {
                throw new Exception("Error al buscar categorias: " + e.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public List<Reserva> CargarReservas()
        {
            List<Reserva> listaReservas = new List<Reserva>();
            try
            {
                MySqlConnection conexionBD = Conexion.obtenerConexionAbierta();

                if (conexionBD == null)
                {
                    throw new Exception("Fallo en la conexion a BD");
                }
                else
                {
                    try
                    {
                        String consulta = "select * from Reserva";
                        using var comando = new MySqlCommand(consulta, conexionBD);

                        using var reader = comando.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listaReservas.Add(new Reserva()
                                {
                                    idReserva = reader.GetInt32(0),
                                    dniCliente = reader.GetString(1),
                                    idHabitacion = reader.GetInt32(2),
                                    fechaInicio = reader.GetDateTime(3),
                                    fechaFin = reader.GetDateTime(4),
                                    estado = reader.GetString(5)
                                });
                            }
                        }
                        return listaReservas;
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new Exception("Error al cargar los Reservas: " + ex.Message);
                    }
                }
            }
            catch (MySqlException e)
            {
                throw new Exception("Error al buscar Reservas: " + e.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }
    }
}
