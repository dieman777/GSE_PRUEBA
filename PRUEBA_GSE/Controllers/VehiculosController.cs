using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using PRUEBA_GSE.Models;
using PRUEBA_GSE.Services;
using PRUEBA_GSE.Templates;

namespace PRUEBA_GSE.Controllers
{
    public class VehiculosController : ControllerBase
    {
        private readonly VehiculosService _vehiculosService;
        private readonly TipoVehiculoService _tipoVehiculo;
        private readonly ServicioVehiculoService _servicioVehiculoService;

        private readonly TokenService _tokenService;

        public VehiculosController(VehiculosService vehiculos, TipoVehiculoService tipoVehiculoService, ServicioVehiculoService servicioVehiculoService, TokenService tokenService)
        {
            _vehiculosService = vehiculos;
            _tipoVehiculo = tipoVehiculoService;
            _servicioVehiculoService = servicioVehiculoService;
            _tokenService = tokenService;
        }
        
        [Authorize]
        [HttpPost("ConsultarVehiculo")]
        public async Task<IActionResult> ConsultarVehiculos([FromBody]VehiculoRequest vehiculosRequest)
        {
            if (vehiculosRequest.noPlaca != vehiculosRequest.confirmarPlaca)
            {
                return BadRequest(new { mensaje = "El número de placa no es igual al de la confirmación"});
            }

            var tv_obtenido = await _tipoVehiculo.GetTipoVehiculo(vehiculosRequest.tipo);
            var sv_obtenido = await _servicioVehiculoService.GetServicioVehiculoAsync(vehiculosRequest.servicio);


            await _vehiculosService.GetVehiculos(vehiculosRequest.noPlaca, vehiculosRequest.modelo, tv_obtenido.id, sv_obtenido.id);

            return Ok(new { mensaje = "Vehiculo existe"});
        }


        [HttpGet("InsertarServicioYTipoVehiculo")]
        public async Task<IActionResult> Registrar()
        {
            //Se crean datos para el tipo de Vehiculo
            var nuevoTipo = new List<TipoVehiculo>
            {
                new TipoVehiculo { descripcion = "Particular" },
                new TipoVehiculo { descripcion = "Público"}
            };
            await _tipoVehiculo.CrearTipoVehiculo(nuevoTipo);

            //Se  crea datos para el servicio del vehículo
            var nuevoServicio = new List<ServicioVehiculo> {
                new ServicioVehiculo{ descripcion = "A1"},
                new ServicioVehiculo{ descripcion = "B1"},
                new ServicioVehiculo{ descripcion = "C1"}
            };
            await _servicioVehiculoService.SetServicioVehiculo(nuevoServicio);


            return Ok(new { mensaje = "Tipos vehículo creados / Servicios Vehículo creados" });
        }



    }
}
