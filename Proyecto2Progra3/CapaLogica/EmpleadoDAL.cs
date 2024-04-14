using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Proyecto2Progra3.Interfaz;

namespace Proyecto2Progra3.Clases
{
    public class EmpleadoDAL : IEmpleadoRepository
    {
        private readonly SqlConnection sqlConnection;

        public EmpleadoDAL()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
        }

        public int AgregarEmpleado(Empleado empleado)
        {
            int newId = 0;
            using (SqlCommand command = new SqlCommand("[dbo].[AgregarEmpleado]", sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
                command.Parameters.AddWithValue("@DNI", empleado.Dni);
                command.Parameters.AddWithValue("@Apellidos", empleado.Apellidos);
                command.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                command.Parameters.AddWithValue("@Sexo", empleado.Sexo);
                command.Parameters.AddWithValue("@FechaNacimiento", empleado.FechaNacimiento);
                command.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                command.Parameters.AddWithValue("@EstadoCivil", empleado.EstadoCivil);
                command.Parameters.AddWithValue("@IdCargo", empleado.IdCargo);
                // Configura el parámetro de imagen como SqlDbType.VarBinary
                SqlParameter imagenParameter = new SqlParameter("@Imagen", SqlDbType.VarBinary);
                imagenParameter.Value = (object)empleado.Imagen ?? DBNull.Value;
                command.Parameters.Add(imagenParameter);

                sqlConnection.Open();
                newId = Convert.ToInt32(command.ExecuteScalar());
                sqlConnection.Close();
            }
            return newId;
        }

        public void ModificarEmpleado(Empleado empleado)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("ModificarEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdEmpleado", empleado.IdEmpleado);
                    command.Parameters.AddWithValue("@DNI", empleado.Dni);
                    command.Parameters.AddWithValue("@Apellidos", empleado.Apellidos);
                    command.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                    command.Parameters.AddWithValue("@Sexo", empleado.Sexo);
                    command.Parameters.AddWithValue("@FechaNacimiento", empleado.FechaNacimiento);
                    command.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                    command.Parameters.AddWithValue("@EstadoCivil", empleado.EstadoCivil);
                    command.Parameters.AddWithValue("@Imagen", empleado.Imagen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdCargo", empleado.IdCargo); // Nuevo parámetro para el ID del cargo

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarEmpleado(int idEmpleado)
        {
            using (SqlCommand command = new SqlCommand("[dbo].[EliminarEmpleado]", sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public DataTable ConsultarTodosEmpleados()
        {
            DataTable dtEmpleados = new DataTable();
            using (SqlCommand command = new SqlCommand("[dbo].[ConsultarTodosEmpleados]", sqlConnection))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dtEmpleados);
            }
            return dtEmpleados;
        }

        public DataRow ConsultarEmpleadoPorId(int idEmpleado)
        {
            DataTable dtEmpleado = new DataTable();
            dtEmpleado.Columns.Add("IdEmpleado", typeof(int));
            dtEmpleado.Columns.Add("IdCargo", typeof(int));
            dtEmpleado.Columns.Add("DNI", typeof(string));
            dtEmpleado.Columns.Add("Apellidos", typeof(string));
            dtEmpleado.Columns.Add("Nombres", typeof(string));
            dtEmpleado.Columns.Add("Sexo", typeof(string)); // Asegúrate de que el sexo sea un tipo de datos adecuado
            dtEmpleado.Columns.Add("FechaNacimiento", typeof(DateTime));
            dtEmpleado.Columns.Add("Direccion", typeof(string));
            dtEmpleado.Columns.Add("EstadoCivil", typeof(string)); // Asegúrate de que el estado civil sea un tipo de datos adecuado
            dtEmpleado.Columns.Add("Imagen", typeof(byte[])); // La columna de imagen se almacena como un arreglo de bytes

            using (SqlCommand command = new SqlCommand("[dbo].[ConsultarEmpleadoPorID]", sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dtEmpleado);
                sqlConnection.Close();
            }
            if (dtEmpleado.Rows.Count > 0)
            {
                return dtEmpleado.Rows[0];
            }
            else
            {
                return null;
            }
        }
        public Empleado BuscarEmpleadoPorId(int idEmpleado)
        {
            try
            {
                Empleado empleado = null;
                using (SqlCommand command = new SqlCommand("ConsultarEmpleadoPorID", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        empleado = new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            IdCargo = Convert.ToInt32(reader["IdCargo"]),
                            Apellidos = reader["Apellidos"].ToString(),
                            Nombres = reader["Nombres"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                        };
                    }
                }
                return empleado;
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
        public DataTable GetDataFromDatabaseEmpleado()
        {
            DataTable dtDetallesVenta = new DataTable();

            try
            {
                sqlConnection.Open();

                string query = "SELECT * FROM Empleado";
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