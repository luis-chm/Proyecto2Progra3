using System;
using System.Data;
using System.Web.UI;
using Proyecto2Progra3.Clases;

namespace Proyecto2Progra3.Interfaz
{
    public partial class Empleado : System.Web.UI.Page
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public Empleado()
        {
            _empleadoRepository = new EmpleadoDAL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConsultarTodosEmpleados();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Clases.Empleado empleado = new Clases.Empleado
                {
                    Dni = txtDNI.Text,
                    Apellidos = txtApellidos.Text,
                    Nombres = txtNombres.Text,
                    Sexo = Convert.ToChar(ddlSexo.SelectedValue),
                    FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                    Direccion = txtDireccion.Text,
                    EstadoCivil = Convert.ToChar(txtEstadoCivil.Text[0]),
                    Imagen = fuImagen.HasFile ? fuImagen.FileBytes : null
                };

                int nuevoIdEmpleado = _empleadoRepository.AgregarEmpleado(empleado);
                ConsultarTodosEmpleados();
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Empleado agregado con éxito. ID del Empleado: " + nuevoIdEmpleado + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al agregar el empleado: " + ex.Message + "');", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idEmpleado = Convert.ToInt32(txtId.Text);
                _empleadoRepository.EliminarEmpleado(idEmpleado);
                ConsultarTodosEmpleados();
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Empleado eliminado con éxito.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al eliminar el empleado: " + ex.Message + "');", true);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Clases.Empleado empleado = new Clases.Empleado
                {
                    IdEmpleado = Convert.ToInt32(txtId.Text),
                    Dni = txtDNI.Text,
                    Apellidos = txtApellidos.Text,
                    Nombres = txtNombres.Text,
                    Sexo = Convert.ToChar(ddlSexo.SelectedValue),
                    FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                    Direccion = txtDireccion.Text,
                    EstadoCivil = Convert.ToChar(txtEstadoCivil.Text[0]),
                    Imagen = fuImagen.HasFile ? fuImagen.FileBytes : null
                };

                _empleadoRepository.ModificarEmpleado(empleado);
                LimpiarCampos();
                ConsultarTodosEmpleados();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Empleado modificado con éxito.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al modificar el empleado: " + ex.Message + "');", true);
            }
        }

        private void ConsultarTodosEmpleados()
        {
            gvEmpleados.DataSource = _empleadoRepository.ConsultarTodosEmpleados();
            gvEmpleados.DataBind();
        }

        protected void LimpiarCampos()
        {
            txtId.Text = string.Empty;
            txtDNI.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtNombres.Text = string.Empty;
            ddlSexo.SelectedIndex = 0;
            txtFechaNacimiento.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtEstadoCivil.Text = string.Empty;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int idEmpleado = Convert.ToInt32(txtId.Text);
                DataRow empleado = _empleadoRepository.ConsultarEmpleadoPorId(idEmpleado);
                if (empleado != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("IdEmpleado", typeof(int));
                    dt.Columns.Add("DNI", typeof(string));
                    dt.Columns.Add("Apellidos", typeof(string));
                    dt.Columns.Add("Nombres", typeof(string));
                    dt.Columns.Add("Sexo", typeof(string)); // Asegúrate de que el sexo sea un tipo de datos adecuado
                    dt.Columns.Add("FechaNacimiento", typeof(DateTime));
                    dt.Columns.Add("Direccion", typeof(string));
                    dt.Columns.Add("EstadoCivil", typeof(string)); // Asegúrate de que el estado civil sea un tipo de datos adecuado
                    dt.Columns.Add("Imagen", typeof(byte[])); // La columna de imagen se almacena como un arreglo de bytes

                    // Agrega el empleado encontrado al DataTable
                    dt.Rows.Add(empleado.ItemArray);

                    gvEmpleados.DataSource = dt;
                    gvEmpleados.DataBind();
                }
                else
                {
                    LimpiarCampos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se encontró ningún empleado con el ID especificado.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al buscar el empleado: " + ex.Message + "');", true);
            }
        }




    }
}
