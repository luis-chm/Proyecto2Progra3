using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Proyecto2Progra3.Clases;

namespace Proyecto2Progra3.Interfaz
{
    public partial class Categoria : Page
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public Categoria()
        {
            _categoriaRepository = new CategoriaDAL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            gvCategorias.DataSource = _categoriaRepository.ConsultarTodasCategorias();
            gvCategorias.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string descripcion = txtDescripcion.Text;
                int nuevoIdCategoria = _categoriaRepository.AgregarCategoria(descripcion);
                CargarCategorias();
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Categoría agregada con éxito. ID de Categoría: " + nuevoIdCategoria + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al agregar la categoría: " + ex.Message + "');", true);
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCategoria = Convert.ToInt32(txtIdCategoria.Text);
                Proyecto2Progra3.Clases.Categoria categoria = _categoriaRepository.ConsultarCategoriaPorId(idCategoria);
                if (categoria != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("IdCategoria", typeof(int));
                    dt.Columns.Add("Descripcion", typeof(string));

                    // Agregar la fila con los datos de la categoría consultada
                    dt.Rows.Add(categoria.IdCategoria, categoria.Descripcion);

                    // Asignar el DataTable como origen de datos del GridView
                    gvCategorias.DataSource = dt;
                    gvCategorias.DataBind();
                }
                else
                {
                    LimpiarCampos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se encontró ninguna categoría con el ID especificado.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al consultar la categoría: " + ex.Message + "');", true);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCategoria = Convert.ToInt32(txtIdCategoria.Text);
                string descripcion = txtDescripcion.Text;
                _categoriaRepository.ModificarCategoria(idCategoria, descripcion);
                CargarCategorias();
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Categoría modificada con éxito.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al modificar la categoría: " + ex.Message + "');", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCategoria = Convert.ToInt32(txtIdCategoria.Text);

                // Verificar si la categoría tiene productos asociados
                if (CategoriaTieneProductos(idCategoria))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede eliminar la categoría porque tiene productos asociados.');", true);
                }
                else
                {
                    // Si no hay productos asociados, eliminar la categoría
                    _categoriaRepository.EliminarCategoria(idCategoria);
                    CargarCategorias(); // Método para recargar las categorías en el GridView
                    LimpiarCampos(); // Método para limpiar los campos después de eliminar
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Categoría eliminada con éxito.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al eliminar la categoría: " + ex.Message + "');", true);
            }
        }

        // Método para verificar si la categoría tiene productos asociados
        private bool CategoriaTieneProductos(int idCategoria)
        {
            // Lógica para verificar si la categoría tiene productos asociados en la base de datos
            // Supongamos que existe una tabla Producto con una columna IdCategoria
            // Podemos hacer una consulta SQL para contar la cantidad de productos asociados a la categoría
            string query = "SELECT COUNT(*) FROM Producto WHERE IdCategoria = @IdCategoria";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        protected void LimpiarCampos()
        {
            txtIdCategoria.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}
