using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface ILoginMapper
    {
        Login Map(LoginDto loginDto);
    }

    public class LoginMapper : ILoginMapper
    {
        public Login Map(LoginDto loginDto)
        {
            var login = new Login()
            {
                Email = loginDto.Email,
                Password = loginDto.Password
            };
            return login;
        }
    }
}
