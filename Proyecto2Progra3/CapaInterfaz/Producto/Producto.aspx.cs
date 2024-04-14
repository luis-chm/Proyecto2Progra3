using Proyecto2Progra3.Clases;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Proyecto2Progra3
{
    public partial class Producto : System.Web.UI.Page
    {
        private readonly IProductoRepository _productoRepository;

        public Producto()
        {
            _productoRepository = new ProductoRepository();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConsultarTodosProductos();
                LlenarCategorias();
                LlenarEmpleados();
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
              
                int idEmpleado = Convert.ToInt32(Session["IdEmpleado"]);

               
                if (idEmpleado <= 0)
                {
                    throw new Exception("No se pudo obtener el IdEmpleado del usuario.");
                }

                
                Clases.Producto producto = new Clases.Producto
                {
                    IdCategoria = Convert.ToInt32(dropCategorias.SelectedValue),
                    Nombre = txtNombre.Text,
                    Marca = txtMarca.Text,
                    Stock = Convert.ToInt32(txtStock.Text),
                    PrecioCompra = Convert.ToDecimal(txtPrecioCompra.Text),
                    PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text),
                    FechaVencimiento = Convert.ToDateTime(txtFechaVencimiento.Text),
                    Imagen = fuImagen.HasFile ? fuImagen.FileBytes : null,
                    IdEmpleado = idEmpleado 
                };

              
                int nuevoIdProducto = _productoRepository.AgregarProducto(producto);

               
                ConsultarTodosProductos();

              
                LimpiarCampos();

                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Producto agregado con éxito. ID del Producto: " + nuevoIdProducto + "');", true);
            }
            catch (Exception ex)
            {
            
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al agregar el producto: " + ex.Message + "');", true);
            }
        }


        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = Convert.ToInt32(txtIdProducto.Text);

                
                int idEmpleado = Convert.ToInt32(Session["IdEmpleado"]);

               
                if (idEmpleado <= 0)
                {
                    throw new Exception("No se pudo obtener el IdEmpleado del usuario.");
                }

              
                _productoRepository.EliminarProducto(idProducto,idEmpleado);

                
                ConsultarTodosProductos();

            
                LimpiarCampos();

                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Producto eliminado con éxito.');", true);
            }
            catch (Exception ex)
            {
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al eliminar el producto: " + ex.Message + "');", true);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Clases.Producto producto = new Clases.Producto
                {
                    IdProducto = Convert.ToInt32(txtIdProducto.Text),
                    IdCategoria = Convert.ToInt32(dropCategorias.SelectedValue),
                    IdEmpleado = Convert.ToInt32(dropIdEmpleado.SelectedValue),
                    Nombre = txtNombre.Text,
                    Marca = txtMarca.Text,
                    Stock = Convert.ToInt32(txtStock.Text),
                    PrecioCompra = Convert.ToDecimal(txtPrecioCompra.Text),
                    PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text),
                    FechaVencimiento = Convert.ToDateTime(txtFechaVencimiento.Text),
                    Imagen = fuImagen.HasFile ? fuImagen.FileBytes : null
                };

                _productoRepository.ModificarProducto(producto);
                ConsultarTodosProductos();
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Producto modificado con éxito.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al modificar el producto: " + ex.Message + "');", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = Convert.ToInt32(txtIdProducto.Text);
                Clases.Producto producto = _productoRepository.ConsultarProductoPorId(idProducto);
                if (producto != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("IdProducto", typeof(int));
                    dt.Columns.Add("IdCategoria", typeof(int));
                    dt.Columns.Add("Nombre", typeof(string));
                    dt.Columns.Add("Marca", typeof(string));
                    dt.Columns.Add("Stock", typeof(int));
                    dt.Columns.Add("PrecioCompra", typeof(decimal));
                    dt.Columns.Add("PrecioVenta", typeof(decimal));
                    dt.Columns.Add("FechaVencimiento", typeof(DateTime));
                    dt.Columns.Add("Imagen", typeof(byte[]));
                    dt.Columns.Add("IdEmpleado", typeof(int));
                    dt.Rows.Add(producto.IdProducto, producto.IdCategoria, producto.Nombre, producto.Marca,
                        producto.Stock, producto.PrecioCompra, producto.PrecioVenta, producto.FechaVencimiento,
                        producto.Imagen,producto.IdEmpleado);

                    gvProductos.DataSource = dt;
                    gvProductos.DataBind();
                }
                else
                {
                    LimpiarCampos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se encontró ningún producto con el ID especificado.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al buscar el producto: " + ex.Message + "');", true);
            }
        }

        private void ConsultarTodosProductos()
        {
            gvProductos.DataSource = _productoRepository.ConsultarTodosProductos();
            gvProductos.DataBind();
        }

        protected void LimpiarCampos()
        {
            txtIdProducto.Text = string.Empty;
            dropCategorias.SelectedValue = string.Empty;
            txtNombre.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtPrecioCompra.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            txtFechaVencimiento.Text = string.Empty;
            dropIdEmpleado.SelectedValue = string.Empty;
        }
        protected void LlenarCategorias()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("select IdCategoria, Descripcion from Categoria", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                         
                            DataTable dtModified = new DataTable();
                            dtModified.Columns.Add("IdCategoria");
                            dtModified.Columns.Add("TipoYDescripcion", typeof(string)); 
                            dtModified.Rows.Add("", "Seleccione una categoría");

                           
                            foreach (DataRow row in dt.Rows)
                            {
                                string idCategoria = row["IdCategoria"].ToString();
                                string descripcion = row["Descripcion"].ToString();
                                string tipoYDescripcion = $"ID: {idCategoria} - Descripción: {descripcion}";
                                dtModified.Rows.Add(idCategoria, tipoYDescripcion);
                            }


                            dropCategorias.DataSource = dtModified;
                            dropCategorias.DataTextField = "TipoYDescripcion"; 
                            dropCategorias.DataValueField = "IdCategoria";
                            dropCategorias.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
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
    }
}
