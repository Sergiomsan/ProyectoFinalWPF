using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace ProyectoFinal.Modelo
{
    class HabitacionCollection : ObservableCollection<Habitacion>
    {
        public HabitacionCollection cargarHabitacion()
        {

            HabitacionCollection HabitacionCollection = new HabitacionCollection();

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
                        String consulta = "select * from habitacion";
                        using var comando = new MySqlCommand(consulta, conexionBD);

                        using var reader = comando.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                HabitacionCollection.Add(new Habitacion()
                                {
                                    id_habitacion = reader.GetInt32(0),
                                    tipo = reader.GetString(1),
                                    precio = reader.GetDouble(2),
                                    estado = reader.GetString(3),
                                    piso = reader.GetInt32(4),
                                    numero = reader.GetInt32(5)
                                });
                            }
                        }
                        return HabitacionCollection;
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

        public List<Habitacion> cargarHabitaciones()
        {
            List<Habitacion> listaClientes = new List<Habitacion>();
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
                        String consulta = "select * from habitacion";
                        using var comando = new MySqlCommand(consulta, conexionBD);

                        using var reader = comando.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listaClientes.Add(new Habitacion()
                                {
                                    id_habitacion = reader.GetInt32(0),
                                    numero = reader.GetInt32(1),
                                    tipo = reader.GetString(2),
                                    precio = reader.GetDouble(3),
                                    estado = reader.GetString(4),
                                    piso = reader.GetInt32(5)
                                });
                            }
                        }
                        return listaClientes;
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new Exception("Error al cargar las habitaciones: " + ex.Message);
                    }
                }
            }
            catch (MySqlException e)
            {
                throw new Exception("Error al buscar habitaciones: " + e.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }
    }
}
