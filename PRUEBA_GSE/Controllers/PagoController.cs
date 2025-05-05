using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRUEBA_GSE.Models;
using PRUEBA_GSE.Services;
using PRUEBA_GSE.Templates;

namespace PRUEBA_GSE.Controllers
{
    public class PagoController : ControllerBase
    {
        private readonly PagoSevice _pagoSevice;

        public PagoController(PagoSevice pagoSevice)
        { 
            _pagoSevice = pagoSevice;
        }

        [Authorize]
        [HttpPost("RegistrarPago")]
        public async Task<IActionResult> RegistrarPago([FromBody]PagoRequest pagoRequest)
        {
            var alistar = new Pago
            {
                nombre = pagoRequest.nombre,
               documentoCliente = pagoRequest.documentoCliente
            };
            await _pagoSevice.SetPago(alistar);
            return Ok(new { mensaje = "Tipo de pago registrado"});
        }
    }
}
