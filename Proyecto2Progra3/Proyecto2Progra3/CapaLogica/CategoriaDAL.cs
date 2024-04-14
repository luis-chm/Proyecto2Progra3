using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Proyecto2Progra3.Clases
{
    public class CategoriaDAL : ICategoriaRepository
    {
        private readonly SqlConnection sqlConnection;

        public CategoriaDAL()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
        }

        public int AgregarCategoria(string descripcion)
        {
            try
            {
                int newId = 0;
                using (SqlCommand command = new SqlCommand("[dbo].[AgregarCategoria]", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Descripcion", descripcion);

                    sqlConnection.Open();
                    newId = Convert.ToInt32(command.ExecuteScalar());
                }
                return newId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la categoría: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Categoria ConsultarCategoriaPorId(int idCategoria)
        {
            try
            {
                Categoria categoria = null;
                using (SqlCommand command = new SqlCommand("[dbo].[ConsultarCategoriaPorID]", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        categoria = new Categoria
                        {
                            IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                            Descripcion = reader["Descripcion"].ToString()
                        };
                    }
                }
                return categoria;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la categoría: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void ModificarCategoria(int idCategoria, string descripcion)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("[dbo].[ModificarCategoria]", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la categoría: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void EliminarCategoria(int idCategoria)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("[dbo].[EliminarCategoria]", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la categoría: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }



        public DataTable ConsultarTodasCategorias()
        {
            try
            {
                DataTable dtCategorias = new DataTable();
                using (SqlCommand command = new SqlCommand("SELECT IdCategoria, Descripcion FROM Categoria", sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtCategorias);
                }
                return dtCategorias;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar todas las categorías: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
