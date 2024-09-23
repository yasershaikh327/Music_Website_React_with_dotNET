using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helper
{
    public interface IHelper
    {
        public Object GetKey();
    }

    public class Helper : IHelper
    {
        public Object GetKey()
        {
            return ObjectId.GenerateNewId();
        }
    }
}
