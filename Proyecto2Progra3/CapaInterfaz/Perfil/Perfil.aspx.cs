using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto2Progra3.CapaInterfaz.Perfil
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string correoUsuario = Session["Correo"].ToString();
                LlenarDatosEmpleadoPorCorreo(correoUsuario);
            }
        }
        protected void LlenarDatosEmpleadoPorCorreo(string correo)
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "ObtenerDatosEmpleadoPorCorreo";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Correo", correo);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Obtener los valores del empleado
                        string idEmpleado = reader["IdEmpleado"].ToString();
                        string dni = reader["DNI"].ToString();
                        string nombres = reader["Nombres"].ToString();
                        string apellidos = reader["Apellidos"].ToString();
                        string cargo = reader["Descripcion"].ToString();
                        string email = reader["Correo"].ToString();
                        string direccion = reader["Direccion"].ToString();

                        // Asignar los valores a los controles en la página
                        // Suponiendo que tienes los controles correspondientes en tu página
                        lCodigo.Text = idEmpleado;
                        LCedula.Text = dni;
                        lNombre.Text = nombres;
                        lbApellidos.Text = apellidos;
                        LCargo.Text = cargo;
                        lCorreo.Text = email;
                        lDireccion.Text = direccion;
                    }
                    else
                    {
                        // No se encontraron registros para el correo electrónico proporcionado
                    }

                    reader.Close();
                }
            }
        }
    }
}