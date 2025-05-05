using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PRUEBA_GSE.Models
{
    public class Vehiculos
    {
        private string _noPlaca { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string noPlaca { get => _noPlaca; set => _noPlaca = value.ToUpper(); }
        public string modelo { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string tipoVehiculoId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string servicioVehiculoId {get;set;}
        [BsonRepresentation(BsonType.ObjectId)]
        public string usuarioId { get; set; }
        public string formaPago { get; set; }

        
     
    }
}
