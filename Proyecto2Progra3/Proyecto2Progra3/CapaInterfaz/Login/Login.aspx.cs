using System;
using System.Web.UI;

namespace Proyecto2Progra3.Interfaz
{
    public partial class Login : Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string correoElectronico = txtUsername.Text;
            string passwordIngresada = txtPassword.Text;

            bool loginExitoso = LoginManager.ValidarLogin(correoElectronico, passwordIngresada);

            if (loginExitoso)
            {
                // Redirigir a la página principal después del inicio de sesión exitoso
                Response.Redirect("~/CapaInterfaz/Principal/Principal.aspx");

            }
            else
            {
                ShowAlert("Login fallido. Por favor, verifica tu correo y contraseña.");
            }
        }

        private void ShowAlert(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{message}');", true);
        }
    }
}
