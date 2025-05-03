using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PRUEBA_GSE.Models
{
    public class Usuarios
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string usuario { get; set; }
        public string contrasenia {  get; set; }
        public string nombre { get; set; }
    }
}
