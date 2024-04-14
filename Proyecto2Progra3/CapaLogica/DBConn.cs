using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Examen2progra.Clases
{
    public class DBConn
    {
        private static string server = "192.168.1.8";
        private static string database = "PuntoDeVenta"; 
        private static string userId = "sa";
        private static string password = "Zyrs2023";
        private static string connectionString = $"Server={server};Database={database};User Id={userId};Password={password};";

        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conexion = new SqlConnection(connectionString);
                conexion.Open();
                return conexion;
            }
            catch (SqlException ex)
            {
               
                throw new Exception("Error al conectar con la base de datos: " + ex.Message);
            }
        }
    }
}
