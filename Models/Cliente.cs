
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using celsia.Data;

namespace celsia.Models{
    public class Cliente{
        public int Id { get; set;}
        [Required(ErrorMessage = "el nombre es requerido")]
        public string NombreCompleto { get; set;}
        [Required(ErrorMessage = "Identificacion es requerida")]
        public string Identificacion {get; set;}
        [Required(ErrorMessage = "Direccion es requerida")]
        public string Direccion { get; set;}
        [Required(ErrorMessage = "Telefono es requerido")]
        public string Telefono {get; set;}
        [Required(ErrorMessage = "Correo es requerido")]        
        public string Correo {get; set;}
        [Required(ErrorMessage = "Contraseña es requerido")]
        public string contrasena {get; set;}

        
    }
}