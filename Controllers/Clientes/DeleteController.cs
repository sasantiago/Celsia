using Microsoft.AspNetCore.Mvc;
using celsia.Services.Interfaces;
using System.Threading.Tasks;

namespace celsia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public DeleteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            try
            {
                _clienteRepository.DeleteBook(id);
                return Ok("Cliente eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el cliente: {ex.Message}");
            }
        }
    }
}
