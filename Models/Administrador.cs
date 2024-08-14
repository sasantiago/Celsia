
using System.Text.Json.Serialization;
using celsia.Data;

namespace celsia.Models{
    public class Administrador{
        public int Id { get; set;}       
        public string? Correo {get; set;}
        public string? Contraseña {get; set;}
        
    }
}