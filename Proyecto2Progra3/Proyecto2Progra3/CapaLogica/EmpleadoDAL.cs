using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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
            using (SqlCommand command = new SqlCommand("[dbo].[ModificarEmpleado]", sqlConnection))
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
                command.Parameters.AddWithValue("@Imagen", empleado.Imagen == null ? (object)DBNull.Value : empleado.Imagen);

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
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


    }
}