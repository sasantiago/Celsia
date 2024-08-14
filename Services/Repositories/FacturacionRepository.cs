using Microsoft.EntityFrameworkCore;
using celsia.Data;
using celsia.Models;
using System.Threading.Tasks;
using celsia.Services.Interfaces;
using celsia.Helpers;
namespace celsia.Services
{
    public class FacturacionRepository : IFacturacionRepository
    {
        private readonly DataContext _context;


        public FacturacionRepository(DataContext context)
        {
            _context = context;

        }


        public void CrearFactura(Facturacion factura)
        {
            try
            {
            _context.Facturaciones.Add(factura);
            _context.SaveChangesAsync();
                
            }
            catch (Exception)
            {
                
                throw new Exception("Factura no fue creada");
            }
        }

    
        public void DeleteFactura(int Id)
        {
            var facturaToRemove = _context.Facturaciones.Find(Id);
            if (facturaToRemove != null)
            {
                _context.Facturaciones.Remove(facturaToRemove);
                _context.SaveChangesAsync();
            }
        }

    

        public void UpdateFactura(int Id, Facturacion facturacion)
        {
            var facturaToUpdate = _context.Facturaciones.Find(Id);
            if (facturaToUpdate!= null)
            {
                facturaToUpdate.MontoFacturado = facturacion.MontoFacturado;
                facturaToUpdate.MontoPagado = facturacion.MontoPagado;
                facturaToUpdate.PeriodoFactura = facturacion.PeriodoFactura;

                _context.SaveChangesAsync();
            }
            else{
                throw new Exception("Factura no encontrada");
            }
        }

        IEnumerable<Facturacion> IFacturacionRepository.GetAll()
        {
            var Alls = _context.Facturaciones.ToList();
            if (Alls.Any())
            {
                return Alls;
            }
            else
            {
                throw new Exception("No se puede traer las facturas");
            }
        }

        Facturacion IFacturacionRepository.GetById(int Id)
        {
            try
        {
            var findit = _context.Facturaciones.FirstOrDefault(x => x.NumeroFactura == Id);
            if (findit != null)
            {
                return findit;
            }
            else
            {
                throw new Exception("Factura no encontrada");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Factura no encontrada");
        }
        }

    }
}