using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Proyecto2Progra3.Interfaz
{
    public class UsuarioManager
    {
        public string IdEmpleado { get; set; }
        public string Correo { get; set; }

        public UsuarioManager(string idEmpleado, string correo)
        {
            IdEmpleado = idEmpleado;
            Correo = correo;
        }

        public void RegistrarUsuario()
        {
            try
            {
                // Verificar si el ID de empleado y el correo no están vacíos
                if (string.IsNullOrEmpty(IdEmpleado) || string.IsNullOrEmpty(Correo))
                {
                    throw new ArgumentException("El ID de empleado y el correo no pueden estar vacíos.");
                }

                // Verificar si el correo ya existe en la base de datos
                if (CorreoExistente())
                {
                    throw new InvalidOperationException("El correo electrónico ya está registrado.");
                }

                // Genera una contraseña temporal más segura.
                string claveTemporal = GenerarContrasenaTemporalSegura();

                // Genera un token seguro y establece su expiración.
                string token = GenerarTokenSeguro();
                DateTime tokenExpiracion = DateTime.UtcNow.AddHours(1);

                // Guarda el usuario en la base de datos con el token y la fecha de expiración.
                GuardarUsuarioConToken(claveTemporal, token, tokenExpiracion);

                // Envía la contraseña temporal por correo electrónico.
                EnviarCorreoBienvenida(claveTemporal, token);
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente (p. ej., registrarla, mostrar un mensaje de error, etc.)
                Console.WriteLine("Error al registrar usuario: " + ex.Message);
                throw; // Propagar la excepción para que sea manejada en la página
            }
        }

        private void GuardarUsuarioConToken(string claveHash, string token, DateTime tokenExpiracion)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var spName = "spAgregarUsuarioConToken";
                using (var command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                    command.Parameters.AddWithValue("@Correo", Correo);
                    command.Parameters.AddWithValue("@Clave", claveHash);
                    command.Parameters.AddWithValue("@TokenCambioContrasena", token);
                    command.Parameters.AddWithValue("@TokenExpiracion", tokenExpiracion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private bool CorreoExistente()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT COUNT(*) FROM Usuario WHERE Correo = @Correo";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Correo", Correo);
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private string GenerarContrasenaTemporalSegura()
        {
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            var random = new Random();
            var builder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                builder.Append(caracteresPermitidos[random.Next(caracteresPermitidos.Length)]);
            }
            return builder.ToString();
        }

        private void EnviarCorreoBienvenida(string claveTemporal, string token)
        {
            string urlCambioContrasena = $"http://localhost:53860/CapaInterfaz/CambiarPassword/CambiarContrasena.aspx?token={HttpUtility.UrlEncode(token)}";


            using (SmtpClient client = new SmtpClient("smtp.office365.com"))
            {
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("Stevencamach1@hotmail.com", "ojos2011");
                client.EnableSsl = true;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("Stevencamach1@hotmail.com"),
                    Subject = "Bienvenido al sistema",
                    Body = $"Por favor, cambia tu contraseña temporal: {claveTemporal} <br> Haciendo clic <a href='{urlCambioContrasena}'>aquí</a>.",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(Correo);

                client.Send(mailMessage);
            }
        }

        private string GenerarTokenSeguro()
        {
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int longitudToken = 64;

            StringBuilder tokenBuilder = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < longitudToken; i++)
            {
                tokenBuilder.Append(caracteresPermitidos[random.Next(caracteresPermitidos.Length)]);
            }

            return tokenBuilder.ToString();
        }
    }
}
