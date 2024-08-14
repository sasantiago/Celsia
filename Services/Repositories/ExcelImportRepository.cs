using ClosedXML.Excel;
using celsia.Models;
using celsia.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace celsia.Services
{
    public class ExcelImportRepository : IExcelImportRepository
    {
        private readonly IFacturacionRepository _facturacionRepository;

        public ExcelImportRepository(IFacturacionRepository facturacionRepository)
        {
            _facturacionRepository = facturacionRepository;
        }

        public async Task ImportFromExcelAsync(string filePath)
        {
            var facturaciones = new List<Facturacion>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Asume que los datos están en la primera hoja
                var rows = worksheet.RowsUsed().Skip(1); // Salta la fila de encabezado

                foreach (var row in rows)
                {
                    var numeroFactura = row.Cell(12).GetValue<string>(); // Ajusta el índice según la columna
                    var montoFacturado = row.Cell(14).GetValue<decimal>(); // Ajusta el índice según la columna
                    var montoPagado = row.Cell(15).GetValue<decimal>(); // Ajusta el índice según la columna
                    var periodoFactura = row.Cell(13).GetValue<string>(); // Ajusta el índice según la columna

                    var factura = new Facturacion
                    {
                        NumeroFactura = int.Parse(numeroFactura),
                        MontoFacturado = decimal.ToInt32(montoFacturado),
                        MontoPagado = decimal.ToInt32(montoPagado),
                        PeriodoFactura = periodoFactura
                    };

                    facturaciones.Add(factura);
                }
            }

            foreach (var factura in facturaciones)
            {
                try
                {
                    _facturacionRepository.CrearFactura(factura);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al importar la factura {factura.NumeroFactura}: {ex.Message}");
                }
            }

            await Task.CompletedTask;
        }
    }
}
