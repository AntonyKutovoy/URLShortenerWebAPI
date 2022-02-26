using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace URLShortener_WebAPI.Model
{
    public class UrlDto
    {
        public string Shortened { get; set; }
        public string Original { get; set; }
        public int ViewCount { get; set; }
    }
}
