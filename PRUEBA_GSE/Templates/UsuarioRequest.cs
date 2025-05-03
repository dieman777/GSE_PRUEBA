using PRUEBA_GSE.Models;
using System.ComponentModel.DataAnnotations;

namespace PRUEBA_GSE.Templates
{
    public class UsuarioRequest
    {
        [Required]
        public string usuario { get; set; }
        [Required]
        public string contrasenia { get; set; }
        [Required]
        public string nombre { get; set; }
    }
}
