using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Ui
{
    public class Login
    {
        [BsonElement("email")]
        public string Email {  get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
    }
}
