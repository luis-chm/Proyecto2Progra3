using System.Data;

namespace Proyecto2Progra3.Clases
{
    public interface IClienteRepository
    {
        int AgregarCliente(Cliente cliente);
        Cliente ConsultarClientePorId(int idCliente);
        DataTable ConsultarTodosClientes();
        void ModificarCliente(Cliente cliente);
        void EliminarCliente(int idCliente);
    }
}
