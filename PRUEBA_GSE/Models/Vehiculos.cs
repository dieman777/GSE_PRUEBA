using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PRUEBA_GSE.Models
{
    public class Vehiculos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string noPlaca { get; set; }
        public string modelo { get; set; }
        public string tipo { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string servicioVehiculoId {get;set;}
        [BsonRepresentation(BsonType.ObjectId)]
        public string usuarioId { get; set; }
    }
}
