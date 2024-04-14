using System;
using System.Web.UI;
using Proyecto2Progra3.Interfaz; 

namespace Proyecto2Progra3.CapaInterfaz.Registro
{
    public partial class Registro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string idEmpleado = txtIdEmpleado.Text;
            string correo = txtCorreo.Text;

            try
            {
                
                UsuarioManager usuarioManager = new UsuarioManager(idEmpleado, correo);

             
                usuarioManager.RegistrarUsuario();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Usuario registrado exitosamente.');", true);
            }
            catch (ArgumentException ex)
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error: {ex.Message}');", true);
            }
            catch (InvalidOperationException ex)
            {
             
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error: {ex.Message}');", true);
            }
            catch (Exception ex)
            {
              
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error: {ex.Message}');", true);
            }

            finally
            {
                //Limpiar campos de texto después de intentar registrar el usuario
                txtIdEmpleado.Text = "";
                txtCorreo.Text = "";
            }
        }

        protected void btnSolicitarToken_Click(object sender, EventArgs e)
        {
            string idEmpleado = txtIdEmpleado.Text;
            string correo = txtCorreo.Text;

            try
            {
                
                UsuarioManager usuarioManager = new UsuarioManager(idEmpleado, correo);

                
                usuarioManager.SolicitarNuevoToken();

           
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Se ha enviado un nuevo token al correo electrónico.');", true);
            }
            catch (ArgumentException ex)
            {
              
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error: {ex.Message}');", true);
            }
            catch (InvalidOperationException ex)
            {
               
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error: {ex.Message}');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error: {ex.Message}');", true);
            }
        }
    }
}
