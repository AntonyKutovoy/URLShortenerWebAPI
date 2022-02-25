using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace URLShortener_WebAPI.Model
{
    public class UrlDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Original { get; set; }
        public string Shortened { get; set; }
        public string VisitCount { get; set; }
    }
}
