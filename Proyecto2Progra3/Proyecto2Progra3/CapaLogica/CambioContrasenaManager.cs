using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto2Progra3.Interfaz
{
    public class CambioContrasenaManager
    {
        public string Token { get; set; }
        public string NuevaContrasena { get; set; }

        public CambioContrasenaManager(string token, string nuevaContrasena)
        {
            Token = token;
            NuevaContrasena = nuevaContrasena;
        }

        public bool CambiarContrasena()
        {
            bool cambioExitoso = false;

            // Genera el hash de la nueva contraseña.
            string nuevaContrasenaHash = ConvertirSHA256(NuevaContrasena);

            // Obtener el correo asociado al token
            string correo = ObtenerCorreoPorToken(Token);

            // Verificar si se obtuvo un correo válido
            if (!string.IsNullOrEmpty(correo))
            {
                // Establece la cadena de conexión.
                string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Verifica si la conexión se abrió correctamente
                        if (connection.State == ConnectionState.Open)
                        {
                            using (var command = new SqlCommand("spCambiarContrasenaPorToken", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Correo", correo); // Pasar el correo obtenido
                                command.Parameters.AddWithValue("@NuevaContrasenaHash", nuevaContrasenaHash);

                                // Ejecuta el procedimiento almacenado y recupera el resultado
                                int resultado = Convert.ToInt32(command.ExecuteScalar());

                                // Verifica el resultado para determinar si la contraseña se cambió con éxito
                                if (resultado == 1)
                                {
                                    // Envía la confirmación por correo electrónico
                                    EnviarConfirmacionCorreo(correo);
                                    cambioExitoso = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Maneja cualquier error y registra el mensaje
                    Console.WriteLine("Error al cambiar la contraseña: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("No se pudo obtener el correo asociado al token.");
            }

            return cambioExitoso;
        }

        private void EnviarConfirmacionCorreo(string correoElectronico)
        {
            // Configura aquí tu cliente SMTP y tus credenciales.
            SmtpClient client = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("stevencamach1@hotmail.com", "ojos2011"),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("stevencamach1@hotmail.com"),
                Subject = "Confirmación de cambio de contraseña",
                Body = "Tu contraseña ha sido cambiada con éxito.",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(correoElectronico);

            client.Send(mailMessage);
        }

        private string ObtenerCorreoPorToken(string token)
        {
            string correo = "";

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        string query = "SELECT Correo FROM Usuario WHERE TokenCambioContrasena = @Token";

                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Token", token);
                            object result = command.ExecuteScalar();

                            if (result != null)
                            {
                                correo = result.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente (p. ej., registrarla, mostrar un mensaje de error, etc.)
                // Aquí se puede incluir código para manejar la excepción
                Console.WriteLine("Error al obtener el correo asociado al token: " + ex.Message);
            }

            return correo;
        }

        private string ConvertirSHA256(string input)
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
