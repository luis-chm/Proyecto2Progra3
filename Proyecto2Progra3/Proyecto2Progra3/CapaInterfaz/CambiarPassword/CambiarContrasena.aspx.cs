using System;
using System.Web.UI;

namespace Proyecto2Progra3.Interfaz
{
    public partial class CambiarContrasena : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["token"]))
            {
                string token = Server.UrlDecode(Request.QueryString["token"]);

                if (txtNuevaContrasena.Text == txtConfirmarContrasena.Text)
                {
                    CambioContrasenaManager manager = new CambioContrasenaManager(token, txtNuevaContrasena.Text);
                    bool cambioExitoso = manager.CambiarContrasena();

                    if (cambioExitoso)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Contraseña cambiada con éxito.'); window.location = 'http://localhost:53860/CapaInterfaz/Login/Login.aspx';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se ha podido cambiar la contraseña. El token es inválido.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Las contraseñas no coinciden.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se proporcionó un token válido en la URL.');", true);
            }
        }
    }
}
