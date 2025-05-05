using MongoDB.Driver;
using PRUEBA_GSE.Models;

namespace PRUEBA_GSE.Services
{
    public class ClienteService
    {
        private readonly IMongoCollection<Clientes> _clientes;

        public ClienteService(MongoContextDB context)
        {
            _clientes = context.GetCollection<Clientes>("Clientes");
        }

        public async Task<Clientes> SetClientes(Clientes clientes)
        {
           await _clientes.InsertOneAsync(clientes);
            return clientes;
        }
    }
}
