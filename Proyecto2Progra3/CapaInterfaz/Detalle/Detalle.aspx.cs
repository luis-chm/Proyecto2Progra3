using Proyecto2Progra3.CapaLogica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto2Progra3.CapaInterfaz
{
    public partial class Detalle : System.Web.UI.Page
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

            string valor = tnumerofactura.Text.Trim();
            string tipo = "Factura";

            try
            {

                FacturacionDAL detalleVentaDAL = new FacturacionDAL();


                DataTable dtDetallesVenta = detalleVentaDAL.ObtenerDetallesVentaPorNumeroFactura(valor,tipo);

                GridViewDetallesVenta.DataSource = dtDetallesVenta;
                GridViewDetallesVenta.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write("Error al cargar los detalles de venta: " + ex.Message);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void tnumerofactura_TextChanged(object sender, EventArgs e)
        {

        }
    }
}