using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Proyecto2Progra3.Clases
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection sqlConnection;

        public ProductoRepository()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            sqlConnection = new SqlConnection(_connectionString);
        }

        public int AgregarProducto(Producto producto)
        {
            int newIdProducto = 0;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("AgregarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Marca", producto.Marca);
                    command.Parameters.AddWithValue("@Stock", producto.Stock);
                    command.Parameters.AddWithValue("@PrecioCompra", producto.PrecioCompra);
                    command.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                    command.Parameters.AddWithValue("@FechaVencimiento", producto.FechaVencimiento);
                    command.Parameters.AddWithValue("@Imagen", producto.Imagen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdEmpleado", producto.IdEmpleado);

                    connection.Open();
                    newIdProducto = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al agregar el producto: " + ex.Message);
            }

            return newIdProducto;
        }

        public void ModificarProducto(Producto producto)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("ModificarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    command.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Marca", producto.Marca);
                    command.Parameters.AddWithValue("@Stock", producto.Stock);
                    command.Parameters.AddWithValue("@PrecioCompra", producto.PrecioCompra);
                    command.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                    command.Parameters.AddWithValue("@FechaVencimiento", producto.FechaVencimiento);
                    command.Parameters.AddWithValue("@Imagen", producto.Imagen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdEmpleado", producto.IdEmpleado);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al modificar el producto: " + ex.Message);
            }
        }

        public void EliminarProducto(int idProducto, int idEmpleado)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("EliminarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdProducto", idProducto);
                    command.Parameters.AddWithValue("@IdEmpleado", idEmpleado); 

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar el producto: " + ex.Message);
            }
        }


        public Producto ConsultarProductoPorId(int idProducto)
        {
            Producto producto = null;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("ConsultarProductoPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdProducto", idProducto);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = new Producto
                            {
                                IdProducto = (int)reader["IdProducto"],
                                IdCategoria = (int)reader["IdCategoria"],
                                Nombre = reader["Nombre"].ToString(),
                                Marca = reader["Marca"].ToString(),
                                Stock = (int)reader["Stock"],
                                PrecioCompra = (decimal)reader["PrecioCompra"],
                                PrecioVenta = (decimal)reader["PrecioVenta"],
                                FechaVencimiento = (DateTime)reader["FechaVencimiento"],
                                Imagen = reader["Imagen"] != DBNull.Value ? (byte[])reader["Imagen"] : null,
                                IdEmpleado = (int)reader["IdEmpleado"],
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al consultar el producto por ID: " + ex.Message);
            }

            return producto;
        }

        public IEnumerable<Producto> ConsultarTodosProductos()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("ConsultarTodosLosProductos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = new Producto
                            {
                                IdProducto = reader["IdProducto"] != DBNull.Value ? (int)reader["IdProducto"] : 0,
                                IdCategoria = reader["IdCategoria"] != DBNull.Value ? (int)reader["IdCategoria"] : 0,
                                Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() : string.Empty,
                                Marca = reader["Marca"] != DBNull.Value ? reader["Marca"].ToString() : string.Empty,
                                Stock = reader["Stock"] != DBNull.Value ? (int)reader["Stock"] : 0,
                                PrecioCompra = reader["PrecioCompra"] != DBNull.Value ? (decimal)reader["PrecioCompra"] : 0,
                                PrecioVenta = reader["PrecioVenta"] != DBNull.Value ? (decimal)reader["PrecioVenta"] : 0,
                                FechaVencimiento = reader["FechaVencimiento"] != DBNull.Value ? (DateTime)reader["FechaVencimiento"] : DateTime.MinValue,
                                Imagen = reader["Imagen"] != DBNull.Value ? (byte[])reader["Imagen"] : null,
                                IdEmpleado = reader["IdEmpleado"] != DBNull.Value ? (int)reader["IdEmpleado"] : 0
                            };
                            productos.Add(producto);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al consultar todos los productos: " + ex.Message);
            }

            return productos;
        }
        public DataTable GetDataFromDatabaseProducto()
        {
            DataTable dtDetallesVenta = new DataTable();

            try
            {
                sqlConnection.Open();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Producto";
                    using (var command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable datatable = new DataTable();
                        adapter.Fill(datatable);
                        return datatable;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al descargar: " + ex.Message);
            }
        }
    }
}
