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
            if (codigoHabitacion == 0)
                throw new Exception("Código de habitación no válido.");

            try
            {
                using var conexionBD = Conexion.obtenerConexionAbierta() ?? throw new Exception("Fallo en conexión a BD");
                using var comando = new MySqlCommand(
                    "SELECT id_habitacion, tipo, precio, estado, piso, numero FROM habitacion WHERE id_habitacion = @id LIMIT 1",
                    conexionBD);

                comando.Parameters.AddWithValue("@id", codigoHabitacion);

                using var reader = comando.ExecuteReader();

                if (reader.Read()) // Si hay una fila, se asignan los valores
                {
                    return new Habitacion
                    {
                        id_habitacion = reader.GetInt32(0),
                        tipo = reader.GetString(1),
                        precio = reader.GetDouble(2),
                        estado = reader.GetString(3),
                        piso = reader.GetInt32(4),
                        numero = reader.GetInt32(5)
                    };
                }

                throw new Exception("No se encontró la habitación.");
            }
            catch (MySqlException ex)
            {
                throw new Exception("Incidencia al buscar habitación: " + ex.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }


        public bool existeHabitacion(int codigoHabitacion)
        {
            try
            {
                using var conexionBD = Conexion.obtenerConexionAbierta() ?? throw new Exception("Fallo en conexión a BD");
                using var comando = new MySqlCommand("SELECT 1 FROM Habitacion WHERE id_habitacion = @id LIMIT 1", conexionBD);

                comando.Parameters.AddWithValue("@id", codigoHabitacion);
                using var reader = comando.ExecuteReader();

                return reader.HasRows;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Incidencia al buscar habitación: " + ex.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public bool guardarHabitacion(Habitacion habitacion)
        {
            if (habitacion.numero == 0 || string.IsNullOrEmpty(habitacion.tipo) ||
                habitacion.precio == 0.0 || habitacion.piso == 0 || string.IsNullOrEmpty(habitacion.estado))
                throw new Exception("Algunos datos están vacíos. Introducir todos los datos");

            try
            {
                using var conexionBD = Conexion.obtenerConexionAbierta() ?? throw new Exception("Fallo en conexión a BD");
                using var comando = new MySqlCommand(
                    "INSERT INTO Habitacion (numero, tipo, precio, piso, estado) VALUES (@numero, @tipo, @precio, @piso, @estado)",
                    conexionBD);

                comando.Parameters.AddWithValue("@numero", habitacion.numero);
                comando.Parameters.AddWithValue("@tipo", habitacion.tipo);
                comando.Parameters.AddWithValue("@precio", habitacion.precio);
                comando.Parameters.AddWithValue("@piso", habitacion.piso);
                comando.Parameters.AddWithValue("@estado", habitacion.estado);

                comando.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Incidencia al guardar habitación: " + ex.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public bool actualizarHabitacion(Habitacion habitacion)
        {
            if (habitacion.id_habitacion == 0 || habitacion.numero == 0 ||
                string.IsNullOrEmpty(habitacion.tipo) || habitacion.precio == 0.0 ||
                habitacion.piso == 0 || string.IsNullOrEmpty(habitacion.estado))
                throw new Exception("Datos vacíos. No se puede modificar la habitación");

            if (!existeHabitacion(habitacion.id_habitacion))
                throw new Exception("Imposible modificación - Habitación no existe");

            var habitacionEnBD = buscarHabitacion(habitacion.id_habitacion);

            if (habitacion.Equals(habitacionEnBD))
                throw new Exception("Habitación no modificada. Mismos datos en Base de Datos");

            try
            {
                using var conexionBD = Conexion.obtenerConexionAbierta() ?? throw new Exception("Fallo en conexión a BD");
                using var comando = new MySqlCommand(
                    "UPDATE Habitacion SET numero=@numero, tipo=@tipo, precio=@precio, piso=@piso, estado=@estado WHERE id_habitacion=@id",
                    conexionBD);

                comando.Parameters.AddWithValue("@id", habitacion.id_habitacion);
                comando.Parameters.AddWithValue("@numero", habitacion.numero);
                comando.Parameters.AddWithValue("@tipo", habitacion.tipo);
                comando.Parameters.AddWithValue("@precio", habitacion.precio);
                comando.Parameters.AddWithValue("@piso", habitacion.piso);
                comando.Parameters.AddWithValue("@estado", habitacion.estado);

                comando.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Incidencia al modificar habitación: " + ex.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }


        public bool eliminarHabitacion(Habitacion habitacion)
        {
            if (habitacion.id_habitacion == 0)
                throw new Exception("Datos vacíos. No se puede eliminar Habitación");

            if (!existeHabitacion(habitacion.id_habitacion))
                throw new Exception("Imposible eliminación - Habitación no existe");

            try
            {
                using var conexionBD = Conexion.obtenerConexionAbierta() ?? throw new Exception("Fallo en conexión a BD");
                using var comando = new MySqlCommand("DELETE FROM habitacion WHERE id_habitacion=@id", conexionBD);

                comando.Parameters.AddWithValue("@id", habitacion.id_habitacion);
                comando.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Incidencia al eliminar Habitación: " + ex.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }
    }
}
