using Proyecto2Progra3.CapaDatos;
using System.Data;


namespace Proyecto2Progra3.CapaLogica
{
    public interface IFacturacion
    {
        int AgregarDetalleFactura(int linea, int idProd, int cant, float precio, float subtotal, int stock);
        int AgregarMaestroFactura(Facturacion facturacion);
    }
}
