using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceModel
{
    public class EmailServiceModel
    {
        public string Email {  get; set; }  
        public string Subject { get; set; }  = string.Empty;
        public string Body {  get; set; } = string.Empty ;
    }
}
