using Proyecto2Progra3.CapaDatos;
using Proyecto2Progra3.CapaLogica;
using Proyecto2Progra3.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto2Progra3.CapaInterfaz.Cargo
{
    public partial class Cargo : System.Web.UI.Page
    {
        private readonly ICargoRepository _cargoRepository;
        public Cargo()
        {
            _cargoRepository = new CargoDAL();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarCargos();
        }
        private void CargarCargos()
        {
            gvCargo.DataSource = _cargoRepository.ConsultarTodosCargos();
            gvCargo.DataBind();
        }
        public void alertas(String texto)
        {
            string message = texto;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string descripcion = txtDescCargo.Text;
                int nuevoIdCargo = _cargoRepository.AgregarCargo(descripcion);
                CargarCargos();
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Categoría agregada con éxito. ID de Categoría: " + nuevoIdCargo + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al agregar el cargo: " + ex.Message + "');", true);
            }
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCargo = Convert.ToInt32(txtIdCargo.Text);
                var cargo = _cargoRepository.ConsultarCargoPorId(idCargo);
                if (cargo != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("IdCargo", typeof(int));
                    dt.Columns.Add("Descripcion", typeof(string));
                    dt.Rows.Add(cargo.IdCargo, cargo.Descripcion);

                    gvCargo.DataSource = dt;
                    gvCargo.DataBind();

                    LimpiarCampos();
                }
                else
                {
                    LimpiarCampos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se encontró ningún cargo con el ID especificado.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error al buscar el cargo: {ex.Message}');", true);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCargo = Convert.ToInt32(txtIdCargo.Text);
                string descripcion = txtDescCargo.Text;
                _cargoRepository.ModificarCargo(idCargo, descripcion);
                CargarCargos();
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cargo modificado con éxito.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al modificar el cargo: " + ex.Message + "');", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCargo = Convert.ToInt32(txtIdCargo.Text);
                _cargoRepository.EliminarCargo(idCargo);
                CargarCargos();
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cargo eliminado con éxito.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al eliminar el cargo: " + ex.Message + "');", true);
            }
        }
        protected void LimpiarCampos()
        {
            txtIdCargo.Text = string.Empty;
            txtDescCargo.Text = string.Empty;
        }

        protected void btnRefrescar1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}