using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PRUEBA_GSE.Models;

namespace PRUEBA_GSE.Services
{
    public class VehiculosService
    {
        private readonly IMongoCollection<Vehiculos> _vehiculos;


        public VehiculosService(MongoContextDB context) { 
            _vehiculos = context.GetCollection<Vehiculos>("Vehiculos");
        }

        public async Task<Vehiculos?> GetVehiculos(string noPlaca, string modelo, string tipoVehiculoId, string servicioVehiculoId)
        {
            var buscar = Builders<Vehiculos>.Filter.And(
                Builders<Vehiculos>.Filter.Eq(v=>v.noPlaca, noPlaca),
                Builders<Vehiculos>.Filter.Eq(v=>v.modelo, modelo),
                Builders<Vehiculos>.Filter.Eq(v=>v.tipoVehiculoId, tipoVehiculoId),
                Builders<Vehiculos>.Filter.Eq(v=>v.servicioVehiculoId, servicioVehiculoId)
                );
            return await _vehiculos.Find(buscar).FirstOrDefaultAsync();
        }

        //Se alista para insertar valores predeterminados
        public async Task SetVehiculos(Vehiculos vehiculos)
        { 
            await _vehiculos.InsertOneAsync(vehiculos);
        }
        
    }
}
