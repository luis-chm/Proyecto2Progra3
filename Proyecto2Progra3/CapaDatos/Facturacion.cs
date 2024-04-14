using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Proyecto2Progra3.CapaDatos
{
    public class Facturacion
    {
        public int codigoFactura { get; set; }
        public float subtotal { get; set; }
        public float Total { get; set; }
        public string Serie { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public int stock { get; set; }

    }
}