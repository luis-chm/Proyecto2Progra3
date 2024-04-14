using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Proyecto2Progra3.Clases
{
    public class ClienteDAL : IClienteRepository
    {
        private readonly SqlConnection sqlConnection;

        public ClienteDAL()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
        }

        public int AgregarCliente(Cliente cliente)
        {
            try
            {
                int newId = 0;
                using (SqlCommand command = new SqlCommand("AgregarCliente", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DNI", cliente.Dni);
                    command.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                    command.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                    command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                    sqlConnection.Open();
                    newId = Convert.ToInt32(command.ExecuteScalar());
                }
                return newId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el cliente: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Cliente ConsultarClientePorId(int idCliente)
        {
            try
            {
                Cliente cliente = null;
                using (SqlCommand command = new SqlCommand("ConsultarClientePorId", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCliente", idCliente);

                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            IdCliente = Convert.ToInt32(reader["IdCliente"]),
                            Dni = reader["DNI"].ToString(),
                            Apellidos = reader["Apellidos"].ToString(),
                            Nombres = reader["Nombres"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString()
                        };
                    }
                }
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar el cliente: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public DataTable ConsultarTodosClientes()
        {
            try
            {
                DataTable dtClientes = new DataTable();
                using (SqlCommand command = new SqlCommand("ConsultarTodosClientes", sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtClientes);
                }
                return dtClientes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar todos los clientes: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void ModificarCliente(Cliente cliente)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("ModificarCliente", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                    command.Parameters.AddWithValue("@DNI", cliente.Dni);
                    command.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                    command.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                    command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el cliente: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void EliminarCliente(int idCliente)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("EliminarCliente", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCliente", idCliente);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public DataTable GetDataFromDatabaseCliente()
        {
            DataTable dtDetallesCliente = new DataTable();

            try
            {
                sqlConnection.Open();


                string query = "SELECT * FROM Cliente";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    SqlCommand comamnad = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);
                    return datatable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al descargar: " + ex.Message);
            }
        }
    }
}
