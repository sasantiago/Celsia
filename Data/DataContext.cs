using Microsoft.EntityFrameworkCore;
using celsia.Models;


namespace celsia.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // Implementamos los modelos de la DB
        public DbSet<Cliente> Clientes { get; set;}
        public DbSet<Facturacion> Facturaciones {get; set;}
        public DbSet<Administrador> Administradores {get; set;}
    }
}