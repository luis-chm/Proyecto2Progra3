using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Web;
using Proyecto2Progra3.CapaInterfaz.Usuario;

namespace Proyecto2Progra3.Interfaz
{
    public class UsuarioManager
    {
        private readonly SqlConnection sqlConnection;
        private Usuario usuario; 

        public UsuarioManager(string idEmpleado, string correo)
        {
            usuario = new Usuario { IdEmpleado = idEmpleado, Correo = correo };
        }
        public UsuarioManager()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
        }
        public void RegistrarUsuario()
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.IdEmpleado) || string.IsNullOrEmpty(usuario.Correo))
                {
                    throw new ArgumentException("El ID de empleado y el correo no pueden estar vacíos.");
                }

                if (CorreoExistente())
                {
                    throw new InvalidOperationException("El correo electrónico ya está registrado.");
                }

                string tokenActual = ObtenerTokenActual();

                if (TokenExpirado(tokenActual))
                {
                    DateTime tokenExpiracion;
                    tokenActual = GenerarTokenSeguro(out tokenExpiracion);
                    ActualizarToken(tokenActual, tokenExpiracion);
                }

                string claveTemporal = GenerarContrasenaTemporalSegura();
                GuardarUsuarioConToken(claveTemporal, tokenActual);

                EnviarCorreoBienvenida(claveTemporal, tokenActual);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar usuario: " + ex.Message);
                throw;
            }
        }

        public void SolicitarNuevoToken()
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.IdEmpleado) || string.IsNullOrEmpty(usuario.Correo))
                {
                    throw new ArgumentException("El ID de empleado y el correo no pueden estar vacíos.");
                }

                string tokenActual = ObtenerTokenActual();

                if (!TokenExpirado(tokenActual))
                {
                    throw new InvalidOperationException("El token actual aún no ha expirado.");
                }

                DateTime tokenExpiracion;
                tokenActual = GenerarTokenSeguro(out tokenExpiracion);
                ActualizarToken(tokenActual, tokenExpiracion);

                EnviarCorreoNuevoToken(tokenActual);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al solicitar nuevo token: " + ex.Message);
                throw;
            }
        }

        private string ObtenerTokenActual()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT TokenCambioContrasena FROM Usuario WHERE IdEmpleado = @IdEmpleado";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdEmpleado", usuario.IdEmpleado);
                    connection.Open();
                    return command.ExecuteScalar() as string;
                }
            }
        }

        private bool TokenExpirado(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return true;
            }

            DateTime expiracion;
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT TokenExpiracion FROM Usuario WHERE IdEmpleado = @IdEmpleado";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdEmpleado", usuario.IdEmpleado);
                    connection.Open();
                    expiracion = Convert.ToDateTime(command.ExecuteScalar());
                }
            }

            return expiracion < DateTime.UtcNow;
        }

        private void ActualizarToken(string nuevoToken, DateTime tokenExpiracion)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "UPDATE Usuario SET TokenCambioContrasena = @Token, TokenExpiracion = @Expiracion WHERE IdEmpleado = @IdEmpleado";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Token", nuevoToken);
                    command.Parameters.AddWithValue("@Expiracion", tokenExpiracion);
                    command.Parameters.AddWithValue("@IdEmpleado", usuario.IdEmpleado);
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
                    command.Parameters.AddWithValue("@Correo", usuario.Correo);
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
            string urlCambioContrasena = $"http://localhost:53860/CapaInterfaz/CambiarPassword/CambiarPassword.aspx?token={HttpUtility.UrlEncode(token)}";

            using (SmtpClient client = new SmtpClient("smtp.office365.com"))
            {
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("stevencamach1@hotmail.com", "ojos2011");
                client.EnableSsl = true;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("stevencamach1@hotmail.com"),
                    Subject = "Bienvenido al sistema.",
                    Body = $"¡Bienvenido al sistema!<br/><br/>" +
                           $"Por favor, cambia tu contraseña temporal:<br/><strong>{claveTemporal}</strong><br/><br/>" +
                           $"Haz clic <a href='{urlCambioContrasena}'>aquí</a> para cambiar tu contraseña.<br/><br/>" +
                           $"Atentamente,<br/>" +
                           $"[Soporte TI]",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(usuario.Correo);

                client.Send(mailMessage);
            }
        }

        private void EnviarCorreoNuevoToken(string nuevoToken)
        {
            string urlCambioContrasena = $"http://localhost:53860/CapaInterfaz/CambiarPassword/CambiarPassword.aspx?token={HttpUtility.UrlEncode(nuevoToken)}";

            using (SmtpClient client = new SmtpClient("smtp.office365.com"))
            {
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("stevencamach1@hotmail.com", "ojos2011");
                client.EnableSsl = true;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("stevencamach1@hotmail.com"),
                    Subject = "Nuevo token para cambiar contraseña.",
                    Body = $"Se ha generado un nuevo token para cambiar la contraseña.<br/><br/>" +
                           $"Haz clic <a href='{urlCambioContrasena}'>aquí</a> para cambiar tu contraseña.<br/><br/>" +
                           $"Atentamente,<br/>" +
                           $"[Soporte TI]",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(usuario.Correo);

                client.Send(mailMessage);
            }
        }

        private void GuardarUsuarioConToken(string claveHash, string token)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var spName = "spAgregarUsuarioConToken";
                using (var command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmpleado", usuario.IdEmpleado);
                    command.Parameters.AddWithValue("@Correo", usuario.Correo);
                    command.Parameters.AddWithValue("@Clave", claveHash);
                    command.Parameters.AddWithValue("@TokenCambioContrasena", token);

                    DateTime tokenExpiracion;
                    GenerarTokenSeguro(out tokenExpiracion);
                    command.Parameters.AddWithValue("@TokenExpiracion", tokenExpiracion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private string GenerarTokenSeguro(out DateTime tokenExpiracion)
        {
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int longitudToken = 64;

            StringBuilder tokenBuilder = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < longitudToken; i++)
            {
                tokenBuilder.Append(caracteresPermitidos[random.Next(caracteresPermitidos.Length)]);
            }

            tokenExpiracion = DateTime.UtcNow.AddHours(24);

            return tokenBuilder.ToString();
        }
        public DataTable GetDataFromDatabaseUsuario()
        {
            DataTable dtDetallesUsuario = new DataTable();
            try
            {
                sqlConnection.Open();
                string query = "SELECT * FROM Usuario";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    SqlCommand comamnad = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);
                    return datatable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al descargar: " + ex.Message);
            }
        }
        #region CATALOGO USUARIOS
        public static int AgregarUsuario(int nuevoIdEmpleado, string nuevoCorreo, string nuevaContrasena)
        {
            int retorno = 0;
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("spAgregarUsuarioFromCatalogo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NuevoIdEmpleado", nuevoIdEmpleado);
                        cmd.Parameters.AddWithValue("@NuevoCorreo", nuevoCorreo);
                        cmd.Parameters.AddWithValue("@NuevaContrasena", nuevaContrasena);

                        con.Open();
                        retorno = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción aquí o simplemente devolver -1
                retorno = -1;
                Console.WriteLine("ERROR: " + ex.Message);
            }

            return retorno;
        }
        public static DataTable ConsultarUsuario(int idUsuario)
        {
            DataTable dt = new DataTable();

            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "spConsultarUsuario";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dt.Columns.Add("IDUsuario");
                            dt.Columns.Add("IDEmpleado");
                            dt.Columns.Add("Correo");
                            dt.Columns.Add("Contrasena");
                            dt.Rows.Add(reader["IDUsuario"].ToString(), reader["IDEmpleado"].ToString(), reader["Correo"].ToString(), reader["Contrasena"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción aquí o simplemente devolver un DataTable vacío
                Console.WriteLine("ERROR: " + ex.Message);
            }

            return dt;
        }
        public static int ModificarUsuario(string idUsuario, string nuevoIdEmpleado, string nuevoCorreo, string nuevaContrasena)
        {
            int resultado = 0;
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "spModificarUsuario";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@NuevoIdEmpleado", nuevoIdEmpleado);
                        cmd.Parameters.AddWithValue("@NuevoCorreo", nuevoCorreo);
                        cmd.Parameters.AddWithValue("@NuevaContrasena", nuevaContrasena);

                        con.Open();
                        resultado = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción aquí o simplemente devolver -1
                resultado = -1;
                Console.WriteLine("ERROR: " + ex.Message);
            }

            return resultado;
        }
        public static int EliminarUsuario(string idUsuario)
        {
            int resultado = 0;
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "spEliminarUsuario";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        con.Open();
                        resultado = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción aquí o simplemente devolver -1
                resultado = -1;
                Console.WriteLine("ERROR: " + ex.Message);
            }

            return resultado;
        }
        #endregion FIN CAT USUARIOS
    }
}