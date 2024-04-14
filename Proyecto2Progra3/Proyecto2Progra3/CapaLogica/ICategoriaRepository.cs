using System.Data;

namespace Proyecto2Progra3.Clases
{
    public interface ICategoriaRepository
    {
        int AgregarCategoria(string descripcion);
        Categoria ConsultarCategoriaPorId(int idCategoria);
        void ModificarCategoria(int idCategoria, string descripcion);
        void EliminarCategoria(int idCategoria);
        DataTable ConsultarTodasCategorias();
    }
}
