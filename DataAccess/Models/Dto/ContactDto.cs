using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DataAccess.Models.Dto
{
    public class ContactDto
    {
        [BsonId] // Marks this property as the document's Id
        public ObjectId Id { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }
    }
}
