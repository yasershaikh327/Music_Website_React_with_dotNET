using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbContext
{
    public class AdminCredits
    {
        public string username { get; set; } 
        public ObjectId role { get; set; }    
    }
}
