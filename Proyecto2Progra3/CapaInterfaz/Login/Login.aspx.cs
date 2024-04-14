using Proyecto2Progra3.Interfaz;
using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace Proyecto2Progra3.CapaInterfaz.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string correoElectronico = txtUsername.Text;
            string passwordIngresada = txtPassword.Text;

            bool loginExitoso = LoginManager.ValidarLogin(correoElectronico, passwordIngresada);

            if (loginExitoso)
            {
                int idEmpleado = ObtenerIdEmpleado(correoElectronico); // Obtener el IdEmpleado del usuario
                if (idEmpleado > 0)
                {
                    Session["IdEmpleado"] = idEmpleado; // Almacenar el IdEmpleado en la sesión
                    Session["Correo"] = correoElectronico;
                    Session["nombreEmpleado"] = LoginManager.nombre;

                    if (LoginManager.imagenEmpleado != null)
                    {
                        string imagenBase64 = Convert.ToBase64String(LoginManager.imagenEmpleado);
                        Session["imagenEmpleado"] = string.Format("data:image/jpeg;base64,{0}", imagenBase64);
                    }

                    Response.Redirect("~/CapaInterfaz/Inicio/Inicio.aspx");
                }
                else
                {
                    ShowAlert("No se pudo obtener el IdEmpleado del usuario.");
                }
            }
            else
            {
                ShowAlert("Login fallido. Por favor, verifica tu correo y contraseña.");
            }
        }
        public static int ObtenerIdEmpleado(string correoElectronico)
        {
            int idEmpleado = 0;

            try
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT IdEmpleado FROM Usuario WHERE Correo = @Correo";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Correo", correoElectronico);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idEmpleado = Convert.ToInt32(reader["IdEmpleado"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                throw new Exception("Error al obtener el IdEmpleado del usuario: " + ex.Message);
            }

            return idEmpleado;
        }

        private void ShowAlert(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{message}');", true);
        }
    }
}
