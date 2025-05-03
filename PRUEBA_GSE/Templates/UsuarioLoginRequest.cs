using System.ComponentModel.DataAnnotations;

namespace PRUEBA_GSE.Templates
{
    public class UsuarioLoginRequest
    {
        [Required]
        public string usuario { get; set; }
        [Required]
        public string contrasenia { get; set; }
    }
}
