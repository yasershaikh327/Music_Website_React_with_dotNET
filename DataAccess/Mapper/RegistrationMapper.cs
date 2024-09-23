using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface IRegistrationMapper
    {
        Registration Map(RegistrationDto registrationDto);
    }

    public class RegistrationMapper : IRegistrationMapper
    {
        public Registration Map(RegistrationDto registrationDto)
        {
            var registration = new Registration()
            {
                Firstname = registrationDto.Firstname,
                Lastname = registrationDto.Lastname,
                Email = registrationDto.Email,
                Password = registrationDto.Password
            };
            return registration;
        }
    }
}
