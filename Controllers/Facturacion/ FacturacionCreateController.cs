using Microsoft.AspNetCore.Mvc;
using celsia.Models;
using celsia.Services.Interfaces;

namespace celsia.Controllers
{
    public class FacturacionCreateController : Controller
    {
        private readonly IFacturacionRepository _facturacionRepository;

        public FacturacionCreateController(IFacturacionRepository facturacionRepository)
        {
            _facturacionRepository = facturacionRepository;
        }

        // GET: FacturacionCreate
        public IActionResult Create()
        {
            return View();
        }

        // POST: FacturacionCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NumeroFactura, MontoFacturado, MontoPagado, PeriodoFactura")] Facturacion factura)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _facturacionRepository.CrearFactura(factura);
                    return RedirectToAction("Index", "FacturacionIndex");
                }
                catch
                {
                    ModelState.AddModelError("", "Error al crear la factura.");
                }
            }
            return View(factura);
        }
    }
}
