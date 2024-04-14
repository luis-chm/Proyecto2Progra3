using Proyecto2Progra3.CapaDatos;
using System.Data;

namespace Proyecto2Progra3.CapaLogica
{
    public interface ICargoRepository
    {
        int AgregarCargo(string descripcion);
        Cargo ConsultarCargoPorId(int idCargo);
        void ModificarCargo(int idCargo, string descripcion);
        void EliminarCargo(int idCargo);
        DataTable ConsultarTodosCargos();
    }
}
