using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Proyecto2Progra3.Interfaz;

namespace Proyecto2Progra3.CapaInterfaz.Usuario
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();
                LlenarEmpleados();
            }
        }
        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            
        }
        protected void LlenarGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                string query = "SELECT IDUsuario, IDEmpleado, Correo, Contrasena FROM Usuario";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvUsuarios.DataSource = dt;
                            gvUsuarios.DataBind();
                        }
                    }
                }
            }
        }

        protected void LlenarEmpleados()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("select IdEmpleado, Nombres from Empleado", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);


                            DataTable dtModified = new DataTable();
                            dtModified.Columns.Add("IdEmpleado");
                            dtModified.Columns.Add("TipoYDescripcion", typeof(string));
                            dtModified.Rows.Add("", "Seleccione un empleado");


                            foreach (DataRow row in dt.Rows)
                            {
                                string idEmpleado = row["IdEmpleado"].ToString();
                                string Nombre = row["Nombres"].ToString();
                                string tipoYDescripcion = $"ID: {idEmpleado} - Nombre: {Nombre}";
                                dtModified.Rows.Add(idEmpleado, tipoYDescripcion);
                            }


                            dropIdEmpleado.DataSource = dtModified;
                            dropIdEmpleado.DataTextField = "TipoYDescripcion";
                            dropIdEmpleado.DataValueField = "IdEmpleado";
                            dropIdEmpleado.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void LimpiarCampos()
        {
            txtUsusarioID.Text = "";
            txtCorreo.Text = "";
            txtContrasena.Text = "";
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

        private void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{mensaje}');", true);
        }

        private void MostrarAlerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{mensaje}');", true);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int nuevoIdEmpleado = int.Parse(dropIdEmpleado.SelectedValue);
                string nuevoCorreo = txtCorreo.Text;
                string nuevaContrasena = ConvertirSHA256(txtContrasena.Text);

                int resultado = UsuarioManager.AgregarUsuario(nuevoIdEmpleado, nuevoCorreo, nuevaContrasena);

                if (resultado > 0)
                {
                    LlenarGrid();
                    LimpiarCampos();
                    MostrarMensaje("Usuario agregado correctamente.");
                }
                else
                {
                    MostrarMensaje("Error al agregar el usuario.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                MostrarMensaje("Error al agregar el usuario: " + ex.Message);
            }
        }
        protected void btnConsultar_Click1(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = int.Parse(txtUsusarioID.Text);

                DataTable dt = UsuarioManager.ConsultarUsuario(idUsuario);
                if (dt.Rows.Count > 0)
                {
                    gvUsuarios.DataSource = dt;
                    gvUsuarios.DataBind();
                    gvUsuarios.Visible = true;
                }
                else
                {
                    gvUsuarios.DataSource = null;
                    gvUsuarios.DataBind();
                    gvUsuarios.Visible = false;
                    MostrarAlerta("Usuario no encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                MostrarAlerta("Error al consultar el usuario: " + ex.Message);
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                string idUsuario = txtUsusarioID.Text;
                string nuevoIdEmpleado = dropIdEmpleado.SelectedValue;
                string nuevoCorreo = txtCorreo.Text;
                string nuevaContrasena = ConvertirSHA256(txtContrasena.Text);

                int resultado = UsuarioManager.ModificarUsuario(idUsuario, nuevoIdEmpleado, nuevoCorreo, nuevaContrasena);

                if (resultado > 0)
                {
                    LlenarGrid();
                    LimpiarCampos();
                    MostrarMensaje("Usuario modificado correctamente.");
                }
                else
                {
                    MostrarMensaje("Error al modificar el usuario.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                MostrarMensaje("Error al modificar el usuario: " + ex.Message);
            }
        }
        protected void btnEliminar_Click1(object sender, EventArgs e)
        {
            try
            {
                string idUsuario = txtUsusarioID.Text;

                int resultado = UsuarioManager.EliminarUsuario(idUsuario);

                if (resultado > 0)
                {
                    LlenarGrid();
                    LimpiarCampos();
                    MostrarMensaje("Usuario eliminado correctamente.");
                }
                else
                {
                    MostrarMensaje("No se encontró ningún usuario con ese ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                MostrarMensaje("Error al eliminar el usuario: " + ex.Message);
            }
        }
        protected void Reload_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CapaInterfaz/Usuario/Usuario.aspx");
        }
    }
}