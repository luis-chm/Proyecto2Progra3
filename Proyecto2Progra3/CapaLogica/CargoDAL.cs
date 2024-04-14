using Examen2progra.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Proyecto2Progra3.CapaDatos;
using System.Configuration;

namespace Proyecto2Progra3.CapaLogica
{
    public class CargoDAL : ICargoRepository
    {
        private readonly SqlConnection sqlConnection;

        public CargoDAL()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
        }
        public int AgregarCargo(string descripcion)
        {
            try
            {
                int newId = 0;
                using (SqlCommand command = new SqlCommand("[dbo].[spAgregarCargo]", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Descripcion", descripcion);

                    //Configura el parámetro de salida para capturar el nuevo ID
                    SqlParameter newIdParameter = new SqlParameter("@NewId", SqlDbType.Int);
                    newIdParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(newIdParameter);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();

                    // Obtener el nuevo ID generado por la base de datos
                    newId = Convert.ToInt32(newIdParameter.Value);
                }
                return newId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el cargo: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public Cargo ConsultarCargoPorId(int idCargo)
        {
            try
            {
                Cargo cargo = null;
                using (SqlCommand command = new SqlCommand("spConsultarCargo", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCargo", idCargo);

                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        cargo = new Cargo
                        {
                            IdCargo = Convert.ToInt32(reader["IdCargo"]),
                            Descripcion = reader["Descripcion"].ToString()
                        };
                    }
                }
                return cargo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar el cargo por ID: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void ModificarCargo(int idCargo, string descripcion)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("[dbo].[spModificarCargo]", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCargo", idCargo);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el cargo: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void EliminarCargo(int idCargo)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("[dbo].[spEliminarCargo]", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCargo", idCargo);

                    sqlConnection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró ningún cargo con el ID especificado.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cargo: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public DataTable ConsultarTodosCargos()
        {
            try
            {
                DataTable dtCargos = new DataTable();
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString))
                {
                    using (var command = new SqlCommand("spConsultarTodosLosCargos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtCargos);
                    }
                }
                return dtCargos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar todos los cargos: " + ex.Message);
            }
        }
    }
}