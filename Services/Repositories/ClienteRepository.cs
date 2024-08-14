using Microsoft.EntityFrameworkCore;
using celsia.Data;
using celsia.Models;
using System.Threading.Tasks;
using celsia.Services.Interfaces;
using celsia.Helpers;
namespace celsia.Services
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext _context;


        public ClienteRepository(DataContext context)
        {
            _context = context;

        }


        public void CrearCliente(Cliente cliente, string password)
        {
            if (string.IsNullOrWhiteSpace(cliente.Correo))
                throw new ArgumentException("Email es requerido");

            var emailExists = _context.Clientes.Any(u => u.Correo == cliente.Correo);
            if (emailExists)
                throw new ArgumentException("Email ya está en uso");

            if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
            {
                throw new ArgumentException("Contraseña insegura");
            }

            var pass = new PasswordHasher(); //instancia  del hasher
            string tempPassword = password; //var temp password guarda el valor del password ingresado
            var hasPassword = pass.HashPassword(tempPassword); //se hashea la password
            cliente.contrasena = hasPassword; // se asigna el valor de la contraseña hashea en la contraseña que envia el usuario
            _context.Clientes.Add(cliente);
            _context.SaveChangesAsync();

        }


        public void DeleteBook(int Id)
        {
            var bookToRemove = _context.Clientes.Find(Id);
            if (bookToRemove != null)
            {
                _context.Clientes.Remove(bookToRemove);
                _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Cliente> GetAll()
        {
            var Alls = _context.Clientes.ToList();
            if (Alls.Any())
            {
                return Alls;
            }
            else
            {
                throw new Exception("No se puede traer los clientes");
            }
        }
    public Cliente GetById(int Id)
    {
        try
        {
            var findit = _context.Clientes.FirstOrDefault(x => x.Id == Id);
            if (findit != null)
            {
                return findit;
            }
            else
            {
                throw new Exception("Cliente no encontrado");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Cliente no encontrado");
        }
    }

        public void UpdateBook(int Id, Cliente cliente)
        {
            try
            {
                var bookToUpdate = _context.Clientes.Find(Id);
                if (bookToUpdate!= null)
                {
                    bookToUpdate.NombreCompleto = cliente.NombreCompleto;
                    bookToUpdate.Identificacion = cliente.Identificacion;
                    bookToUpdate.Direccion = cliente.Direccion;
                    bookToUpdate.Telefono = cliente.Telefono;
                    bookToUpdate.Correo = cliente.Correo;

                    _context.SaveChangesAsync();
                }
                else{
                    throw new Exception("Cliente no encontrado");
                }
            }
            catch (Exception)
            {
                throw new Exception("Cliente no pudo ser actualizado");
            }
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsLower)) return false;
            if (!password.Any(char.IsDigit)) return false;
            if (!password.Any(ch => !char.IsLetterOrDigit(ch))) return false;

            return true;
        }
    }
}