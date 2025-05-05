using MongoDB.Bson.Serialization.Attributes;

namespace PRUEBA_GSE.Models
{
    public class Pago
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }
        public string nombre { get; set; }
        public string documentoCliente { get; set; }
    }
}
