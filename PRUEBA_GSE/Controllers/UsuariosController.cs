using Microsoft.AspNetCore.Mvc;
using PRUEBA_GSE.Models;
using PRUEBA_GSE.Services;
using PRUEBA_GSE.Templates;

namespace PRUEBA_GSE.Controllers
{
    //[Router()]
    public class UsuariosController: ControllerBase
    {
        private readonly UsuariosService _usuariosService;
        private readonly TokenService _tokenService;


        public UsuariosController(UsuariosService usuariosService, TokenService tokenService)
        { 
            _usuariosService = usuariosService;
            _tokenService = tokenService;
        }


        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario(UsuarioRequest usuariosRequest)
        {
            var existeUsusario = await _usuariosService.GetUsuariosAsync(usuariosRequest.usuario);
            if (existeUsusario != null)
            {
                return BadRequest(new { mensaje = $"Ya existe un usuario llamado {usuariosRequest.usuario}"});
            }

            var listarUsuario = new Usuarios
            {
                usuario = usuariosRequest.usuario,
                contrasenia = usuariosRequest.contrasenia,
                nombre = usuariosRequest.nombre
            };

            await _usuariosService.SetUsuarios(listarUsuario);
            return Ok(new { mensaje = "Usuario registrado"});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UsuarioLoginRequest usuariosLoginRequest)
        {
            //Validar si el usuario ingresado es correspondiente a la contraseña
            var esValido = await _usuariosService.ValidarLogin(usuariosLoginRequest.usuario, usuariosLoginRequest.contrasenia);
            if (!esValido)
            { 
                //Se devuelve un mensaje negativo
                return Unauthorized(new { mensaje = "Las credenciales no son corretas." });
            }
            //Se crea y devuleve token
            var usuarioToken = _tokenService.CrearToken(usuariosLoginRequest.usuario);
            return Ok(new { usuarioToken });
        }
        
    }
}
