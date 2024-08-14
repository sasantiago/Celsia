using celsia.Data;
using celsia.Models;

namespace celsia.Services.Interfaces{
    public interface IClienteRepository{
        public void CrearCliente (Cliente cliente,string password);
        Cliente GetById (int Id);
        IEnumerable<Cliente> GetAll ();
        public void UpdateBook(int Id, Cliente book);
        public void DeleteBook(int Id);
    }
}