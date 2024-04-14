using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto2Progra3.Interfaz
{
    public class LoginManager
    {
        public static string nombre { get; set; }
        public static byte[] imagenEmpleado;

        public static bool ValidarLogin(string correo, string password)
        {
            string passwordHash = ConvertirSHA256(password);
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("spValidarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Correo", correo);
                    command.Parameters.AddWithValue("@Contrasena", passwordHash);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nombre = $"{reader["Nombres"]} {reader["Apellidos"]}";
                            imagenEmpleado = reader["Imagen"] as byte[];
                            return true;
                        }
                        return false;
                    }
                }
            }
        }
        private static string ConvertirSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
