using System; 
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace ProyectoFinal
{
    public class Conexion
    {
        private static MySqlConnectionStringBuilder builder = null;
        private static MySqlConnection conn = null;
        private static String SERVIDOR = "192.168.56.1";
        private static uint PUERTO = 3306;
        private static String BD = "hoteldb";
        private static String USUARIO = "admin";
        private static String PASSWORD = "admin";


        private static MySqlConnectionStringBuilder getBuilder()
        {
            if (builder == null)
            {
                try
                {
                    builder = new MySqlConnectionStringBuilder();
                }
                catch
                {
                    builder = null;
                }
                return builder;
            }
            return builder;
        }

        public static MySqlConnection obtenerConexionAbierta()
        {
            if (conn == null)
            {
                if (getBuilder() == null)
                {
                    return null;
                }
                builder.Server = SERVIDOR;
                builder.Port = PUERTO;
                builder.UserID = USUARIO;
                builder.Password = PASSWORD;
                builder.Database = BD;

                try
                {
                    conn = new MySqlConnection(builder.ToString());
                    conn.Open();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: " + ex.ToString());
                }
            }
            else if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Close();
                    conn.Open();
                }
                catch
                {
                    return null;
                }
            }
            if (conn.State == ConnectionState.Open)
            {
                Console.WriteLine("Conexion a la base de datos establecida");
            }
            return conn;

        }
    

        public static void cerrarConexion()
        {
            if (conn != null)
            {
                try
                {
                    conn.Close();

                    if (conn.State == ConnectionState.Closed)
                    {
                        Console.Write("BD Desconectada");
                    }
                }
                catch (Exception)
                {
                    Console.Write("Error al desconectar la BD");
                }
            }
        }
    }
}
