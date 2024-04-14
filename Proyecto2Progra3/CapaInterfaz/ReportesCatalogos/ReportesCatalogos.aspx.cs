using Proyecto2Progra3.CapaLogica;
using Proyecto2Progra3;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Proyecto2Progra3.Clases;
using System.Drawing;
using ClosedXML;
using ClosedXML.Excel;
using Proyecto2Progra3.Interfaz;

namespace Proyecto2Progra3.CapaInterfaz.ReportesCatalogos
{
    public partial class ReportesCatalogos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnBuscarCatEmpleados_Click(object sender, EventArgs e)
        {

            EmpleadoDAL dataEmpleado = new EmpleadoDAL();
            DataTable data = dataEmpleado.GetDataFromDatabaseEmpleado();

            string filepath = Server.MapPath("~/Archivos/ReporteEmpleados.xlsx");
            ExportToXLSEachCat(data, filepath);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ReporteEmpleados.xlsx");
            Response.TransmitFile(filepath);
            Response.End();

        }
        protected void btnBuscarCatClientes_Click(object sender, EventArgs e)
        {
            ClienteDAL dataCliente = new ClienteDAL();
            DataTable data = dataCliente.GetDataFromDatabaseCliente();

            string filepath = Server.MapPath("~/Archivos/ReporteClientes.xlsx");
            ExportToXLSEachCat(data, filepath);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ReporteCliente.xlsx");
            Response.TransmitFile(filepath);
            Response.End();

        }
        protected void btnBuscarCatProductos_Click(object sender, EventArgs e)
        {
            ProductoRepository dataCliente = new ProductoRepository();
            DataTable data = dataCliente.GetDataFromDatabaseProducto();

            string filepath = Server.MapPath("~/Archivos/ReporteProductos.xlsx");
            ExportToXLSEachCat(data, filepath);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ReporteProductos.xlsx");
            Response.TransmitFile(filepath);
            Response.End();

        }
        protected void btnBuscarCatUsuario_Click(object sender, EventArgs e)
        {
            UsuarioManager datausuario = new UsuarioManager();
            DataTable data = datausuario.GetDataFromDatabaseUsuario();

            string filepath = Server.MapPath("~/Archivos/ReporteUsuarios.xlsx");
            ExportToXLSEachCat(data, filepath);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Reporteusuarios.xlsx");
            Response.TransmitFile(filepath);
            Response.End();
        }

        protected void btnCatCategoria_Click(object sender, EventArgs e)
        {
            CategoriaDAL datacategoria = new CategoriaDAL();
            DataTable data = datacategoria.ConsultarTodasCategorias();

            string filepath = Server.MapPath("~/Archivos/ReporteCategorias.xlsx");
            ExportToXLSEachCat(data, filepath);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ReporteCategorias.xlsx");
            Response.TransmitFile(filepath);
            Response.End();
        }

        protected void btnCatCargo_Click(object sender, EventArgs e)
        {
            CargoDAL datacargo = new CargoDAL();
            DataTable data = datacargo.ConsultarTodosCargos();

            string filepath = Server.MapPath("~/Archivos/ReporteCargos.xlsx");
            ExportToXLSEachCat(data, filepath);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ReporteCargos.xlsx");
            Response.TransmitFile(filepath);
            Response.End();
        }
        public void ExportToXLSEachCat(DataTable data, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Datos");
                worksheet.Cell(1, 1).InsertTable(data.AsEnumerable());
                workbook.SaveAs(filePath);

            }
        }
        protected void btnCatTodos_Click(object sender, EventArgs e)
        {
            //Obtener los datos de todos los catálogos
            DataTable dtEmpleados = GetDataFromDatabase("Empleados");
            dtEmpleados.TableName = "Empleados";// asigna nombre a la hoja

            DataTable dtClientes = GetDataFromDatabase("Clientes");
            dtClientes.TableName = "Clientes";

            DataTable dtProductos = GetDataFromDatabase("Productos");
            dtProductos.TableName = "Productos";

            DataTable dtUsuarios = GetDataFromDatabase("Usuarios");
            dtUsuarios.TableName = "Usuarios";

            DataTable dtCategorias = GetDataFromDatabase("Categorias");
            dtCategorias.TableName = "Categorias";

            DataTable dtCargos = GetDataFromDatabase("Cargos");
            dtCargos.TableName = "Cargos";

            // Combinar todos los datos en un solo DataSet
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dtEmpleados);
            dataSet.Tables.Add(dtClientes);
            dataSet.Tables.Add(dtProductos);
            dataSet.Tables.Add(dtUsuarios);
            dataSet.Tables.Add(dtCategorias);
            dataSet.Tables.Add(dtCargos);

            //Exportar todos los datos a un archivo de Excel con hojas separadas
            string filePath = Server.MapPath("~/Archivos/ReporteGeneralCatalogos.xlsx");
            ExportToXLSAllData(dataSet, filePath);

            // Descargar el archivo Excel
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ReporteGeneralCatalogos.xlsx");
            Response.TransmitFile(filePath);
            Response.End();
        }
        protected DataTable GetDataFromDatabase(string tipoCatalogo)
        {
            switch (tipoCatalogo)
            {
                case "Empleados":
                    EmpleadoDAL dataEmpleado = new EmpleadoDAL();
                    DataTable dtEmpleados = dataEmpleado.GetDataFromDatabaseEmpleado();
                    dtEmpleados.TableName = "Empleados";
                    return dtEmpleados;
                case "Clientes":
                    ClienteDAL dataCliente = new ClienteDAL();
                    DataTable dtClientes = dataCliente.GetDataFromDatabaseCliente();
                    dtClientes.TableName = "Clientes";
                    return dtClientes;
                case "Productos":
                    ProductoRepository dataProducto = new ProductoRepository();
                    DataTable dtProductos = dataProducto.GetDataFromDatabaseProducto();
                    dtProductos.TableName = "Productos";
                    return dtProductos;
                case "Usuarios":
                    UsuarioManager dataUsuario = new UsuarioManager();
                    DataTable dtUsuarios = dataUsuario.GetDataFromDatabaseUsuario();
                    dtUsuarios.TableName = "Usuarios";
                    return dtUsuarios;
                case "Categorias":
                    CategoriaDAL dataCategoria = new CategoriaDAL();
                    DataTable dtCategorias = dataCategoria.ConsultarTodasCategorias();
                    dtCategorias.TableName = "Categorias";
                    return dtCategorias;
                case "Cargos":
                    CargoDAL dataCargo = new CargoDAL();
                    DataTable dtCargos = dataCargo.ConsultarTodosCargos();
                    dtCargos.TableName = "Cargos";
                    return dtCargos;
                default:
                    return null;
            }
        }
        public void ExportToXLSAllData(DataSet dataSet, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                foreach (DataTable dt in dataSet.Tables)
                {
                    var worksheet = workbook.Worksheets.Add(dt.TableName);
                    worksheet.Cell(1, 1).InsertTable(dt.AsEnumerable());
                }
                workbook.SaveAs(filePath);
            }
        }
    }
}