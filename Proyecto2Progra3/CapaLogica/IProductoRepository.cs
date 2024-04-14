using System.Collections.Generic;

namespace Proyecto2Progra3.Clases
{
    public interface IProductoRepository
    {
        int AgregarProducto(Producto producto);
        void ModificarProducto(Producto producto);
        void EliminarProducto(int idProducto, int idEmpleado);
        Producto ConsultarProductoPorId(int idProducto);
        IEnumerable<Producto> ConsultarTodosProductos();
    }
}
