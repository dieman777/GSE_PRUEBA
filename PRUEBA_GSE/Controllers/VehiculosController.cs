using Microsoft.AspNetCore.Mvc;
using PRUEBA_GSE.Models;
using PRUEBA_GSE.Services;

namespace PRUEBA_GSE.Controllers
{
    public class VehiculosController : ControllerBase
    {
        private readonly VehiculosService _vehiculosService;
        private readonly TokenService _tokenService;

        public VehiculosController(VehiculosService vehiculos, TokenService tokenService)
        {
            _vehiculosService = vehiculos;
            _tokenService = tokenService;
        }



        public async Task<IActionResult> Insertar()
        {
            var listaVerhiculos = new Vehiculos { 
                modelo = "Volkswagen",
                noPlaca = ""
            };
            return Ok(new { mensaje = "Vehiculos insertados correstamente" });
        }

        /*
        [Authorize]
        [HttpPost("ConsultarVehiculo")]
        public async Task<IActionResult> ConsultarVehiculos(Vehiculos vehiculos)
        {

        }*/


    }
}
