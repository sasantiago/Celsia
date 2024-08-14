using Microsoft.AspNetCore.Mvc;
using celsia.Models;
using celsia.Services.Interfaces;

namespace celsia.Controllers
{
    public class FacturacionEditController : Controller
    {
        private readonly IFacturacionRepository _facturacionRepository;

        public FacturacionEditController(IFacturacionRepository facturacionRepository)
        {
            _facturacionRepository = facturacionRepository;
        }

        // GET: FacturacionEdit/5
        public IActionResult Edit(int id)
        {
            var factura = _facturacionRepository.GetById(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        // POST: FacturacionEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("NumeroFactura, MontoFacturado, MontoPagado, PeriodoFactura")] Facturacion factura)
        {
            if (id != factura.NumeroFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _facturacionRepository.UpdateFactura(id, factura);
                    return RedirectToAction("Index", "FacturacionIndex");
                }
                catch
                {
                    ModelState.AddModelError("", "Error al actualizar la factura.");
                }
            }
            return View(factura);
        }
    }
}
