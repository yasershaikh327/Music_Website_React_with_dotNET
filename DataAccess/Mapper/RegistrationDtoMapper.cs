using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface IRegistrationDtoMapper
    {
        RegistrationDto Map(Registration registration);
    }

    public class RegistrationDtoMapper : IRegistrationDtoMapper
    {
        public RegistrationDto Map(Registration registration)
        {
            var dto = new RegistrationDto()
            {
                Firstname = registration.Firstname,
                Lastname = registration.Lastname,
                Email = registration.Email,
                Password = registration.Password
            };
           
            return dto;
        }
    }
}
