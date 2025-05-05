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

        /*public async Task<Vehiculos?> GetVehiculos(string)
        {
           
        }*/

        //Se alista para insertar valores predeterminados
        public async Task SetVehiculos(Vehiculos vehiculos)
        { 
            await _vehiculos.InsertOneAsync(vehiculos);
        }
        
    }
}
