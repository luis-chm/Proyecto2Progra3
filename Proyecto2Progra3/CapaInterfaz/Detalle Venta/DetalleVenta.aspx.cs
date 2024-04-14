using Proyecto2Progra3.CapaDatos;
using Proyecto2Progra3.CapaLogica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Proyecto2Progra3.Clases;
using System.Drawing.Drawing2D;

namespace Proyecto2Progra3.CapaInterfaz.Detalle_Venta
{
    public partial class DetalleVenta : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Fecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                Ttipo.Text = "F";
                tcantidad.Text = "0";
                tStock.Text = "0";
                string serie = GenerarSerieAleatoria(5);
                Tserie.Text = serie;

                Random rnd = new Random();
                tnumerofactura.Text = rnd.Next(10000, 99999).ToString();
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Codigo"), new DataColumn("Nombre"), new DataColumn("Marca"), new DataColumn("cantidad"), new DataColumn("Precio"), new DataColumn("Subtotal") });
                ViewState["Factura"] = dt;
                this.BindGrid();


                if (Session["FacturaExitosa"] != null && (bool)Session["FacturaExitosa"])
                {
                    // Mostrar la alerta de factura ingresada con éxito
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Factura ingresada con éxito');", true);

                    // Limpiar la variable de sesión
                    Session["FacturaExitosa"] = false;
                }
            }
        }

        protected void BindGrid()
        {
            GridView1.DataSource = (DataTable)ViewState["Factura"];
            GridView1.DataBind();
        }

        private string GenerarSerieAleatoria(int longitud)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Caracteres permitidos para la serie
            Random rnd = new Random();
            char[] serie = new char[longitud];

            // Generar caracteres aleatorios para la serie
            for (int i = 0; i < longitud; i++)
            {
                serie[i] = caracteres[rnd.Next(caracteres.Length)];
            }

            return new string(serie);
        }
        protected void DropDownTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ttipo.Text = DropDownTipo.SelectedItem.Value;
        }

        protected void tnombre_TextChanged(object sender, EventArgs e)
        {

        }
        protected void tcantidad_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tcantidad.Text, out int cantidad))
            {
                if (cantidad < 0 && int.Parse(tcantidad.Text) > int.Parse(tStock.Text))
                {
                    mensajeError.Text = "Por favor, ingrese una Cantidad Correcta.";
                    tcantidad.Text = "0";
                }
                else
                {
                    mensajeError.Text = "";
                }
            }
            else
            {
                mensajeError.Text = "Por favor, ingrese un número válido.";
                tcantidad.Text = "0";
            }
        }
        protected void tprecio_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tnombrecliente_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tfecha_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tnumerofactura_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ttipo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tcedula_TextChanged(object sender, EventArgs e)
        {

        }
        protected void tnombreEmpleado_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                FacturacionDAL facturacionDAL = new FacturacionDAL();
                Facturacion factura = new Facturacion();

                factura.IdEmpleado = int.Parse(Tcodigoempleado.Text);
                factura.IdCliente = int.Parse(tcodigocliente.Text);
                factura.Serie = Tserie.Text;
                factura.NumeroDocumento = tnumerofactura.Text;
                factura.TipoDocumento = Ttipo.Text;
                factura.Total = float.Parse(LTOTAL.Text);

                int resultadoAgregarMaestro = facturacionDAL.AgregarMaestroFactura(factura);

                if (resultadoAgregarMaestro > 0)
                {
                    int linea = 0;
                    foreach (GridViewRow item in GridView1.Rows)
                    {

                        int codigo = int.Parse(item.Cells[0].Text);
                        int cantidad = int.Parse(item.Cells[3].Text);
                        float precio = float.Parse(item.Cells[4].Text);
                        float subtotal = float.Parse(item.Cells[5].Text);

                        ProductoRepository productoRepo = new ProductoRepository();
                        Proyecto2Progra3.Clases.Producto productoEncontrado = productoRepo.ConsultarProductoPorId(codigo);
                        int stock = productoEncontrado.Stock - cantidad;

                        linea++;
                        if (facturacionDAL.AgregarDetalleFactura(linea, codigo, cantidad, precio, subtotal, stock) > 0)
                        {

                        }

                    }

                    // Limpiar el GridView
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    limpiar();
                    Session["FacturaExitosa"] = true;
                    Response.Redirect(Request.Url.AbsolutePath);// Redirigir a la misma página para refrescar
                }         
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al Facturar: " + ex.Message + "');", true);
            }
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (int.Parse(tcantidad.Text) > 0 && int.Parse(tcantidad.Text) <= int.Parse(tStock.Text))
            {
                try
                {
                    DataTable dt = (DataTable)ViewState["Factura"];
                    float sb = (float.Parse(tcantidad.Text) * float.Parse(tprecio.Text));
                    ViewState["Subtotal"] = (float.Parse(tcantidad.Text) * float.Parse(tprecio.Text));
                    dt.Rows.Add(tcodigo.Text.Trim(), tnombre.Text.Trim(), tmarca.Text.Trim(), tcantidad.Text.Trim(), tprecio.Text.Trim(), ViewState["Subtotal"]);
                    ViewState["Factura"] = dt;
                    this.BindGrid();

                    ViewState["subtotal"] = (float.Parse(LSB.Text) + sb);
                    LSB.Text = (ViewState["subtotal"]).ToString();
                    ViewState["IVA"] = (float.Parse(LSB.Text) * 0.13);
                    LIVA.Text = (ViewState["IVA"]).ToString();
                    ViewState["total"] = (float.Parse(LSB.Text) + float.Parse(LIVA.Text));
                    LTOTAL.Text = (ViewState["total"]).ToString();

                    tcodigo.Focus();
                    tcodigo.Text = "";
                    tnombre.Text = "";
                    tcantidad.Text = "0";
                    tprecio.Text = "";
                    tmarca.Text = "";
                    tStock.Text = "";
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR");

                }
                finally
                {

                }
            }
        }
        protected void tcodigoempleado_TextChanged(object sender, EventArgs e)
        {
            EmpleadoDAL empleado = new EmpleadoDAL();

            Proyecto2Progra3.Clases.Empleado empleadoEncontrado = empleado.BuscarEmpleadoPorId(int.Parse(Tcodigoempleado.Text));

            if (empleadoEncontrado != null)
            {

                TnombreEmpleado.Text = empleadoEncontrado.Nombres;
                tcodigocliente.Focus();
                mensajeError.Text = "";
            }

            else
            {
                TnombreEmpleado.Text = "";
                mensajeError.Text = "Empleado no encontrado";
                Tcodigoempleado.Focus();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Empleado no encontrado');", true);
            }
        }
        protected void tcodigocliente_TextChanged(object sender, EventArgs e)
        {
            try
            {

                ClienteDAL cliente = new ClienteDAL();

                Proyecto2Progra3.Clases.Cliente clienteEncontrado = cliente.ConsultarClientePorId(int.Parse(tcodigocliente.Text));

                if (clienteEncontrado != null)
                {

                    tnombrecliente.Text = clienteEncontrado.Nombres;
                    tcedula.Text = clienteEncontrado.Dni;
                    tcodigo.Focus();
                    mensajeError.Text = "";
                }

                else
                {
                    tnombrecliente.Text = "";
                    tcedula.Text = "";
                    mensajeError.Text = "Cliente no encontrado";
                    tcodigocliente.Focus();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cliente no encontrado');", true);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error al buscar el cliente: {ex.Message}');", true);
            }
        }
        protected void tcodigo_TextChanged(object sender, EventArgs e)
        {
            ProductoRepository productoRepo = new ProductoRepository();

            Proyecto2Progra3.Clases.Producto productoEncontrado = productoRepo.ConsultarProductoPorId(int.Parse(tcodigo.Text));

            if (productoEncontrado != null)
            {

                tnombre.Text = productoEncontrado.Nombre;
                tprecio.Text = productoEncontrado.PrecioVenta.ToString();
                tmarca.Text = productoEncontrado.Marca.ToString();
                tStock.Text = productoEncontrado.Stock.ToString();
                tcantidad.Focus();
                mensajeError.Text = "";
            }

            else
            {
                tnombre.Text = "";
                tprecio.Text = "";
                tcantidad.Text = "0";
                tmarca.Text = "";
                tStock.Text = "0";
                mensajeError.Text = "Producto no encontrado";
                tcodigo.Focus();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Producto no encontrado');", true);
            }
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        public void limpiar()
        {
            Tcodigoempleado.Text = "";
            tcodigocliente.Text = "";
            tcodigo.Text = "";
            TnombreEmpleado.Text = "";
            tnombrecliente.Text = "";
            tnombre.Text = "";
            tcedula.Text = "";
            tcantidad.Text = "0";
            tStock.Text = "0";
            tmarca.Text = "";
            tprecio.Text = "";
            LSB.Text = "0";
            LIVA.Text = "0";
            LTOTAL.Text = "0";
        }
    }
}