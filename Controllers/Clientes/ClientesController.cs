using Microsoft.AspNetCore.Mvc;
using celsia.Models;
using celsia.Services.Interfaces;
using System.Collections.Generic;



// cambie los nombres de las vistas con las acciones ojoooooooooooooooo!!!!!
namespace celsia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult GetAllCliente()
        {
            var clientes = _clienteRepository.GetAll();
            return View("Index", clientes);
        }

        public IActionResult GetClienteById(int id)
        {
            var cliente = _clienteRepository.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View("Details", cliente);
        }
        // Obtener todos los clientes
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetAllClientes()
        {
            try
            {
                var clientes = _clienteRepository.GetAll();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los clientes: {ex.Message}");
            }
        }

        // Obtener un cliente por ID
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetClienteByIds(int id)
        {
            try
            {
                var cliente = _clienteRepository.GetById(id);
                if (cliente == null)
                {
                    return NotFound("Cliente no encontrado");
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el cliente: {ex.Message}");
            }
        }
    }
}