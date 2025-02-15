using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace ProyectoFinal.Modelo
{
    public class Reserva
    {
        private int idReservaValue;

        public int idReserva
        {  
            get { return idReservaValue; } 
            set { idReservaValue = value; }
        }

        private String dniClienteValue;

        public String dniCliente
        {
            get { return dniClienteValue; }
            set { dniClienteValue = value; }
        }

        private int idHabitacionValue;

        public int idHabitacion
        {
            get { return idHabitacionValue; }
            set { idHabitacionValue = value; }
        }

        private DateTime fechaInicioValue;

        public DateTime fechaInicio
        {
            get { return fechaInicioValue; }
            set { fechaInicioValue = value; }
        }

        private DateTime fechaFinValue;

        public DateTime fechaFin
        {
            get { return fechaFinValue; }
            set { fechaFinValue = value; }
        }

        private string? estadoValue;

        public string? estado
        {
            get { return estadoValue; }
            set { estadoValue = value; }
        }

        public Reserva()
        {
            idReserva = 0;
            dniCliente = "";
            idHabitacion = 0;
            fechaInicio = DateTime.Now;
            fechaFin = DateTime.MinValue;
            estado = "Pendiente";
        }

        public Reserva buscarReserva(int codigoReserva)
        {

            Reserva Reserva = new Reserva();

            if (codigoReserva != 0)
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
                                "SELECT id_reserva, dni, id_habitacion," +
                                     "fecha_inicio, fecha_fin, " +
                                     "estado " +
                                     "FROM reserva WHERE id_reserva = @idReserva" +
                                     " LIMIT 1";

                            using var comando = new MySqlCommand(consulta, conexionBD);

                            comando.Parameters.AddWithValue("@idReserva", codigoReserva);
                            comando.Prepare();

                            // Ejecución del comando
                            using var reader = comando.ExecuteReader();

                            if (reader.HasRows)
                            {
                                // Obtención del cursor con el resultado de una consulta
                                while (reader.Read())
                                {
                                    Reserva.idReserva = reader.GetInt32(0);
                                    Reserva.dniCliente = reader.GetString(1);
                                    Reserva.idHabitacion = reader.GetInt32(2);
                                    Reserva.fechaInicio = reader.GetDateTime(3);
                                    Reserva.fechaFin = reader.GetDateTime(4);
                                    Reserva.estado = reader.GetString(5);
                                }
                            }
                            else
                            {
                                throw new Exception("No se encontraron Reservas");

                            }
                        }
                        catch (InvalidOperationException ex)
                        {
                            throw new Exception("Incidencia en busqueda de Reservas: " + ex.Message);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Incidencia al buscar Reservas: " + ex.Message);
                }
                finally
                {
                    // siempre se cierra la conexion
                    Conexion.cerrarConexion();
                }
            }
            else
            {
                throw new Exception("Código de Reserva no introducido. Introducir Código de Reserva");
            }

            return Reserva;
        }



        public bool existeReserva(int codigoReserva)
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
                        // Para modificar, verificar primero que exista el Reserva.
                        // comando a ejecutar en la BD
                        String consulta =
                            "SELECT id_reserva " +
                                 "FROM reserva WHERE id_reserva = @idReserva LIMIT 1";

                        using var comando = new MySqlCommand(consulta, conexionBD);

                        comando.Parameters.AddWithValue("@idReserva", codigoReserva);
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
                        throw new Exception("Incidencia en busqueda de Reserva: " + ex.Message);
                    }
                    finally
                    {

                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Incidencia al buscar Reserva: " + ex.Message);
            }
            finally
            {
                // siempre se cierra la conexion
                Conexion.cerrarConexion();
            }
        }

        public bool guardarReserva(Reserva Reserva)
        {
            bool operacion_ok = false;
            try
            {
                // No permitir guardar registros con datos vacios
                if (Reserva.idReserva != 0 &&
                    Reserva.dniCliente != "" &&
                    Reserva.idHabitacion != 0 &&
                    Reserva.fechaInicio != null &&
                    Reserva.fechaFin != null &&
                    Reserva.estado != "")
                {
                    // Verificar si existe el registro. No se puede dar de alta
                    // un registro si ya existe
                    try
                    {
                        if (!existeReserva(Reserva.idReserva))
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
                                    string sql = "INSERT INTO reserva (id_reserva, " +
                                        "dni, id_habitacion, fecha_inicio, fecha_fin, estado" +
                                        ") VALUES (@idReserva, @dni, @idHabitacion, " +
                                        "@fechaInicio, @fechaFin, @estado)";

                                    // comando a ejecutar en la BD
                                    using var comando = new MySqlCommand(sql, conexionBD);
                                    comando.Parameters.AddWithValue("@idReserva", Reserva.idReserva);
                                    comando.Parameters.AddWithValue("@dni", Reserva.dniCliente);
                                    comando.Parameters.AddWithValue("@idHabitacion", Reserva.idHabitacion);
                                    comando.Parameters.AddWithValue("@fechaInicio", Reserva.fechaInicio);
                                    comando.Parameters.AddWithValue("@fechaFin", Reserva.fechaFin);
                                    comando.Parameters.AddWithValue("@estado", Reserva.estado);
                                    comando.Prepare();

                                    // Ejecución del comando
                                    comando.ExecuteNonQuery();

                                    operacion_ok = true;
                                    return operacion_ok;

                                }
                                catch (MySqlException ex)
                                {
                                    throw new Exception("Incidencia al guardar Reserva: " + ex.Message);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Imposible guardar - Reserva ya existe");
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

        public bool actualizarReserva(Reserva reserva)
        {
            Reserva ReservaEnBD = new Reserva();

            bool operacion_ok = false;

            // No permitir modificar registros con datos vacios
            if (reserva.idReserva != 0 && reserva.dniCliente != "" &&
                reserva.idHabitacion != 0 && reserva.fechaInicio != DateTime.MinValue &&
                reserva.fechaFin != DateTime.MinValue && reserva.estado != "")
            {
                if (existeReserva(reserva.idReserva))
                {
                    // Modificar datos si no son identicos a los que existen en la BD.
                    ReservaEnBD = buscarReserva(reserva.idReserva);

                    if ((reserva.dniCliente.Equals(ReservaEnBD.dniCliente) ||
                        reserva.idReserva == ReservaEnBD.idReserva ||
                        reserva.idHabitacion == ReservaEnBD.idHabitacion ||
                        reserva.fechaInicio == ReservaEnBD.fechaFin ||
                        reserva.fechaFin == ReservaEnBD.fechaFin ||
                        reserva.estado.Equals(ReservaEnBD.estado)) == true)
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
                                    string sql = "UPDATE reserva SET " +
                                    "dni=@dni, id_habitacion=@idHabitacion, " +
                                    "fecha_inicio=@fechaInicio, fecha_fin=@fechaFin, " +
                                    "estado=@estado " +
                                    "WHERE id_reserva=@idReserva;";
                                    // comando a ejecutar en la BD
                                    using var comando = new MySqlCommand(sql, conexionBD);

                                    comando.Parameters.AddWithValue("@idReserva", reserva.idReserva);
                                    comando.Parameters.AddWithValue("@dni", reserva.dniCliente);
                                    comando.Parameters.AddWithValue("@idHabitacion", reserva.idHabitacion);
                                    comando.Parameters.AddWithValue("@fechaInicio", reserva.fechaInicio);
                                    comando.Parameters.AddWithValue("@fechaFin", reserva.fechaFin);
                                    comando.Parameters.AddWithValue("@estado", reserva.estado);
                                    comando.Prepare();

                                    // Ejecución del comando
                                    comando.ExecuteNonQuery();

                                    operacion_ok = true;
                                    return operacion_ok;
                                }
                                catch (MySqlException ex)
                                {
                                    throw new Exception("Incidencia al modificar Reserva: " + ex.Message);
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
                        throw new Exception("Reserva no modificado. Mismos datos en Base de Datos");
                    }
                }
                else
                {
                    throw new Exception("Imposible modificación - Reserva no existe");
                }
            }
            else
            {
                throw new Exception("Datos vacios. No se puede modificar Reserva");
            }
        }

        public bool eliminarReserva(Reserva Reserva)
        {
            bool operacion_ok = false;

            // No se puede eliminar Reserva si codigo vacio
            if (Reserva.idReserva != 0)
            {
                if (existeReserva(Reserva.idReserva))
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
                                string sql = "DELETE FROM reserva " +
                                    "WHERE id_reserva=@idReserva;";
                                // comando a ejecutar en la BD
                                using var comando = new MySqlCommand(sql, conexionBD);

                                comando.Parameters.AddWithValue("@idReserva", Reserva.idReserva);
                                comando.Prepare();

                                // Ejecución del comando
                                comando.ExecuteNonQuery();

                                operacion_ok = true;
                                return operacion_ok;
                            }
                            catch (MySqlException ex)
                            {
                                throw new Exception("Incidencia al eliminar Reserva: " + ex.Message);
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
                    throw new Exception("Imposible eliminación - Reserva no existe");
                }
            }
            else
            {
                throw new Exception("Datos vacios. No se puede eliminar Reserva");
            }
        }
    }
}
