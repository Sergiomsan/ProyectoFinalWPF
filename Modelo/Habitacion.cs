using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace ProyectoFinal.Modelo
{
    public class Habitacion
    {
        private int id_habitacionValue;

        public int id_habitacion
        { 
            get { return id_habitacionValue;} 
            set { id_habitacionValue = value;}
        }

        private int numeroValue;

        public int numero
        {
            get { return numeroValue; }
            set { numeroValue = value; }
        }

        private string? tipoValue;

        public string? tipo
        {
            get { return tipoValue; }
            set { tipoValue = value; }
        }

        private double precioValue;

        public double precio
        {
            get { return precioValue; }
            set { precioValue = value; }
        }

        private string? estadoValue;

        public string? estado 
        { 
            get { return estadoValue; } 
            set { estadoValue = value; } 
        }

        private int pisoValue;

        public int piso
        {
            get { return pisoValue; }
            set {  pisoValue = value; }
        }

        public Habitacion()
        {
            id_habitacion = 0;
            tipo = "";
            precio = 0.0;
            estado = "";
            piso = 0;
            numero = 0;
        }

        public Habitacion buscarHabitacion(int codigoHabitacion)
        {

            Habitacion Habitacion = new Habitacion();

            if (codigoHabitacion != 0)
            {
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
                            // comando a ejecutar en la BD
                            String consulta =
                                "SELECT id_habitacion, tipo, precio," +
                                     "estado, piso, numero " +
                                     "FROM habitacion WHERE id_habitacion = " +
                                     "@id_habitacion LIMIT 1";

                            using var comando = new MySqlCommand(consulta, conexionBD);

                            comando.Parameters.AddWithValue("@id_habitacion", codigoHabitacion.ToString());
                            comando.Prepare();

                            // Ejecución del comando
                            using var reader = comando.ExecuteReader();

                            if (reader.HasRows)
                            {
                                // Obtención del cursor con el resultado de una consulta
                                while (reader.Read())
                                {
                                    Habitacion.id_habitacion = reader.GetInt32(0);
                                    Habitacion.tipo = reader.GetString(1);
                                    Habitacion.precio = reader.GetDouble(2);
                                    Habitacion.estado = reader.GetString(3);
                                    Habitacion.piso = reader.GetInt32(4);
                                    Habitacion.numero = reader.GetInt32(5);
                                }
                            }
                            else
                            {
                                throw new Exception("No se encontraron Habitacions");

                            }
                        }
                        catch (InvalidOperationException ex)
                        {
                            throw new Exception("Incidencia en busqueda de Habitacions: " + ex.Message);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Incidencia al buscar Habitacions: " + ex.Message);
                }
                finally
                {
                    // siempre se cierra la conexion
                    Conexion.cerrarConexion();
                }
            }
            else
            {
                throw new Exception("Código de Habitacion no introducido. Introducir Código de Habitacion");
            }

            return Habitacion;
        }

        public bool existeHabitacion(int codigoHabitacion)
        {
            bool existe = false;

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
                        // Para modificar, verificar primero que exista el Habitacion.
                        // comando a ejecutar en la BD
                        String consulta =
                            "SELECT id_habitacion " +
                                 "FROM Habitacion WHERE id_habitacion = " +
                                 "@id_habitacion LIMIT 1";

                        using var comando = new MySqlCommand(consulta, conexionBD);

                        comando.Parameters.AddWithValue("@id_habitacion", codigoHabitacion);
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
                        throw new Exception("Incidencia en busqueda de Habitacion: " + ex.Message);
                    }
                    finally
                    {

                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Incidencia al buscar Habitacion: " + ex.Message);
            }
            finally
            {
                // siempre se cierra la conexion
                Conexion.cerrarConexion();
            }
        }

        public bool guardarHabitacion(Habitacion Habitacion)
        {
            bool operacion_ok = false;
            try
            {
                // No permitir guardar registros con datos vacios
                if (Habitacion.id_habitacion != 0 &&
                    Habitacion.numero != 0 &&
                    Habitacion.tipo != "" &&
                    Habitacion.precio != 0.0 &&
                    Habitacion.piso != 0 &&
                    Habitacion.estado != ""
                    )
                {
                    // Verificar si existe el registro. No se puede dar de alta
                    // un registro si ya existe
                    try
                    {
                        if (!existeHabitacion(Habitacion.id_habitacion))
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
                                    string sql = "INSERT INTO Habitacion (id_habitacion, numero, tipo, " +
                                        "precio, piso, estado) VALUES (@id_habitacion, @numero, @tipo, " +
                                        "@precio, @piso, @estado)";

                                    // comando a ejecutar en la BD
                                    using var comando = new MySqlCommand(sql, conexionBD);
                                    comando.Parameters.AddWithValue("@id_habitacion", Habitacion.id_habitacion);
                                    comando.Parameters.AddWithValue("@numero", Habitacion.numero);
                                    comando.Parameters.AddWithValue("@tipo", Habitacion.tipo);
                                    comando.Parameters.AddWithValue("@precio", Habitacion.precio);
                                    comando.Parameters.AddWithValue("@piso", Habitacion.piso);
                                    comando.Parameters.AddWithValue("@estado", Habitacion.estado);
                                    comando.Prepare();

                                    // Ejecución del comando
                                    comando.ExecuteNonQuery();

                                    operacion_ok = true;
                                    return operacion_ok;

                                }
                                catch (MySqlException ex)
                                {
                                    throw new Exception("Incidencia al guardar Habitacion: " + ex.Message);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Imposible guardar - Habitacion ya existe");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Incidencia: " + ex.Message);
                    }
                    finally
                    {
                        // siempre se cierra la conexion
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

        public bool actualizarHabitacion(Habitacion Habitacion)
        {
            Habitacion HabitacionEnBD = new Habitacion();

            bool operacion_ok = false;

            // No permitir modificar registros con datos vacios
            if (Habitacion.id_habitacion != 0 && Habitacion.numero != 0 &&
                Habitacion.tipo != "" && Habitacion.precio != 0.0 &&
                Habitacion.piso != 0 && Habitacion.estado != "")
            {
                if (existeHabitacion(Habitacion.id_habitacion))
                {
                    // Modificar datos si no son identicos a los que existen en la BD.
                    HabitacionEnBD = buscarHabitacion(Habitacion.id_habitacion);

                    if ((Habitacion.id_habitacion == HabitacionEnBD.id_habitacion ||
                        Habitacion.numero == HabitacionEnBD.numero ||
                        Habitacion.tipo.Equals(HabitacionEnBD.tipo) ||
                        Habitacion.precio == HabitacionEnBD.precio ||
                        Habitacion.piso == HabitacionEnBD.piso ||
                        Habitacion.estado.Equals(HabitacionEnBD.estado)) == true)
                    {
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
                                    string sql = "UPDATE Habitacion SET " +
                                    "numero=@numero," +
                                    "tipo=@tipo, precio=@precio, " +
                                    "piso=@piso, estado =@estado where id_habitacion = @id_habitacion;";
                                    // comando a ejecutar en la BD
                                    using var comando = new MySqlCommand(sql, conexionBD);

                                    comando.Parameters.AddWithValue("@id_habitacion", Habitacion.id_habitacion);
                                    comando.Parameters.AddWithValue("@numero", Habitacion.numero);
                                    comando.Parameters.AddWithValue("@tipo", Habitacion.tipo);
                                    comando.Parameters.AddWithValue("@precio", Habitacion.precio);
                                    comando.Parameters.AddWithValue("@piso", Habitacion.piso);
                                    comando.Parameters.AddWithValue("@estado", Habitacion.estado);
                                    comando.Prepare();

                                    // Ejecución del comando
                                    comando.ExecuteNonQuery();

                                    operacion_ok = true;
                                    return operacion_ok;
                                }
                                catch (MySqlException ex)
                                {
                                    throw new Exception("Incidencia al modificar Habitacion: " + ex.Message);
                                }
                            }
                        }
                        catch (MySqlException ex)
                        {
                            throw new Exception("Incidencia: " + ex.Message);
                        }
                        finally
                        {
                            // siempre se cierra la conexion
                            Conexion.cerrarConexion();
                        }
                    }
                    else
                    {
                        throw new Exception("Habitacion no modificado. Mismos datos en Base de Datos");
                    }
                }
                else
                {
                    throw new Exception("Imposible modificación - Habitacion no existe");
                }
            }
            else
            {
                throw new Exception("Datos vacios. No se puede modificar Habitacion");
            }
        }

        public bool eliminarHabitacion(Habitacion Habitacion)
        {
            bool operacion_ok = false;

            // No se puede eliminar Habitacion si codigo vacio
            if (Habitacion.id_habitacion != 0)
            {
                if (existeHabitacion(Habitacion.id_habitacion))
                {
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
                                string sql = "DELETE FROM habitacion " +
                                    "WHERE id_habitacion=@id_habitacion;";
                                // comando a ejecutar en la BD
                                using var comando = new MySqlCommand(sql, conexionBD);

                                comando.Parameters.AddWithValue("@id_habitacion", Habitacion.id_habitacion);
                                comando.Prepare();

                                // Ejecución del comando
                                comando.ExecuteNonQuery();

                                operacion_ok = true;
                                return operacion_ok;
                            }
                            catch (MySqlException ex)
                            {
                                throw new Exception("Incidencia al eliminar Habitacion: " + ex.Message);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Incidencia: " + ex.Message);
                    }
                    finally
                    {
                        // siempre se cierra la conexion
                        Conexion.cerrarConexion();
                    }
                }
                else
                {
                    throw new Exception("Imposible eliminación - Habitacion no existe");
                }
            }
            else
            {
                throw new Exception("Datos vacios. No se puede eliminar Habitacion");
            }
        }
    }
}
