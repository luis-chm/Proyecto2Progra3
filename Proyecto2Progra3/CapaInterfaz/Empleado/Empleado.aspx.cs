using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                LlenarCargos();
            }
        }
        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
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
                    EstadoCivil = Convert.ToChar(DDLEstadoCivil.SelectedValue),
                    Imagen = fuImagen.HasFile ? fuImagen.FileBytes : null,
                };

                // Obtener el valor seleccionado del campo IdCargo
                int idCargo;
                if (int.TryParse(dropCargo.SelectedValue, out idCargo))
                {
                    // Asignar el valor de IdCargo al objeto Empleado
                    empleado.IdCargo = idCargo;

                    // Llamar al método para agregar empleado
                    int nuevoIdEmpleado = _empleadoRepository.AgregarEmpleado(empleado);
                    ConsultarTodosEmpleados();
                    LimpiarCampos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Empleado agregado con éxito. ID del Empleado: " + nuevoIdEmpleado + "');", true);
                }
                else
                {
                    // Manejar el caso en que no se haya seleccionado ningún cargo
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Por favor, seleccione un cargo.');", true);
                }
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
                    EstadoCivil = Convert.ToChar(DDLEstadoCivil.SelectedValue),
                    Imagen = fuImagen.HasFile ? fuImagen.FileBytes : null
                };

                // Obtener el valor seleccionado del campo IdCargo
                int idCargo;
                if (int.TryParse(dropCargo.SelectedValue, out idCargo))
                {
                    // Asignar el valor de IdCargo al objeto Empleado
                    empleado.IdCargo = idCargo;

                    // Llamar al método para modificar empleado
                    _empleadoRepository.ModificarEmpleado(empleado);
                    ConsultarTodosEmpleados();
                    LimpiarCampos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Empleado modificado con éxito.');", true);
                }
                else
                {
                    // Manejar el caso en que no se haya seleccionado ningún cargo
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Por favor, seleccione un cargo.');", true);
                }
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
            DDLEstadoCivil.SelectedIndex = 0;
            dropCargo.SelectedIndex = 0; // Limpiar el DropDownList dropCargo
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
                    dt.Columns.Add("IdCargo", typeof(int));
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
        protected void LlenarCargos()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("select IdCargo, Descripcion from Cargo", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            //Crear un nuevo DataTable para agregar el elemento "Seleccione una categoría" al inicio
                            DataTable dtModified = new DataTable();
                            dtModified.Columns.Add("IdCargo");
                            dtModified.Columns.Add("TipoYDescripcion", typeof(string)); //Columna combinadale
                            dtModified.Rows.Add("", "Seleccione un cargo");

                            // Llenar el DataTable con datos combinados de IdCategoria y Descripcion
                            foreach (DataRow row in dt.Rows)
                            {
                                string IdCargo = row["IdCargo"].ToString();
                                string descripcion = row["Descripcion"].ToString();
                                string tipoYDescripcion = $"ID: {IdCargo} - Descripción: {descripcion}"; // Combinar los valores de IdCategoria y Descripcion
                                dtModified.Rows.Add(IdCargo, tipoYDescripcion);
                            }

                            //Enlazar el DropDownList con el nuevo DataTable modificado y la nueva columna combinada
                            dropCargo.DataSource = dtModified;
                            dropCargo.DataTextField = "TipoYDescripcion"; // Usar la nueva columna combinada
                            dropCargo.DataValueField = "IdCargo";
                            dropCargo.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
