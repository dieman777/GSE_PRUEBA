using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PRUEBA_GSE
{
    public class MongoContextDB
    {
        private readonly IMongoDatabase _basedatos;

        public MongoContextDB(IOptions<MongoDBSettings> configura)
        {
            var cliente = new MongoClient(configura.Value.ConnectionString);
            _basedatos = cliente.GetDatabase(configura.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string nombre) =>
            _basedatos.GetCollection<T>(nombre);
        

    }
}
