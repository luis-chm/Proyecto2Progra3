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

           
            string nuevaContrasenaHash = ConvertirSHA256(NuevaContrasena);

            string correo = ObtenerCorreoPorToken(Token);

            
            if (!string.IsNullOrEmpty(correo))
            {
                
                string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        if (connection.State == ConnectionState.Open)
                        {
                            using (var command = new SqlCommand("spCambiarContrasenaPorToken", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Correo", correo);
                                command.Parameters.AddWithValue("@NuevaContrasenaHash", nuevaContrasenaHash);

                                
                                int resultado = Convert.ToInt32(command.ExecuteScalar());

                                
                                if (resultado == 1)
                                {
                                    
                                    EnviarConfirmacionCorreo(correo);
                                    cambioExitoso = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                  
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
            
            SmtpClient client = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("NotificacionToken@outlook.com", "*i,5ScZrn8%P-d3"),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("NotificacionToken@outlook.com"),
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
                        using (var command = new SqlCommand("ObtenerCorreoPorToken", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
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
