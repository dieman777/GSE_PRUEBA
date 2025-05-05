using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using PRUEBA_GSE.Models;
using PRUEBA_GSE.Services;
using PRUEBA_GSE.Templates;
using System.Runtime.CompilerServices;

namespace PRUEBA_GSE.Controllers
{
    //[Router()]
    public class UsuariosController: ControllerBase
    {
        private readonly UsuariosService _usuariosService;
        private readonly VehiculosService _vehiculosService;
        private readonly ServicioVehiculoService _servicioVehiculoService;
        private readonly TipoVehiculoService _tipoVehiculoService;
        private readonly TokenService _tokenService;


        public UsuariosController(UsuariosService usuariosService, 
            VehiculosService vehiculosService, 
            ServicioVehiculoService servicioVehiculoService,
            TipoVehiculoService tipoVehiculoService,
            TokenService tokenService)
        { 
            _usuariosService = usuariosService;
            _vehiculosService = vehiculosService;
            _servicioVehiculoService = servicioVehiculoService;
            _tipoVehiculoService = tipoVehiculoService;
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

            //Insertar usuario y obtener el id para luego crear los datos predefinidos o colecciones
            var usuarioCreado = await _usuariosService.SetUsuarios(listarUsuario);
            await this.InsertarDatosPredefinidos(usuarioCreado.Id, usuariosRequest.noPlaca);


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
                return Unauthorized(new { mensaje = "Las credenciales no son correctas." });
            }
            //Se crea y devuleve token
            var usuarioToken = _tokenService.CrearToken(usuariosLoginRequest.usuario);
            return Ok(new { usuarioToken });
        }

        private async Task InsertarDatosPredefinidos(string usuarioCreadoId, string noPlaca)
        {
            try
            {
                //1----------------------------------------------------------------------------------
                //Se asignan 2 valores de acuerdo a lo que está registrado en la colección TipoVehiculo 
                string[] tiposv_opciones = { "Particular", "Público" };
                var random_tipos_opciones = new Random();
                var tipo_selecionado = tiposv_opciones[random_tipos_opciones.Next(tiposv_opciones.Length)];
                //2----------------------------------------------------------------------------------
                //Se optiene un modelo random pra registrar luego
                var random_modelos = new Random();
                var modelo_seleccionado = random_modelos.Next(1974, 2026).ToString();
                //3----------------------------------------------------------------------------------
                //Se asignan 3 valores de acuerdo a lo que está registrado en la colección ServicioVehiculo
                string[] sv_opciones = { "A1", "B1", "C1" };
                var random_sv_opciones = new Random();
                var sv_selecionado = sv_opciones[random_sv_opciones.Next(sv_opciones.Length)];
                //4----------------------------------------------------------------------------------
                //Se asignan 2 valores para forma de pago
                string[] fp_opciones = { "Contado", "Tío paco" };
                var random_fp_opciones = new Random();
                var fp_selecionado = fp_opciones[random_fp_opciones.Next(fp_opciones.Length)];

                //5
                var otroVehiculo = new Vehiculos
                {
                    modelo = modelo_seleccionado,
                    noPlaca = noPlaca,
                    tipoVehiculoId = await this.ObtenerTipoVehiculoId(tipo_selecionado),
                    servicioVehiculoId = await this.ObtenerServicioVehiculoId(sv_selecionado),
                    usuarioId = usuarioCreadoId,
                    formaPago = fp_selecionado
                };

                await _vehiculosService.SetVehiculos(otroVehiculo);
            } catch (Exception exc)
            {
                throw new Exception($"Se presentó un erro al insertar datos predefinidos: {exc}");
            }
        }

        private async Task<string> ObtenerServicioVehiculoId(string descripcion){
            var servicioV = await _servicioVehiculoService.GetServicioVehiculoAsync(descripcion);

            if (servicioV == null)
            {
                throw new Exception("El servicio de vehículo no existe.");
            }

            return servicioV.id;
        }

        private async Task<string> ObtenerTipoVehiculoId(string descripcion)
        {
            var tipoV = await _tipoVehiculoService.GetTipoVehiculo(descripcion);

            if (tipoV == null)
            {
                throw new Exception("El tipo de vehículo no existe");
            }
            return tipoV.id;
        }
    }
}
