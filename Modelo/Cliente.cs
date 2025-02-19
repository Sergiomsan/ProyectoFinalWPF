using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Modelo
{
    public class Cliente
    {
        private string? dniValue;

        public string? dni
        { 
          get { return dniValue;} 
          set { dniValue = value;} 
        }

        private string? nombreValue;

        public string? nombre
        {
            get { return  nombreValue;}
            set { nombreValue = value;}
        }

        private string? telefonoValue;

        public string? telefono
        {
            get { return telefonoValue; }
            set { telefonoValue = value; }
        }

        private string? emailValue;

        public string email
        {
            get { return emailValue; }
            set { emailValue = value; }
        }

        public Cliente ()
        {
            dni = "";
            nombre = "";
            telefono = "";
            email = "";
        }

        public Cliente buscarCliente(string codigoCliente)
        {

            Cliente Cliente = new Cliente();

            if (codigoCliente != "")
            {
                try
                {
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
                                "SELECT dni, nombre, " +
                                     "telefono, email " +
                                     "FROM cliente WHERE dni = " +
                                     "@dni LIMIT 1";

                            using var comando = new MySqlCommand(consulta, conexionBD);

                            comando.Parameters.AddWithValue("@dni", codigoCliente);
                            comando.Prepare();

                            using var reader = comando.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Cliente.dni = reader.GetString(0);
                                    Cliente.nombre = reader.GetString(1);
                                    Cliente.telefono = reader.GetString(2);
                                    Cliente.email = reader.GetString(3);
                                }
                            }
                            else
                            {
                                throw new Exception("No se encontraron Clientes");

                            }
                        }
                        catch (InvalidOperationException ex)
                        {
                            throw new Exception("Incidencia en busqueda de Clientes: " + ex.Message);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Incidencia al buscar Clientes: " + ex.Message);
                }
                finally
                {
                    Conexion.cerrarConexion();
                }
            }
            else
            {
                throw new Exception("Código de Cliente no introducido. Introducir Código de Cliente");
            }

            return Cliente;
        }

        public bool existeCliente(string codigoCliente)
        {
            bool existe = false;

            try
            {
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
                            "SELECT dni FROM Cliente " +
                            "WHERE dni = @codigoCliente LIMIT 1";

                        using var comando = new MySqlCommand(consulta, conexionBD);

                        comando.Parameters.AddWithValue("@codigoCliente", codigoCliente);
                        comando.Prepare();

                        // Ejecución del comando
                        using var reader = comando.ExecuteReader();

                        if (reader.HasRows)
                        {
                            existe = true;
                        }
                        return existe;
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new Exception("Incidencia en busqueda de Cliente: " + ex.Message);
                    }
                    finally
                    {

                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Incidencia al buscar Cliente: " + ex.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public bool guardarCliente(Cliente Cliente)
        {
            bool operacion_ok = false;
            try
            {
                if (Cliente.dni != "" &&
                    Cliente.nombre != "" &&
                    Cliente.email != "" &&
                    Cliente.telefono != "")
                {
                    try
                    {
                        if (!existeCliente(Cliente.dni))
                        {
                            MySqlConnection conexionBD = Conexion.obtenerConexionAbierta();

                            if (conexionBD == null)
                            {
                                throw new Exception("Fallo en Conexion a BD");
                            }
                            else
                            {
                                try
                                {
                                    string sql = "INSERT INTO cliente (dni, nombre, email, telefono) VALUES (@dni, @nombre, @email, @telefono)";

                                    using var comando = new MySqlCommand(sql, conexionBD);
                                    comando.Parameters.AddWithValue("@dni", Cliente.dni);
                                    comando.Parameters.AddWithValue("@nombre", Cliente.nombre);
                                    comando.Parameters.AddWithValue("@email", Cliente.email);
                                    comando.Parameters.AddWithValue("@telefono", Cliente.telefono);
                                    comando.Prepare();

                                    comando.ExecuteNonQuery();

                                    operacion_ok = true;
                                    return operacion_ok;

                                }
                                catch (MySqlException ex)
                                {
                                    throw new Exception("Incidencia al guardar Cliente: " + ex.Message);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Imposible guardar - Cliente ya existe");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Incidencia: " + ex.Message);
                    }
                    finally
                    {
                        Conexion.cerrarConexion();
                    }
                }
                else
                {
                    throw new Exception("Algunos datos vacios. Introducir todos los datos");
                }
            }
            catch (FormatException fex)
            {
                throw new Exception("Datos incorrectos: " + fex.Message);
            }
        }

        public bool actualizarCliente(Cliente Cliente)
        {
            Cliente ClienteEnBD = new Cliente();;

            bool operacion_ok = false;

            if (Cliente.dni != "" && Cliente.nombre != "" &&
                Cliente.telefono != "" && Cliente.email != "")
            {
                if (existeCliente(Cliente.dni))
                {
                    ClienteEnBD = buscarCliente(Cliente.dni);

                    if ((Cliente.dni.Equals(ClienteEnBD.dni) ||
                        Cliente.nombre.Equals(ClienteEnBD.nombre) ||
                        Cliente.telefono.Equals(ClienteEnBD.telefono) ||
                        Cliente.email.Equals(ClienteEnBD.email)) == true)
                    {
                        try
                        {
                            MySqlConnection conexionBD = Conexion.obtenerConexionAbierta();

                            if (conexionBD == null)
                            {
                                throw new Exception("Fallo en Conexion a BD");
                            }
                            else
                            {
                                try
                                {
                                    string sql = "UPDATE cliente SET " +
                                    "dni=@dni, " +
                                    "nombre=@nombre, telefono=@telefono, " +
                                    "email=@email where dni = @dni;";

                                    using var comando = new MySqlCommand(sql, conexionBD);

                                    comando.Parameters.AddWithValue("@dni", Cliente.dni);
                                    comando.Parameters.AddWithValue("@nombre", Cliente.nombre);
                                    comando.Parameters.AddWithValue("@telefono", Cliente.telefono);
                                    comando.Parameters.AddWithValue("@email", Cliente.email);
                                    comando.Prepare();

                                    comando.ExecuteNonQuery();

                                    operacion_ok = true;
                                    return operacion_ok;
                                }
                                catch (MySqlException ex)
                                {
                                    throw new Exception("Incidencia al modificar Cliente: " + ex.Message);
                                }
                            }
                        }
                        catch (MySqlException ex)
                        {
                            throw new Exception("Incidencia: " + ex.Message);
                        }
                        finally
                        {
                            Conexion.cerrarConexion();
                        }
                    }
                    else
                    {
                        throw new Exception("Cliente no modificado. Mismos datos en Base de Datos");
                    }
                }
                else
                {
                    throw new Exception("Imposible modificación - Cliente no existe");
                }
            }
            else
            {
                throw new Exception("Datos vacios. No se puede modificar Cliente");
            }
        }

        public bool eliminarCliente(Cliente Cliente)
        {
            bool operacion_ok = false;

            if (Cliente.dni != "")
            {
                if (existeCliente(Cliente.dni))
                {
                    try
                    {
                        MySqlConnection conexionBD = Conexion.obtenerConexionAbierta();

                        if (conexionBD == null)
                        {
                            throw new Exception("Fallo en Conexion a BD");
                        }
                        else
                        {
                            try
                            {
                                string sql = "DELETE FROM cliente " +
                                    "WHERE dni=@dni;";

                                using var comando = new MySqlCommand(sql, conexionBD);

                                comando.Parameters.AddWithValue("@dni", Cliente.dni);
                                comando.Prepare();


                                comando.ExecuteNonQuery();

                                operacion_ok = true;
                                return operacion_ok;
                            }
                            catch (MySqlException ex)
                            {
                                throw new Exception("Incidencia al eliminar Cliente: " + ex.Message);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Incidencia: " + ex.Message);
                    }
                    finally
                    {
                        Conexion.cerrarConexion();
                    }
                }
                else
                {
                    throw new Exception("Imposible eliminación - Cliente no existe");
                }
            }
            else
            {
                throw new Exception("Datos vacios. No se puede eliminar Cliente");
            }
        }
    }
}
