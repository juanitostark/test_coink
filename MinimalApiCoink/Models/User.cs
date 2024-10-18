using System.ComponentModel.DataAnnotations;

namespace MinimalApiCoink.Models
{
    public class User
    {
        public int IdUsuario { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public int IdMunicipio { get; set; }
    }
}