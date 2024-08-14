using celsia.Data;
using celsia.Models;

namespace celsia.Services.Interfaces{
    public interface IFacturacionRepository{
        public void CrearFactura (Facturacion factura);
        Facturacion GetById (int Id);
        IEnumerable<Facturacion> GetAll ();
        public void UpdateFactura(int Id, Facturacion facturacion);
        public void DeleteFactura(int Id);
    }
}