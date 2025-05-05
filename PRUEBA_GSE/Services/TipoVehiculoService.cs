using MongoDB.Bson;
using MongoDB.Driver;
using PRUEBA_GSE.Models;

namespace PRUEBA_GSE.Services
{
    public class TipoVehiculoService
    {
        private readonly IMongoCollection<TipoVehiculo> _tipoVehiculo;

        public TipoVehiculoService(MongoContextDB context)
        {
            _tipoVehiculo = context.GetCollection<TipoVehiculo>("TipoVehiculo");
        }

        /*public async Task<List<TipoVehiculo>> GetTipoVehiculo()
        {
            return await _tipoVehiculo.Find(Builders<TipoVehiculo>.Filter.Empty).ToListAsync();
        }*/


        public async Task<TipoVehiculo> GetTipoVehiculo(string descripcion)
        {
            return await _tipoVehiculo.Find(tv=>tv.descripcion == descripcion).FirstOrDefaultAsync();
        }

        public async Task CrearTipoVehiculo(List<TipoVehiculo> tipoVehiculo)
        {
            await _tipoVehiculo.InsertManyAsync(tipoVehiculo);
        }
    }
}
