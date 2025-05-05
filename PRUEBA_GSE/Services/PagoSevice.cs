using MongoDB.Driver;
using PRUEBA_GSE.Models;

namespace PRUEBA_GSE.Services
{
    public class PagoSevice
    {
        private readonly IMongoCollection<Pago> _pago;

        public PagoSevice(MongoContextDB contextp)
        {
            _pago = contextp.GetCollection<Pago>("Pago");
        }

        public async Task<Pago> SetPago(Pago pago)
        {
            await _pago.InsertOneAsync(pago);
            return pago;
        }
    }
}
