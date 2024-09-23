using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Dto
{
    public class RegistrationDto
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("firstname")]
        public string Firstname { get; set; }

        [BsonElement("lastname")]
        public string Lastname { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }

    public class RegistrationDtos
    {
        public RegistrationDto _Registration { get; set; }
        public RegistrationDtos(RegistrationDto Registration)
        {
            _Registration = Registration;
        }
    }
}
