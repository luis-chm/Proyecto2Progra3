using System;
using System.Web.UI;

namespace Proyecto2Progra3.Interfaz
{
    public partial class Registro : Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string idEmpleado = txtIdEmpleado.Text;
            string correo = txtCorreo.Text;

            try
            {
                UsuarioManager manager = new UsuarioManager(idEmpleado, correo);
                manager.RegistrarUsuario();

                // Registro exitoso, mostrar mensaje de alerta
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Registro exitoso. Por favor, revisa tu correo electrónico.');", true);
            }
            catch (ArgumentException ex)
            {
                // ID de empleado o correo vacío, mostrar mensaje de alerta
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error: {ex.Message}');", true);
            }
            catch (InvalidOperationException ex)
            {
                // Correo ya registrado, mostrar mensaje de alerta
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error: {ex.Message}');", true);
            }
            catch (Exception ex)
            {
                // Ocurrió otro error, mostrar mensaje de alerta
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error al registrar usuario: {ex.Message}');", true);
            }
        }
    }
}
