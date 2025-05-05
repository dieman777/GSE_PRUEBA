using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRUEBA_GSE.Models;
using PRUEBA_GSE.Services;
using PRUEBA_GSE.Templates;

namespace PRUEBA_GSE.Controllers
{
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        [Authorize]
        [HttpPost("RegistrarCliente")]
        public async Task<IActionResult> RegistrarCliente([FromBody]ClienteRequest clienteRequest)
        {
            var selecion = new Clientes { 
                tipoDocumento = clienteRequest.tipoDocumento,
                numeroDocumento = clienteRequest.numeroDocumento,
                nombre = clienteRequest.nombre,
                apellido = clienteRequest.apellido,
                direccion = clienteRequest.direccion,
                telefono = clienteRequest.telefono,
                correo = clienteRequest.correo
            };

            await _clienteService.SetClientes(selecion);

            return Ok(new { mensaje = "Cliente registrado"});
        }
    }
}
