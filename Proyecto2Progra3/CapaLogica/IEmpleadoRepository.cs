using System.Data;

namespace Proyecto2Progra3.Clases
{
    public interface IEmpleadoRepository
    {
        int AgregarEmpleado(Empleado empleado);
        void ModificarEmpleado(Empleado empleado);
        void EliminarEmpleado(int idEmpleado);
        DataTable ConsultarTodosEmpleados();
        DataRow ConsultarEmpleadoPorId(int idEmpleado);
    }
}