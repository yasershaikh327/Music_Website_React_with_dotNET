using DataAccess.Models;
using DataAccess.Models.Dto;
using DataAccess.Models.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface IMemberMappper
    {
        Contact Map(ContactDto contactDto);
    }

    public class MemberMapper : IMemberMappper
    {

        //Contact
        public Contact Map(ContactDto contactDto)
        {
            var contact = new Contact()
            {
                Name = contactDto.Name,
                Email = contactDto.Email,
                Message = contactDto.Message,
            };
            return contact;
        }


      

    }
}
