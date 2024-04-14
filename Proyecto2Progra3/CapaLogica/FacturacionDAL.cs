using Examen2progra.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Services.Description;
using Proyecto2Progra3.CapaDatos;

namespace Proyecto2Progra3.CapaLogica
{
    public class FacturacionDAL : IFacturacion
    {
        private readonly SqlConnection sqlConnection;

        public FacturacionDAL()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
        }

        public int AgregarDetalleFactura(int linea, int idProd, int cant, float precio, float subtotal, int stock)
        {
            int retorno = 0;

            try
            {
                sqlConnection.Open(); // Abre la conexión

                using (SqlCommand command = new SqlCommand("DetFactura", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Linea", linea));
                    command.Parameters.Add(new SqlParameter("@IdProducto", idProd));
                    command.Parameters.Add(new SqlParameter("@Cantidad", cant));
                    command.Parameters.Add(new SqlParameter("@PrecioUnitario", precio));
                    command.Parameters.Add(new SqlParameter("@SubTotal", subtotal));

                    retorno = command.ExecuteNonQuery();
                }

                string updateQuery = "UPDATE producto SET stock = @NuevoStock WHERE IdProducto = @IdProducto";
                using (SqlCommand command = new SqlCommand(updateQuery, sqlConnection))
                {

                    command.Parameters.AddWithValue("@NuevoStock", stock);
                    command.Parameters.AddWithValue("@IdProducto", idProd);

                    // Ejecutar la consulta
                    // int rowsAffected = command.ExecuteNonQuery();
                    retorno = command.ExecuteNonQuery() + 1;

                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close(); // Cierra la conexión
            }
            return retorno;
        }
        public int AgregarMaestroFactura(Facturacion facturacion)
        {
            int retorno = 0;

            try
            {
                sqlConnection.Open(); // Abre la conexión

                using (SqlCommand command = new SqlCommand("MaeFatura", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdEmpleado ", facturacion.IdEmpleado));
                    command.Parameters.Add(new SqlParameter("@IdCliente ", facturacion.IdCliente));
                    command.Parameters.Add(new SqlParameter("@Serie ", facturacion.Serie));
                    command.Parameters.Add(new SqlParameter("@NumeroDocumento ", facturacion.NumeroDocumento));
                    command.Parameters.Add(new SqlParameter("@TipoDocumento ", facturacion.TipoDocumento));
                    command.Parameters.Add(new SqlParameter("@Total ", facturacion.Total));

                    retorno = command.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close(); // Cierra la conexión
            }

            return retorno;
        }
        public DataTable ObtenerDetallesVentaPorNumeroFactura(string valor, string tbusqueda)
        {
            DataTable dtDetallesVenta = new DataTable();

            try
            {
                sqlConnection.Open();

                if (tbusqueda == "Factura")
                {
                    string query = "SELECT * FROM DetalleVenta WHERE IdVenta = (SELECT IdVenta FROM Venta WHERE NumeroDocumento = @NumeroDocumento)";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@NumeroDocumento", valor);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtDetallesVenta);
                    }
                }
                else if (tbusqueda == "Cliente")
                {

                    string query = "SELECT * FROM DetalleVenta WHERE IdVenta IN (SELECT IdVenta FROM Venta WHERE IdCliente = @IdCliente)";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        int id = int.Parse(valor);
                        command.Parameters.AddWithValue("@IdCliente", id);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtDetallesVenta);
                    }
                }
                else if (tbusqueda == "Equipo")
                {
                    string query = "SELECT * FROM DetalleVenta WHERE IdProducto = @IdProducto";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@IdProducto", valor);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtDetallesVenta);
                    }
                }
                else if (tbusqueda == "Fecha")
                {
                    string query = "SELECT * FROM DetalleVenta WHERE IdVenta IN (SELECT IdVenta FROM Venta WHERE FechaVenta = @FechaVenta)";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        DateTime date = Convert.ToDateTime(valor);
                        command.Parameters.AddWithValue("@FechaVenta", date);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtDetallesVenta);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los detalles: " + ex.Message);
            }
            return dtDetallesVenta;
        }
    }
}