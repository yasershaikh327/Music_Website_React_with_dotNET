using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Ui
{
    public class LoginDto
    {
        [BsonElement("email")]
        public string Email {  get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
    }

    public class LoginDtos
    {
        public LoginDto dto { get; set; }

        public LoginDtos(LoginDto dto)
        {
            dto = new LoginDto();
        }
    }
}
