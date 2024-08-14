using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using celsia.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace celsia.Controllers
{
    public class ImportController : Controller
    {
        private readonly IExcelImportRepository _importService;

        public ImportController(IExcelImportRepository importService)
        {
            _importService = importService;
        }

        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                try
                {
                    await _importService.ImportFromExcelAsync(filePath);
                    TempData["Message"] = "Archivo importado exitosamente.";
                }
                catch (Exception ex)
                {
                    TempData["Message"] = $"Error al importar el archivo: {ex.Message}";
                }

                System.IO.File.Delete(filePath); // Elimina el archivo temporal

                return RedirectToAction("Index");
            }

            TempData["Message"] = "No se seleccionó ningún archivo.";
            return View("Index");
        }
    }
}
