using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Ui
{
    public class Registration
    {
        [BsonId] // Marks this property as the document's Id
        public ObjectId Id { get; set; }

        [BsonElement("firstname")]
        public string Firstname { get; set; }

        [BsonElement("lastname")]
        public string Lastname { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }


    }
}
