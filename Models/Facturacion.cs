
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace celsia.Models{
    public class Facturacion{
        public int NumeroFactura { get; set;}
        [Required(ErrorMessage = "el periodo de factura es requeridas")]
        public string? PeriodoFactura { get; set;}
        [Required(ErrorMessage = "el monto facturado es requerido")]
        public int MontoFacturado {get; set;}
        [Required(ErrorMessage = "El monto pagado es requerido")]
        public int MontoPagado { get; set;}
        public int Id_cliente {get; set;}
        public Cliente cliente {get;set;}
    }
}