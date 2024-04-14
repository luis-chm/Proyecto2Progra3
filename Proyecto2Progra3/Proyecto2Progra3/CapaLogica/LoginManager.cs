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
        public static bool ValidarLogin(string correo, string password)
        {
            string passwordHash = ConvertirSHA256(password);
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("spValidarLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Correo", correo);
                    command.Parameters.AddWithValue("@ContrasenaHash", passwordHash);

                    var paramResultado = new SqlParameter("@Resultado", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(paramResultado);

                    connection.Open();
                    command.ExecuteNonQuery();

                    bool loginExitoso = (bool)paramResultado.Value;
                    return loginExitoso;
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
