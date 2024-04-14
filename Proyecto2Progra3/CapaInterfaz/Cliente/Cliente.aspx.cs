using System;
using System.Data;
using System.Web.UI;
using Proyecto2Progra3.Clases;

namespace Proyecto2Progra3.CapaInterfaz.Cliente
{
    public partial class Cliente : Page
    {
        private readonly IClienteRepository _clienteRepository;

        public Cliente()
        {
            _clienteRepository = new ClienteDAL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Lógica para cargar los clientes en la interfaz
                CargarClientes();
            }
        }

        protected void CargarClientes()
        {
            gvClientes.DataSource = _clienteRepository.ConsultarTodosClientes();
            gvClientes.DataBind();
        }

        protected void LimpiarCampos()
        {
            txtIdCliente.Text = string.Empty;
            txtDNI.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            // Limpia otros campos de entrada si es necesario
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Clases.Cliente cliente = new Clases.Cliente
                {
                    Dni = txtDNI.Text,
                    Apellidos = txtApellidos.Text,
                    Nombres = txtNombres.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtTelefono.Text
                    // Asigna otros campos de entrada al objeto cliente
                };

                _clienteRepository.AgregarCliente(cliente);
                CargarClientes();
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cliente agregado con éxito.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error al agregar el cliente: {ex.Message}');", true);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = Convert.ToInt32(txtIdCliente.Text);
                Clases.Cliente cliente = _clienteRepository.ConsultarClientePorId(idCliente);
                if (cliente != null)
                {
                    cliente.Dni = txtDNI.Text;
                    cliente.Apellidos = txtApellidos.Text;
                    cliente.Nombres = txtNombres.Text;
                    cliente.Direccion = txtDireccion.Text;
                    cliente.Telefono = txtTelefono.Text;
                    // Asigna otros campos de entrada al objeto cliente
                    _clienteRepository.ModificarCliente(cliente);
                    CargarClientes();
                    LimpiarCampos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cliente modificado con éxito.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se encontró ningún cliente con el ID especificado.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error al modificar el cliente: {ex.Message}');", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = Convert.ToInt32(txtIdCliente.Text);
                _clienteRepository.EliminarCliente(idCliente);
                CargarClientes();
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cliente eliminado con éxito.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error al eliminar el cliente: {ex.Message}');", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = Convert.ToInt32(txtIdCliente.Text);
                Clases.Cliente cliente = _clienteRepository.ConsultarClientePorId(idCliente);
                if (cliente != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("IdCliente");
                    dt.Columns.Add("DNI");
                    dt.Columns.Add("Apellidos");
                    dt.Columns.Add("Nombres");
                    dt.Columns.Add("Direccion");
                    dt.Columns.Add("Telefono");

                    dt.Rows.Add(cliente.IdCliente, cliente.Dni, cliente.Apellidos, cliente.Nombres, cliente.Direccion, cliente.Telefono);

                    gvClientes.DataSource = dt;
                    gvClientes.DataBind();

                    LimpiarCampos();
                }
                else
                {
                    LimpiarCampos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se encontró ningún cliente con el ID especificado.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error al buscar el cliente: {ex.Message}');", true);
            }
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}
