using Microsoft.AspNetCore.Mvc;
using celsia.Services.Interfaces;

namespace celsia.Controllers
{
    public class FacturacionDeleteController : Controller
    {
        private readonly IFacturacionRepository _facturacionRepository;

        public FacturacionDeleteController(IFacturacionRepository facturacionRepository)
        {
            _facturacionRepository = facturacionRepository;
        }

        // GET: FacturacionDelete/5
        public IActionResult Delete(int id)
        {
            var factura = _facturacionRepository.GetById(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        // POST: FacturacionDelete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _facturacionRepository.DeleteFactura(id);
                return RedirectToAction("Index", "FacturacionIndex");
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar la factura.");
                return View();
            }
        }
    }
}
