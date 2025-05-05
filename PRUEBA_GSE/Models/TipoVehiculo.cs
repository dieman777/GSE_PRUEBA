using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PRUEBA_GSE.Models
{
    public class TipoVehiculo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string descripcion { get; set; }
    }
}
