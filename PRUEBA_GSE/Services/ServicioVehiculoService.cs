using MongoDB.Driver;
using PRUEBA_GSE.Models;

namespace PRUEBA_GSE.Services
{
    public class ServicioVehiculoService
    {
        private readonly IMongoCollection<ServicioVehiculo> _servicioVehiculo;

        public ServicioVehiculoService(MongoContextDB context){
            _servicioVehiculo = context.GetCollection<ServicioVehiculo>("ServicioVehiculo");
        }

        public async Task<ServicioVehiculo?> GetServicioVehiculoAsync(string descripcion)
        {
            return await _servicioVehiculo.Find(sv => sv.descripcion == descripcion).FirstOrDefaultAsync();
        }

        public async Task SetServicioVehiculo(/*ServicioVehiculo*/List<ServicioVehiculo> listservicioVehiculo)
        {
            //await _servicioVehiculo.InsertOneAsync(listservicioVehiculo);
            await _servicioVehiculo.InsertManyAsync(listservicioVehiculo);
        }
    }
}
