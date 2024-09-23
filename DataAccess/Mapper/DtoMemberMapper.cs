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
    public interface IDtoMemberMapper
    {
        ContactDto Map(Contact contact);
    }

    public class DtoMemberMapper : IDtoMemberMapper
    {

        //Contact
        public ContactDto Map(Contact contact)
        {
            var contactDto = new ContactDto()
            {
                Name = contact.Name,
                Email = contact.Email,
                Message = contact.Message,
            };
            return contactDto;
        }

       
      
    }
}
