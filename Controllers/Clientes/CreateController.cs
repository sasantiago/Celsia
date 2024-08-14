using Microsoft.AspNetCore.Mvc;
using celsia.Models;
using celsia.Services.Interfaces;
using System.Threading.Tasks;

namespace celsia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public CreateController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public IActionResult CrearCliente()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] Cliente cliente, [FromQuery] string password)
        {
            try
            {
                _clienteRepository.CrearCliente(cliente, password);
                return Ok("Cliente creado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el cliente: {ex.Message}");
            }
        }
    }
}
