using MongoDB.Bson.Serialization.Attributes;

namespace PRUEBA_GSE.Models
{
    public class Clientes
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set;  }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string nombre {  get; set; }
        public string apellido { get; set; }
        public string direccion {  get; set; }
        public string telefono { get; set; }
        public string correo {  get; set; }
        public string noPLacaVehiculo {  get; set; }
    }
}
