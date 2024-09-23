using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface ILoginDtoMapper
    {
        LoginDto Map(Login login);
    }

    public class LoginDtoMapper : ILoginDtoMapper
    {
        public LoginDto Map(Login login)
        {
            var dto = new LoginDto()
            {
                Email = login.Email,
                Password = login.Password
            };
           
            return dto;
        }
    }
}
