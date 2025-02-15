using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using ProyectoFinal.VIsta;

namespace ProyectoFinal.Modelo
{
    class ClienteCollection : ObservableCollection<Cliente>
    {
        public ClienteCollection consultar(String dato)
        {

        ClienteCollection lista = new ClienteCollection();

        try
        {
            // Obtener una conexión abierta a la BD
            MySqlConnection conexionBD = Conexion.obtenerConexionAbierta();

            if (conexionBD == null)
            {
                throw new Exception("Fallo en Conexion a BD");
            }
            else
            {
                    try
                    {
                        String consulta =
                            "SELECT c.dni AS dni, " +
                                    "c.nombre AS Nombre, " +
                                    "c.telefono AS Telefono, " +
                                    "e.email AS Email " +
                                    "FROM cliente c ";


                        using var comando = new MySqlCommand(consulta, conexionBD);
                        comando.Prepare();

                        // Ejecución del comando
                        using var reader = comando.ExecuteReader();

                        if (reader.HasRows)
                        {

                            // Obtención del cursor con el resultado de una consulta
                            while (reader.Read())
                            {
                                Cliente producto = new Cliente();

                                producto.dni = reader.GetString("Dni");
                                producto.nombre = reader.GetString("Nombre");
                                producto.telefono = reader.GetString("Telefono");
                                producto.email = reader.GetString("Email");

                                lista.Add(producto);
                            }
                        }
                        else
                        {
                            throw new Exception("No se encontraron productos");
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new Exception("Incidencia en busqueda de productos: " + ex.Message);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Incidencia al buscar productos: " + ex.Message);
            }
            finally
            {
                // siempre se cierra la conexion
                Conexion.cerrarConexion();
            }
            return lista;
        }
        
        public ClienteCollection cargarCliente() {
        
            ClienteCollection clienteCollection = new ClienteCollection();

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
                        String consulta = "select * from cliente";
                        using var comando = new MySqlCommand(consulta, conexionBD);

                        using var reader = comando.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                clienteCollection.Add(new Cliente()
                                {
                                   dni = reader.GetString(0),
                                   nombre = reader.GetString(1),
                                   email = reader.GetString(2),
                                   telefono = reader.GetString(3)
                                });
                            }
                        }
                        return clienteCollection;
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
        public List<Cliente> cargarClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();
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
                        String consulta = "select * from cliente";
                        using var comando = new MySqlCommand(consulta, conexionBD);

                        using var reader = comando.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listaClientes.Add(new Cliente()
                                {
                                    dni = reader.GetString(0),
                                    nombre = reader.GetString(1),
                                    email = reader.GetString(2),
                                    telefono = reader.GetString(3)
                                });
                            }
                        }
                        return listaClientes;
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new Exception("Error al cargar los clientes: " + ex.Message);
                    }
                }
            }
            catch (MySqlException e)
            {
                throw new Exception("Error al buscar clientes: " + e.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }
    }
}
