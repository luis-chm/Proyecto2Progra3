using Proyecto2Progra3.CapaLogica;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto2Progra3.CapaInterfaz.ReportesVentas
{
    public partial class ReporteVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewDetallesVenta.DataSource = null;
                GridViewDetallesVenta.DataBind();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            mensajeError.Text = "";
            string valor = tbusqueda.Text.Trim();
            string tipo = DropDownTipo.SelectedValue;

            FacturacionDAL detalleVentaDAL = new FacturacionDAL();

            DataTable dtDetallesVenta = detalleVentaDAL.ObtenerDetallesVentaPorNumeroFactura(valor, tipo);


            GridViewDetallesVenta.DataSource = dtDetallesVenta;
            GridViewDetallesVenta.DataBind();

            if (dtDetallesVenta.Rows.Count == 0)
            {

                mensajeError.Text = "No se encuentra en la base de datos";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Producto no encontrado');", true);
            }
            tbusqueda.Text = "";

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }


        protected void tnumerofactura_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOpcionesBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownTipo.SelectedValue == "Fecha")
            {
                tbusqueda.TextMode = TextBoxMode.Date;
            }
            else
            {
                tbusqueda.TextMode = TextBoxMode.SingleLine;
            }
        }

        protected void tbusqueda_TextChanged(object sender, EventArgs e)
        {

        }

    }
}